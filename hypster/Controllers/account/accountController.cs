using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;
using SolveMedia;
using AreYouAHuman;

namespace hypster.Controllers
{
    [Authorize]
    public class accountController : ControllerBase
    {
        //*********************************************************************************************************
        #region Account_Register_Login




        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult SignIn(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        //public ActionResult SignIn(hypster.ViewModels.LoginViewModel model, string returnUrl)
        //
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult SignIn(string UserName, string Password, string RememberMe, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
                if (membersManager.ValidateUser(UserName, Password))
                {
                    string IP_Address;
                    IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                    if (IP_Address == null)
                        IP_Address = Request.ServerVariables["REMOTE_ADDR"];
                    else
                        IP_Address = "";


                    //save to database user activity data
                    hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
                    membersManager.CleanMemberFromCache(UserName);

                    member = membersManager.getMemberByUserName(UserName);
                    membersManager.UpdateMemberActivityData(User.Identity.Name, member.id, DateTime.Now, IP_Address);


                    //track user logins
                    hypster_tv_DAL.TrackLoginManagement trackLoginManager = new hypster_tv_DAL.TrackLoginManagement();

                    hypster_tv_DAL.TrackLogin trkLogin = new hypster_tv_DAL.TrackLogin();
                    trkLogin.User_id = member.id;
                    trkLogin.Login_Date = DateTime.Now;
                    trackLoginManager.hyDB.TrackLogins.AddObject(trkLogin);
                    trackLoginManager.hyDB.SaveChanges();



                    //----------------------------------------------------------------------------------------------
                    //this code is updating email tracker (some another tracker can be implemented)
                    //
                    if (HttpContext.Request.Cookies.AllKeys.Contains("ETT") || member.ArtistLevel > 0)
                    {
                        membersManager.UpdateMemberTrackData(User.Identity.Name, member.id);

                        if (HttpContext.Request.Cookies["ETT"] != null)
                        {
                            HttpCookie cookie = HttpContext.Request.Cookies["ETT"];
                            cookie.Expires = DateTime.Now.AddDays(-4);
                            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                        }
                    }
                    //----------------------------------------------------------------------------------------------




                    bool isActive_check = true;
                    isActive_check = membersManager.isActiveCheck(member.id);

                    if (isActive_check == true)
                    {

                        bool rembr_me = false;

                        if (RememberMe == "on")
                        {
                            rembr_me = true;
                        }

                        FormsAuthentication.SetAuthCookie(UserName, rembr_me);

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ViewBag.ActivateAccount = true;
                        ModelState.AddModelError("", "User is deactivated.");
                    }


                }
                else
                {
                    ViewBag.ForgotPassword = true;
                    ModelState.AddModelError("", "The user name or password provided is incorrect. ");
                }

            }


            ViewBag.ReturnUrl = returnUrl;


            // If we got this far, something failed, redisplay form
            return View();
        }
        //----------------------------------------------------------------------------------------------------------






        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult SignInD(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;



            if (User.Identity.IsAuthenticated)
            {
                return RedirectPermanent(returnUrl);
            }


            if (Request.QueryString["RLD"] == null)
            {
                ViewBag.Reload = 1;
                ViewBag.Reload_URL = "/account/SignInD?RLD=1&ReturnUrl=" + returnUrl;
            }

            return View();
        }
        //----------------------------------------------------------------------------------------------------------








        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult Unsibscribe()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Unsibscribe(string email)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByEmail(email);

            if (curr_user.id > 0)
            {
                memberManager.UpdateMemberProfileDetails(curr_user.username, curr_user.id, curr_user.name, curr_user.AutoshareEnabled, (DateTime)curr_user.birth, curr_user.city, curr_user.country, curr_user.zipcode, (byte)curr_user.sex, (int)curr_user.user_interest, 0);
                ViewBag.result = "Thanks, you has been successfully unsubscribed.";
            }
            else
            {
                ViewBag.result = "Member not found.";
            }



            return View("UnsibscribeThanks");
        }
        //----------------------------------------------------------------------------------------------------------










        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(string user_name, string email)
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------




            //-----------------------------------------------------------------------------------------------------
            if (email != null && email != "")
            {
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByEmail(email);

                if (curr_user.id != 0)
                {
                    string emailString = string.Format("\r\nYour Hypster login details are as follows:\r\n\r\nUsername: {0}\r\nPassword: {1}\r\n\r\nThank you,\r\nhelp@hypster.com", curr_user.username, curr_user.password);
                    emailManager.SendPasswordRecoveryEMail(curr_user.email, emailString);
                }
            }


            if (user_name != null && user_name != "")
            {
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(user_name);

                if (curr_user.id != 0)
                {
                    string emailString = string.Format("\r\nYour Hypster login details are as follows:\r\n\r\nUsername: {0}\r\nPassword: {1}\r\n\r\nThank you,\r\nhelp@hypster.com", curr_user.username, curr_user.password);
                    emailManager.SendPasswordRecoveryEMail(curr_user.email, emailString);
                }
            }
            //-----------------------------------------------------------------------------------------------------



            return RedirectToAction("ForgotPasswordDone", "account");
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult ForgotPasswordDone()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------









        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult ActivateAccount()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ActivateAccount(string user_name, string email)
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------




            //-----------------------------------------------------------------------------------------------------
            if (email != null && email != "")
            {
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByEmail(email);

                if (curr_user.id != 0)
                {
                    memberManager.DeleteDeactivatedUser(curr_user.id);
                }
            }


            if (user_name != null && user_name != "")
            {
                hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
                curr_user = memberManager.getMemberByUserName(user_name);

                if (curr_user.id != 0)
                {
                    memberManager.DeleteDeactivatedUser(curr_user.id);
                }
            }
            //-----------------------------------------------------------------------------------------------------



            return RedirectToAction("ActivateAccountDone", "account");
        }
        //----------------------------------------------------------------------------------------------------------


        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult ActivateAccountDone()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------


        









        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult Register()
        {
            hypster.ViewModels.LoginViewModel model = new ViewModels.LoginViewModel();
            
            //LIVE
            ViewBag.ChallengeKey = System.Configuration.ConfigurationManager.AppSettings["SolveMediaChallengeKey"];



            ViewBag.updatesAPlaylists = "checked='checked'";



            //________________________________________________________________________________________
            int CURR_CAPTCHA = 1;

            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["CurrCaptcha_1"] != null)
            {
                CURR_CAPTCHA = (int)i_chache["CurrCaptcha_1"];

                CURR_CAPTCHA = CURR_CAPTCHA * -1;

                ViewBag.CurrCaptcha = CURR_CAPTCHA;


                i_chache.Remove("CurrCaptcha_1");
                i_chache.Add("CurrCaptcha_1", CURR_CAPTCHA, DateTime.Now.AddDays(1));
            }
            else
            {
                CURR_CAPTCHA = 1;

                ViewBag.CurrCaptcha = CURR_CAPTCHA;
                
                i_chache.Add("CurrCaptcha_1", CURR_CAPTCHA, DateTime.Now.AddSeconds(1800));
            }
            //________________________________________________________________________________________


            
            return View();
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(string email, string username, string password, string I_agree, string adcopy_challenge, string adcopy_response, int? interestP, string checkNewsAboutMusic)
        {
            //LIVE
            ViewBag.ChallengeKey = System.Configuration.ConfigurationManager.AppSettings["SolveMediaChallengeKey"];



            
            //CHECK IF RECAPTCHA CODE VALID
            
            //-----------------------------------------------------------------------------------------------------
            AyahServiceIntegration AyahDirect = new AyahServiceIntegration();
            string sessionSecret = this.Request.Form.Get("session_secret");
            if (AyahDirect.ScoreResult(sessionSecret))
            {
                AyahDirect.RecordConversion(sessionSecret);

                Session["skipcaptcha"] = true;
                ViewBag.SkipCaptcha = "on";
            }
            else
            {
                ViewBag.SkipCaptcha = "on";
            }
            
            //-----------------------------------------------------------------------------------------------------





            //CHECK IF RECAPTCHA CODE VALID
            //-----------------------------------------------------------------------------------------------------
            SolveMedia.SolveMediaValidator validator = new SolveMediaValidator();
            string message_recaptcha = "";
            bool is_recaptch_valid = true;
            if (Session["skipcaptcha"] == null)
            {
                //is_recaptch_valid = validator.check_answer(System.Configuration.ConfigurationManager.AppSettings["SolveMediaVerificationKey"], System.Configuration.ConfigurationManager.AppSettings["SolveMediaHashKey"], adcopy_challenge, adcopy_response, out message_recaptcha);

                if (is_recaptch_valid == true)
                {
                    Session["skipcaptcha"] = true;
                    ViewBag.SkipCaptcha = "on";
                }
            }
            else
            {
                ViewBag.SkipCaptcha = "on";
            }
            //-----------------------------------------------------------------------------------------------------







            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement member_manager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            //-----------------------------------------------------------------------------------------------------


            ViewBag.email = email;
            ViewBag.username = username;


            //-----------------------------------------------------------------------------------------------------
            if(email != "" && username != "" && password != "" && I_agree == "on")
            {
                //1.STEP - validate user input
                if (Regex.IsMatch(username, @"^[a-zA-Z0-9-_@]+$") == false)
                {
                    ModelState.AddModelError("", " Username may contain only letters and numbers. ");
                    return View();
                }
                if (password.Length < 6)
                {
                    ModelState.AddModelError("", "Password must be 6 characters and more. ");
                    return View();
                }
                if (email == "")
                {
                    ModelState.AddModelError("", "Please enter email address. ");
                    return View();
                } 
                /*if(Regex.IsMatch(email, "^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$") == false){ModelState.AddModelError("", " Please enter valid email address");return View();}*/





                //2.step - validate database
                //current data verification

                //bool username_validate = true;
                if (member_manager.getMemberByUserName(username).id == 0)
                {

                    bool member_validate = false;
                    member_validate = member_manager.ValidateUser(username, password);

                    hypster_tv_DAL.Member member_email = new hypster_tv_DAL.Member();
                    member_email = member_manager.getMemberByEmail(email);


                    if (member_validate == false && member_email.id == 0) //user not exist (add new)
                    {
                        hypster_tv_DAL.Member MEMBER_TO_ADD = new hypster_tv_DAL.Member();
                        MEMBER_TO_ADD.username = username;
                        MEMBER_TO_ADD.password = password;
                        MEMBER_TO_ADD.email = email;
                        MEMBER_TO_ADD.regdate = DateTime.Now;


                        string IP_Address;
                        IP_Address = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (IP_Address == null)
                            IP_Address = Request.ServerVariables["REMOTE_ADDR"];
                        else
                            IP_Address = "";


                        MEMBER_TO_ADD.RegistrationIp = IP_Address;
                        MEMBER_TO_ADD.LastActivityIp = IP_Address;
                        MEMBER_TO_ADD.LastActivityDate = DateTime.Now;
                        MEMBER_TO_ADD.AutoshareEnabled = true;

                        if(interestP != null)
                        {
                            MEMBER_TO_ADD.user_interest = interestP;
                        }


                        
                        
                        bool updatesAPlaylists = false;
                        if (checkNewsAboutMusic == "on")
                        {
                            updatesAPlaylists = true;
                        }
                        else
                        {
                            ViewBag.updatesAPlaylists = "";
                        }

                        MEMBER_TO_ADD.email_optout = (int)hypster_tv_DAL.NewsletterOptions.None;
                        
                        if (updatesAPlaylists == true)
                        {
                            MEMBER_TO_ADD.email_optout = (int)hypster_tv_DAL.NewsletterOptions.Playlists;
                        }
                        


                        //add new member
                        hyDB.Members.AddObject(MEMBER_TO_ADD);
                        hyDB.SaveChanges();



                        //save captcha code
                        //-----------------------------------------------------------------------------------
                        hypster_tv_DAL.CaptchaLog captchaLog = new hypster_tv_DAL.CaptchaLog();
                        captchaLog.CaptchaIP = IP_Address;
                        captchaLog.CatpchaDate = DateTime.Now.AddHours(3);
                        captchaLog.CaptchaFrase = adcopy_response;
                        captchaLog.CaptchaResponse = is_recaptch_valid.ToString() + ": " + message_recaptcha;
                        hyDB.CaptchaLogs.AddObject(captchaLog);
                        hyDB.SaveChanges();






                        //need to add email verification logic
                        //-----------------------------------------------------------------------------------
                        FormsAuthentication.SetAuthCookie(username, false);
                        emailManager.SendWelcomeEmail("Welcome to Hypster", MEMBER_TO_ADD.email);
                        //-----------------------------------------------------------------------------------




                        //jasons - solve email api
                        //-----------------------------------------------------------------------------------
                        SolvemediaDAPI smDAPI = new SolvemediaDAPI("db42bt");       //set your site ID
                        var userData = new Dictionary<string, string>();            //end-user data
                        userData.Add("hema", SolvemediaDAPI.hashEmail(MEMBER_TO_ADD.email));
                        ViewBag.smDAPI = smDAPI.getHTML(userData);                  //generate the data collection code    
                        //-----------------------------------------------------------------------------------



                        
                        return View("Register_Thanks");

                    }
                    else //if user already exist - need to verify what is used and display message
                    {
                        if (member_validate == true)
                        {
                            ModelState.AddModelError("", " You already registrered. Please login. ");
                        }

                        if (member_email.id != 0)
                        {
                            ModelState.AddModelError("", " User with following email already registered. ");
                        }

                        return View(); //return current screen
                    }

                }
                else
                {
                    ModelState.AddModelError("", " This username is occupied by someone else. ");
                }

            }
            else //missing some info
            {
                if (email == "")
                {
                    ModelState.AddModelError("", " Missing email. ");
                }

                if (username == "")
                {
                    ModelState.AddModelError("", " Missing username. ");
                }

                if (password == "")
                {
                    ModelState.AddModelError("", " Missing password. ");
                }

                if (I_agree != "on")
                {
                    ModelState.AddModelError("", " You must agree to the terms and conditions. ");
                }
            }
            //-----------------------------------------------------------------------------------------------------



            if (I_agree == "on")
            {
                ViewBag.I_agree = "on";
            }


            if (checkNewsAboutMusic == "on")
            {
                ViewBag.updatesAPlaylists = "checked='checked'";
            }
            else
            {
                ViewBag.updatesAPlaylists = "";
            }
            


            return View();
            
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Register_Thanks()
        {
            return View();
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Deactivate()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            memberManager.DeactivateUser(memberManager.getMemberByUserName(User.Identity.Name).id);

            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        //----------------------------------------------------------------------------------------------------------




        #endregion
        //*********************************************************************************************************








        //*********************************************************************************************************
        #region AccountPicsManagement
        

       



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult AccountPicsManagement_UploadPics(HttpPostedFileBase file, string PicDesc)
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Image_Resize_Manager image_resizer = new hypster_tv_DAL.Image_Resize_Manager();
            //-----------------------------------------------------------------------------------------------------



            if (file != null)
            {

                //1st step
                //check if user folder exist
                //-----------------------------------------------------------------------------------------------------
                System.IO.DirectoryInfo user_dir = new System.IO.DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name);
                if (user_dir.Exists == false)
                {
                    user_dir.Create();
                }
                //-----------------------------------------------------------------------------------------------------




                //-----------------------------------------------------------------------------------------------------
                int image_id = 0;
                image_id = photoManager.AddUserPhoto(memberManager.getMemberByUserName(User.Identity.Name).id, PicDesc);


                //2nd step
                //save image in user folder
                file.SaveAs(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg");
                image_resizer.Resize_Image(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg", 1000, -1, System.Drawing.Imaging.ImageFormat.Jpeg);


                //save thumbnail
                System.IO.FileInfo thumb_file = new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg");
                string new_thumb_path = System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + "_s.jpg";
                thumb_file.CopyTo(new_thumb_path, true);
                image_resizer.Resize_Image(new_thumb_path, 150, 150, System.Drawing.Imaging.ImageFormat.Jpeg);
                //-----------------------------------------------------------------------------------------------------

            }



            return RedirectToAction("pics", "account");
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public string AccountPicsUploadCameraPhoto(string content)
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.Image_Resize_Manager image_resizer = new hypster_tv_DAL.Image_Resize_Manager();
            //-----------------------------------------------------------------------------------------------------


          

            //1st step
            //check if user folder exist
            //-----------------------------------------------------------------------------------------------------
            System.IO.DirectoryInfo user_dir = new System.IO.DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name);
            if (user_dir.Exists == false)
            {
                user_dir.Create();
            }
            //-----------------------------------------------------------------------------------------------------



            // 3.upload camera photo and save
            //-----------------------------------------------------------------------------------------------------
            int image_id = 0;
            image_id = photoManager.AddUserPhoto(memberManager.getMemberByUserName(User.Identity.Name).id, "");



            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(content);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            image.Save(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg");


            //2nd step
            //save image in user folder
            image_resizer.Resize_Image(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg", 1000, -1, System.Drawing.Imaging.ImageFormat.Jpeg);


            //save thumbnail
            System.IO.FileInfo thumb_file = new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg");
            string new_thumb_path = System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + "_s.jpg";
            thumb_file.CopyTo(new_thumb_path, true);
            image_resizer.Resize_Image(new_thumb_path, 150, 150, System.Drawing.Imaging.ImageFormat.Jpeg);
            //-----------------------------------------------------------------------------------------------------



            return "success";
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult DeleteImage()
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            int image_id = 0;
            //-----------------------------------------------------------------------------------------------------


            // 2.delete user photo
            //-----------------------------------------------------------------------------------------------------
            if (Request.QueryString["image_id"] != null && Int32.TryParse(Request.QueryString["image_id"], out image_id) == true)
            {   
                photoManager.DeleteUserPhoto(image_id, memberManager.getMemberByUserName(User.Identity.Name).id);

                try
                {
                    System.IO.FileInfo file_large = new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + ".jpg");
                    System.IO.FileInfo file_small = new System.IO.FileInfo(System.Configuration.ConfigurationManager.AppSettings["userPics_StoragePath"] + "\\" + User.Identity.Name + "\\" + image_id + "_s.jpg");
                    file_large.Delete();
                    file_small.Delete();
                }
                catch (Exception ex) { }
            }
            //-----------------------------------------------------------------------------------------------------


            return RedirectToAction("pics", "account");
        }
        //----------------------------------------------------------------------------------------------------------



        //----------------------------------------------------------------------------------------------------------
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult SetAsProfileImage()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            int image_id = 0;
            if (Request.QueryString["image_id"] != null && Int32.TryParse(Request.QueryString["image_id"], out image_id) == true)
            {
                memberManager.UpdateMemberProfilePicture(User.Identity.Name, memberManager.getMemberByUserName(User.Identity.Name).id, image_id);
            }
            //-----------------------------------------------------------------------------------------------------


            return RedirectToAction("pics", "account");
        }
        //----------------------------------------------------------------------------------------------------------



        //called by ajax and shown as popup
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult pr_PicturesViewer()
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------


            
            // 2.parse query string
            //-----------------------------------------------------------------------------------------------------
            string view_pics_user_name = "";
            if (Request.QueryString["username"] != null)
            {
                view_pics_user_name = Request.QueryString["username"];
                ViewBag.view_pics_user_name = view_pics_user_name;
            }
            if (Request.QueryString["imgid"] != null)
            {
                ViewBag.active_img_id = Request.QueryString["imgid"];
            }
            //-----------------------------------------------------------------------------------------------------




            // 3. get member photo model
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.Member member_user = new hypster_tv_DAL.Member();
            member_user = memberManager.getMemberByUserName(view_pics_user_name);


            List<hypster_tv_DAL.Photo> photos_list = new List<hypster_tv_DAL.Photo>();
            photos_list = photoManager.GetUserPhotos(member_user.id);
            //-----------------------------------------------------------------------------------------------------
            



            return View(photos_list);
        }

        #endregion
        //*********************************************************************************************************







        //*********************************************************************************************************
        #region AccountInfoManagement


        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Index()
        {
            return RedirectToAction("info");
        }
        

        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult info()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------


            // 2.get user model
            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.getAccountInfo_PrivateViewModel model = new ViewModels.getAccountInfo_PrivateViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.userPhotos_list = photoManager.GetUserPhotos(model.curr_user.id);

            if (model.curr_user.email_optout == (int)hypster_tv_DAL.NewsletterOptions.Both)
            {
                ViewBag.checkNewsAboutMusic = true;
                
            }
            if (model.curr_user.email_optout == (int)hypster_tv_DAL.NewsletterOptions.Playlists)
            {
                ViewBag.checkNewsAboutMusic = true;
            }
            
            if (model.curr_user.email_optout == (int)hypster_tv_DAL.NewsletterOptions.None)
            {
                ViewBag.checkNewsAboutMusic = false;
            }


            return View("acctInfo", model);
        }


        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult music()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------


            // 2.get user model
            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.getAccountInfo_PrivateViewModel model = new ViewModels.getAccountInfo_PrivateViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            

            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            model.userPlayers_list = playerManager.GetUserPlayersList(model.curr_user.id);
            model.musicGenres_list = genreManager.GetMusicGenresList();
            model.radioStation_list = radioStationManager.GetUserRadioStations(model.curr_user.id);

            //get playlists I like
            model.playlists_I_like = playlistManager.GetPlaylistsILike_NoCache(model.curr_user.id);


            // 3.update model music genres according to user selection
            //-----------------------------------------------------------------------------------------------------
            List<hypster_tv_DAL.MemberMusicGenre> memberMusicGenre_list = new List<hypster_tv_DAL.MemberMusicGenre>();
            memberMusicGenre_list = genreManager.GetUserMusicGenres(model.curr_user.id);

            foreach (var item in model.musicGenres_list)
            {
                foreach (var item_sel in memberMusicGenre_list)
                {
                    if (item.Genre_ID == item_sel.MusicGenre_ID)
                    {
                        item.selected = true;
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------------





            return View("acctMusic", model);
        }




        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult friends()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------


            // 2.get user model
            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.getAccountInfo_PrivateViewModel model = new ViewModels.getAccountInfo_PrivateViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            

            model.NumberOfMyFollowers = followersManager.GetNumberOfMyFollowers(model.curr_user.id);
            model.NumberOfMembersIFollow = followersManager.GetNumberOfMembersIFollow(model.curr_user.id);
            model.MembersIFollow_list = followersManager.GetMembersIFollow(memberManager.getMemberByUserName(User.Identity.Name).id);
            model.MyFollowers_list = followersManager.GetMyFollowers(memberManager.getMemberByUserName(User.Identity.Name).id);
            //-----------------------------------------------------------------------------------------------------




            return View("acctFriends", model);
        }




        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult pics()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------


            // 2.get user model
            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.getAccountInfo_PrivateViewModel model = new ViewModels.getAccountInfo_PrivateViewModel();
            model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
            model.userPhotos_list = photoManager.GetUserPhotos(model.curr_user.id);


            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);



            return View("acctPics", model);
        }

        #endregion
        //*********************************************************************************************************







        //*********************************************************************************************************
        public ActionResult InfoPost(string FName, string LName, string Name, string Address, string City, string State, string Country, string Zip, int DOB_MM, int DOB_DD, int DOB_YYYY, int Sex, string Introduce, int? interestP, string OldUserPass, string NewUserPass, string RepNewUserPass, string checkNewsAboutMusic)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);

            hypster.ViewModels.getAccountInfo_PrivateViewModel model = new ViewModels.getAccountInfo_PrivateViewModel();


            if (FName != null)
                curr_user.first_name = FName;

            if (LName != null)
                curr_user.last_name = LName;

            if (Name != null)
                curr_user.name = Name;

            if (Address != null)
                curr_user.address = Address;

            if (City != null)
                curr_user.city = City;

            if (State != null)
                curr_user.state = State;            

            if (Zip != null)
                curr_user.zipcode = Zip;

            if (Country != null)
                curr_user.country = Country;

            if (DOB_MM != null && DOB_DD != null && DOB_YYYY != null && DOB_MM != 0 && DOB_DD != 0 && DOB_YYYY != 0)
            {
                curr_user.birth = new DateTime(DOB_YYYY, DOB_MM, DOB_DD);
            }


            if (curr_user.sex != null)
            {
                curr_user.sex = (byte)Sex;
            }
            else
            {
                curr_user.sex = (byte)0;
            }

            if (Introduce != null)
                curr_user.introduce = Introduce;

            int USER_INTEREST = 0;
            if (interestP != null)
            {
                USER_INTEREST = (int)interestP;
            }

            
            bool updatesAPlaylists = false;
            if (checkNewsAboutMusic == "on")
            {
                updatesAPlaylists = true;
            }


            curr_user.email_optout = (int)hypster_tv_DAL.NewsletterOptions.None;
            
            if (updatesAPlaylists == true)
            {
                curr_user.email_optout = (int)hypster_tv_DAL.NewsletterOptions.Playlists;
            }
            
            memberManager.UpdateMemberProfileDetails(curr_user.username, curr_user.id, curr_user.name, curr_user.AutoshareEnabled, (DateTime)curr_user.birth, curr_user.city, curr_user.country, curr_user.zipcode, (byte)curr_user.sex, USER_INTEREST, curr_user.email_optout);
            memberManager.UpdateMemberProfileDetails(curr_user.username, curr_user.id, curr_user.first_name, curr_user.last_name, curr_user.address, curr_user.state, curr_user.introduce);

            bool isError = false;
            if (OldUserPass != "" && NewUserPass != "" && RepNewUserPass != "")
            {
                if (curr_user.password == OldUserPass)
                {
                    if (NewUserPass == RepNewUserPass)
                    {
                        if (NewUserPass.Length >= 6)
                        {
                            memberManager.UpdateMemberPassword(curr_user.username, curr_user.id, NewUserPass);
                        }
                        else
                        {
                            ViewBag.ErrorStr = "Password must be 6 characters or longer";
                            isError = true;
                        }
                    }
                    else
                    {
                        ViewBag.ErrorStr = "Wrong Repeat New Password";
                        isError = true;
                    }
                }
                else
                {
                    ViewBag.ErrorStr = "Wrong Old Password";
                    isError = true;
                }
            }



            if (isError == true)
            {
                model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);
                model.userPhotos_list = photoManager.GetUserPhotos(model.curr_user.id);
                return View("acctInfo", model);
            }



            return RedirectPermanent("/account/info");
        }


        public ActionResult MusicPost(List<int> musicGenres_list)
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            //-----------------------------------------------------------------------------------------------------




            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            // 5.manage music genres
            //----------------------------------------------------------------------------------------
            if (musicGenres_list != null)
            {   
                genreManager.DeleteAllUserMusicGenres(curr_user.id);

                foreach (int music_genre_sel in musicGenres_list)
                {
                    hypster_tv_DAL.MemberMusicGenre member_genre = new hypster_tv_DAL.MemberMusicGenre();
                    member_genre.Member_ID = curr_user.id;
                    member_genre.MusicGenre_ID = music_genre_sel;

                    genreManager.hyDB.MemberMusicGenres.AddObject(member_genre);
                }

                genreManager.hyDB.SaveChanges();
            }
            //----------------------------------------------------------------------------------------




            return RedirectPermanent("/account/music");
        }
        //*********************************************************************************************************









        //*********************************************************************************************************
        #region PlaylistsManagement_ACTIONS



        //----------------------------------------------------------------------------------------------------------
        // Edit playlist name
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult AccountPlaylists_EditPlaylist(string PlaylistName, int PlaylistID)
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            playlistManager.Edit_Playlist(memberManager.getMemberByUserName(User.Identity.Name).id, PlaylistID, PlaylistName);



            return RedirectPermanent("/create/playlist");
        }
        //----------------------------------------------------------------------------------------------------------




        //----------------------------------------------------------------------------------------------------------
        // Make playlist active
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult AccountPlaylists_MakeActivePlaylist(int Act_PlaylistID)
        {
            hypster_tv_DAL.memberManagement userManager = new hypster_tv_DAL.memberManagement();
            
            userManager.SetUserDefaultPlaylist(User.Identity.Name, userManager.getMemberByUserName(User.Identity.Name).id, Act_PlaylistID);



            return RedirectPermanent("/create/playlist");
        }
        //----------------------------------------------------------------------------------------------------------





        //----------------------------------------------------------------------------------------------------------
        // Add new Playlist
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [HttpPost]
        public ActionResult AccountPlaylist_AddNewPlaylist(string AddPlaylist_Name)
        {
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            //-----------------------------------------------------------------------------------------------------



            if (AddPlaylist_Name != "")
            {
                //-----------------------------------------------------------------------------------------------------
                hypster_tv_DAL.Member member = new hypster_tv_DAL.Member();
                member = memberManager.getMemberByUserName(User.Identity.Name);


                //-----------------------------------------------------------------------------------------------------
                hypster_tv_DAL.Playlist playlist = new hypster_tv_DAL.Playlist();
                playlist.name = AddPlaylist_Name;
                playlist.userid = member.id;

                string crtd = DateTime.Now.ToString("yyyyMMdd");
                int crtd_i = 0;
                Int32.TryParse(crtd, out crtd_i);
                playlist.create_time = crtd_i;

                if (playlist.name.Length > 60)
                    playlist.name = playlist.name.Substring(0, 60);


                hypster_tv_DAL.Hypster_Entities hyDB_man = new hypster_tv_DAL.Hypster_Entities();
                hyDB_man.Playlists.AddObject(playlist);
                hyDB_man.SaveChanges();
                //-----------------------------------------------------------------------------------------------------

                hypster_tv_DAL.playlistManagement playlistManagement = new hypster_tv_DAL.playlistManagement();
                List<hypster_tv_DAL.Playlist> playlists_list = playlistManagement.GetUserPlaylists(member.id);
                if (member.active_playlist == 0 && playlists_list.Count > 0)
                {
                    member.active_playlist = playlists_list[0].id;
                    memberManager.SetUserDefaultPlaylist(User.Identity.Name, member.id, member.active_playlist);
                }
            }


            return RedirectPermanent("/create/playlist");
        }
        //----------------------------------------------------------------------------------------------------------


        #endregion
        //*********************************************************************************************************



        

        //*********************************************************************************************************
        #region AccountFollowers_ACTIONS


        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Delete_MemberIFollow()
        {

            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.followersManagement folManager = new hypster_tv_DAL.followersManagement();
            //-----------------------------------------------------------------------------------------------------


            // 2.delete member I follow
            //-----------------------------------------------------------------------------------------------------
            int follower_id = 0;
            if (Request.QueryString["follower_id"] != null && Int32.TryParse(Request.QueryString["follower_id"], out follower_id) == true && membersManager.getMemberByUserName(User.Identity.Name).id != 0)
            {
                folManager.Delete_MemberIFollow(membersManager.getMemberByUserName(User.Identity.Name).id, follower_id);
            }
            //-----------------------------------------------------------------------------------------------------



            return RedirectPermanent("/account/friends");
        }


        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Delete_MyFollower()
        {
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement membersManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.followersManagement folManager = new hypster_tv_DAL.followersManagement();
            //-----------------------------------------------------------------------------------------------------


            // 2.delete my follower
            //-----------------------------------------------------------------------------------------------------
            int follower_id = 0;
            if (Request.QueryString["follower_id"] != null && Int32.TryParse(Request.QueryString["follower_id"], out follower_id) == true && membersManager.getMemberByUserName(User.Identity.Name).id != 0)
            {
                folManager.Delete_MyFollower(membersManager.getMemberByUserName(User.Identity.Name).id, follower_id);
            }
            //-----------------------------------------------------------------------------------------------------


            return RedirectPermanent("/account/friends");
        }


        #endregion
        //*********************************************************************************************************





        //*********************************************************************************************************
        /// <summary>
        /// Delete user radio station
        /// </summary>
        /// <returns></returns>
        public ActionResult deleteStation()
        {
            int stationID = 0;
            if (Request.QueryString["station"] != "")
            {
                Int32.TryParse(Request.QueryString["station"], out stationID);

                if (stationID > 0)
                {
                    hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
                    hypster_tv_DAL.RadioStationManager stationManager = new hypster_tv_DAL.RadioStationManager();
                    stationManager.DeleteUserRadioStation(memberManager.getMemberByUserName(User.Identity.Name).id, stationID);
                }
            }


            if (Request.QueryString["gb"] != null)
            {
                return RedirectPermanent("/create/station");
            }
            

            return RedirectPermanent("/account/music");
        }
        //*********************************************************************************************************



        
















        //
        // EXPENSIVE CALL - WILL NEED TO OPTIMIZE (redirected now to music page)
        //
        //*********************************************************************************************************
        #region Public Profile

        
        // for public profile
        //
        //----------------------------------------------------------------------------------------------------------
        //this mainly related to follow friends
        //users who visit follow friends getting 240 sec caching and 
        //also users public profiles should have same cache duration
        [AllowAnonymous]
        [OutputCache(Duration = 120, VaryByParam = "none")]
        public ActionResult getPublicProfile(string user_name)
        {
            
            // 1.general declarations
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.getAccountInfo_PublicViewModel model = new ViewModels.getAccountInfo_PublicViewModel();
            //-----------------------------------------------------------------------------------------------------

            
            


            //-----------------------------------------------------------------------------------------------------
            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);

            model.curr_user = memberManager.getMemberByUserName(user_name);
            if (model.curr_user.id == 0)
            {
                return View("getAccountInfo_Public", model);
            }

            bool isActive_check = true;
            isActive_check = memberManager.isActiveCheck(model.curr_user.id);
            if (isActive_check == false)
            {
                return RedirectPermanent("/");
            }

            model.userPhotos_list = photoManager.GetUserPhotos(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);
            model.musicGenres_list = genreManager.GetMusicGenresList();
            model.radioStation_list = radioStationManager.GetUserRadioStations(model.curr_user.id);


            List<hypster_tv_DAL.MemberMusicGenre> memberMusicGenre_list = new List<hypster_tv_DAL.MemberMusicGenre>();
            memberMusicGenre_list = genreManager.GetUserMusicGenres(model.curr_user.id);
            foreach (var item in model.musicGenres_list)
            {
                foreach (var item_sel in memberMusicGenre_list)
                {
                    if (item.Genre_ID == item_sel.MusicGenre_ID)
                    {
                        item.selected = true;
                    }
                }
            }
            //-----------------------------------------------------------------------------------------------------





            //-----------------------------------------------------------------------------------------------------
            model.NumberOfMyFollowers = followersManager.GetNumberOfMyFollowers(model.curr_user.id);
            model.NumberOfMembersIFollow = followersManager.GetNumberOfMembersIFollow(model.curr_user.id);
            model.MembersIFollow_list = followersManager.GetMembersIFollow(model.curr_user.id);
            model.MyFollowers_list = followersManager.GetMyFollowers(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            model.songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.curr_user.active_playlist);
            //-----------------------------------------------------------------------------------------------------




            //check if has public music page
            //-----------------------------------------------------------------------------------------------------
            if(memberManager.GetMemberPublicPageByID(model.curr_user.id).PublicPage_ID > 0)
            {
                return RedirectPermanent("/music/" + user_name);
            }
            //-----------------------------------------------------------------------------------------------------


            
            return View("getAccountInfo_Public", model);
        }
        //----------------------------------------------------------------------------------------------------------



        // for public profile
        //
        //----------------------------------------------------------------------------------------------------------
        //
        // follow friend click event
        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        [AllowAnonymous]
        public ActionResult FollowFriend(int user_to_follow_id)
        {
            //add follower
            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.Hypster_Entities hyDB = new hypster_tv_DAL.Hypster_Entities();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            //-----------------------------------------------------------------------------------------------------




            if (User.Identity.IsAuthenticated)
            {

                if (followersManager.CheckIfAlreadyFollow(user_to_follow_id, memberManager.getMemberByUserName(User.Identity.Name).id) == false)
                {
                    hypster_tv_DAL.Follower follower = new hypster_tv_DAL.Follower();
                    follower.User_ID = user_to_follow_id;
                    follower.Follower_ID = memberManager.getMemberByUserName(User.Identity.Name).id;


                    hyDB.Followers.AddObject(follower);
                    hyDB.SaveChanges();
                }


                return RedirectPermanent("/account/friends");
            }


            return RedirectPermanent("/account/SignIn?ReturnUrl=%2fprofile%2f" + memberManager.getMemberByID(user_to_follow_id).username);
        }
        //----------------------------------------------------------------------------------------------------------

        #endregion
        //*********************************************************************************************************









        //*********************************************************************************************************
        #region Public_Music_Page




        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 15)]
        [AllowAnonymous]
        public ActionResult PublicMusicPage(string user_name)
        {
            //-----------------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                Random r = new Random();
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "") + "?rnd=" + r.Next(1, 100000).ToString();
                return RedirectPermanent(new_url);
            }
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            hypster.ViewModels.MemberPublicPageViewModel model = new ViewModels.MemberPublicPageViewModel();

            model.curr_user = memberManager.getMemberByUserName(user_name);
            model.member_page = memberManager.GetMemberPublicPageByID(model.curr_user.id);
            model.songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, (int)model.member_page.Playlist_ID);
            //-----------------------------------------------------------------------------------------------------



            //-----------------------------------------------------------------------------------------------------
            model.New_User_ID = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["newImageLogicID"]);
            model.userPhotos_list = photoManager.GetUserPhotos(model.curr_user.id);
            //-----------------------------------------------------------------------------------------------------





            //-----------------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated)
            {
                if (user_name == User.Identity.Name) //SAME USER - NEED TO DISPLAY EDIT CONTROLS
                {
                    if (Request.QueryString["isE"] != null && Request.QueryString["isE"] == "y")
                    {
                        model.userPlaylists_list = playlistManager.GetUserPlaylists(model.curr_user.id);

                        if (model.member_page.Playlist_ID == 0 && model.userPlaylists_list.Count > 0)
                        {
                            model.songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, model.userPlaylists_list[0].id);
                        }
                        return View("PublicMusicPage_EDIT", model);
                    }
                    else
                    {
                        return View("PublicMusicPage", model);
                    }
                }
            }

            return View("PublicMusicPage", model);
        }


        //ajax function - called to populate playlist
        public ActionResult GetPublicAcctPlaylist()
        {
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();


            int PLST_ID = 0;
            if (Request.QueryString["PLST_ID"] != null)
            {
                Int32.TryParse(Request.QueryString["PLST_ID"], out PLST_ID);
            }

            int LAYOUT = 1;
            if (Request.QueryString["LAYOUT"] != null)
            {
                Int32.TryParse(Request.QueryString["LAYOUT"], out LAYOUT);
                ViewBag.LAYOUT = LAYOUT;
            }


            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.PlaylistData_Song> songs_list = new List<hypster_tv_DAL.PlaylistData_Song>();
            songs_list = playlistManager.GetSongsForPlayList(curr_user.id, PLST_ID);




            return View(songs_list);
        }


        [HttpPost]
        public ActionResult SavePublicMusicPage(int? hf_MemberPublicPageID, int? PPPlaylistDD, int layout, int? showHeader, int? showDescription, int? autoplay, int? showLikeButtton, int? showInfoButton, int? showPhotosButton, string header, string description, string hf_HaFBackgroundColor, string hf_BackgroundColor, string hf_LeftSectionColor, string hf_RightSectionColor, string hf_ButtonsBackgroundColor, string hf_SongBackgroundColor, string hf_TextColor, string hf_ButtonsColor)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectPermanent("/");
            }


            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);


            hypster_tv_DAL.MemberPublicPage member_page = new hypster_tv_DAL.MemberPublicPage();
            member_page.autoplay = (autoplay > 0) ? true : false;
            member_page.BackgroundColor = hf_BackgroundColor;
            member_page.ButtonsBackgroundColor = hf_ButtonsBackgroundColor;
            member_page.ButtonsColor = hf_ButtonsColor;
            member_page.description = description;
            member_page.HaFBackgroundColor = hf_HaFBackgroundColor;
            member_page.header = header;
            member_page.LeftSectionColor = hf_LeftSectionColor;
            member_page.Member_ID = curr_user.id;

            member_page.RightSectionColor = hf_RightSectionColor;
            member_page.showDescription = (showDescription > 0) ? true : false;
            member_page.showHeader = (showHeader > 0) ? true : false;
            member_page.showInfoButton = (showInfoButton > 0) ? true : false;
            member_page.showLikeButtton = (showLikeButtton > 0) ? true : false;
            member_page.showPhotosButton = (showPhotosButton > 0) ? true : false;
            member_page.SongBackgroundColor = hf_SongBackgroundColor;
            member_page.TextColor = hf_TextColor;
            if (PPPlaylistDD != null)
                member_page.Playlist_ID = PPPlaylistDD;
            else
                member_page.Playlist_ID = 0;
            member_page.Playlist_Layout = layout;


            if (hf_MemberPublicPageID != null && hf_MemberPublicPageID > 0)
            {
                member_page.PublicPage_ID = (int)hf_MemberPublicPageID;
                memberManager.EditMemberPublicPage(member_page);
            }
            else
            {
                memberManager.AddMemberPublicPage(member_page);
            }




            //-------------------------------------------------------------------------------------
            //genreate random number
            Random ran = new Random();
            //-------------------------------------------------------------------------------------


            return RedirectPermanent("/music/" + curr_user.username + "?rnum=" + ran.Next(1, 200000).ToString());
        }

        #endregion
        //*********************************************************************************************************










        //*********************************************************************************************************
        #region Public_Music_Page_Playlist




        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 15)]
        [AllowAnonymous]
        public ActionResult PublicMusicPagePlaylist(int plst_id)
        {
            //-----------------------------------------------------------------------------------------------------
            if (Request.Url.AbsoluteUri.Contains("www."))
            {
                Random r = new Random();
                string new_url = Request.Url.AbsoluteUri.Replace("www.", "") + "?rnd=" + r.Next(1, 100000).ToString();
                return RedirectPermanent(new_url);
            }
            //-----------------------------------------------------------------------------------------------------


            ViewBag.Playlist_ID = plst_id;


            //-----------------------------------------------------------------------------------------------------
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();
            hypster_tv_DAL.playlistManagement playlistManager = new hypster_tv_DAL.playlistManagement();
            hypster_tv_DAL.followersManagement followersManager = new hypster_tv_DAL.followersManagement();
            hypster_tv_DAL.memberPhotoManagement photoManager = new hypster_tv_DAL.memberPhotoManagement();
            hypster_tv_DAL.playerManagement playerManager = new hypster_tv_DAL.playerManagement();
            hypster_tv_DAL.MemberMusicGenreManager genreManager = new hypster_tv_DAL.MemberMusicGenreManager();
            hypster_tv_DAL.RadioStationManager radioStationManager = new hypster_tv_DAL.RadioStationManager();
            //-----------------------------------------------------------------------------------------------------


            hypster.ViewModels.MemberPublicPageViewModel model = new ViewModels.MemberPublicPageViewModel();


            //-----------------------------------------------------------------------------------------------------
            if (User.Identity.IsAuthenticated && Request.QueryString["edit"] != null && Request.QueryString["edit"] == "yes")
            {

                //-----------------------------------------------------------------------------------------------------
                model.curr_user = memberManager.getMemberByUserName(User.Identity.Name);

                model.member_page = memberManager.GetMemberPublicPageByPlaylistID(model.curr_user.id, plst_id);

                model.songs_list = playlistManager.GetSongsForPlayList(model.curr_user.id, plst_id);
                //-----------------------------------------------------------------------------------------------------



                if (Request.QueryString["edit"] != null && Request.QueryString["edit"] == "yes")
                {
                    return View("PublicMusicPagePlaylist_EDIT", model);
                }
                else
                {
                    return View("PublicMusicPagePlaylist", model);
                }
            }
            else //just for viewing
            {
                //-----------------------------------------------------------------------------------------------------
                
                model.member_page = memberManager.GetMemberPublicPageByPlaylistID(plst_id);

                model.curr_user = memberManager.getMemberByID((int)model.member_page.Member_ID);

                model.songs_list = playlistManager.GetSongsForPlayList((int)model.member_page.Member_ID, plst_id);
                //-----------------------------------------------------------------------------------------------------
            }
            

            return View("PublicMusicPagePlaylist", model);
        }


        [HttpPost]
        public ActionResult SavePublicMusicPagePlaylist(int? hf_MemberPublicPageID, int? PPPlaylistDD, int layout, int? showHeader, int? showDescription, int? autoplay, int? showLikeButtton, int? showInfoButton, int? showPhotosButton, string header, string description, string hf_HaFBackgroundColor, string hf_BackgroundColor, string hf_LeftSectionColor, string hf_RightSectionColor, string hf_ButtonsBackgroundColor, string hf_SongBackgroundColor, string hf_TextColor, string hf_ButtonsColor)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectPermanent("/");
            }


            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_user = new hypster_tv_DAL.Member();
            curr_user = memberManager.getMemberByUserName(User.Identity.Name);



            hypster_tv_DAL.MemberPublicPage member_page = new hypster_tv_DAL.MemberPublicPage();
            member_page.autoplay = (autoplay > 0) ? true : false;
            member_page.BackgroundColor = hf_BackgroundColor;
            member_page.ButtonsBackgroundColor = hf_ButtonsBackgroundColor;
            member_page.ButtonsColor = hf_ButtonsColor;
            member_page.description = description;
            member_page.HaFBackgroundColor = hf_HaFBackgroundColor;
            member_page.header = header;
            member_page.LeftSectionColor = hf_LeftSectionColor;
            member_page.Member_ID = curr_user.id;

            member_page.RightSectionColor = hf_RightSectionColor;
            member_page.showDescription = (showDescription > 0) ? true : false;
            member_page.showHeader = (showHeader > 0) ? true : false;
            member_page.showInfoButton = (showInfoButton > 0) ? true : false;
            member_page.showLikeButtton = (showLikeButtton > 0) ? true : false;
            member_page.showPhotosButton = (showPhotosButton > 0) ? true : false;
            member_page.SongBackgroundColor = hf_SongBackgroundColor;
            member_page.TextColor = hf_TextColor;
            if (PPPlaylistDD != null)
                member_page.Playlist_ID = PPPlaylistDD;
            else
                member_page.Playlist_ID = 0;
            member_page.Playlist_Layout = layout;


            if (hf_MemberPublicPageID != null && hf_MemberPublicPageID > 0)
            {
                member_page.PublicPage_ID = (int)hf_MemberPublicPageID;
                memberManager.EditMemberPublicPage(member_page);
            }
            else
            {
                memberManager.AddMemberPublicPage(member_page);
            }




            //-------------------------------------------------------------------------------------
            //genreate random number
            Random ran = new Random();
            //-------------------------------------------------------------------------------------


            return RedirectPermanent("/music_playlist/" + PPPlaylistDD + "?rnum=" + ran.Next(1, 200000).ToString());
        }

        #endregion
        //*********************************************************************************************************



        public ActionResult DeleteLikedPlaylist()
        {
            int playlist_id = 0;
            int us_id = 0;

            if(Request.QueryString["playlist_id"] != null)
            {
                playlist_id = Int32.Parse(Request.QueryString["playlist_id"].ToString());
            }

            if(Request.QueryString["us_id"] != null)
            {
                us_id = Int32.Parse(Request.QueryString["us_id"].ToString());
            }


            hypster_tv_DAL.playlistLikeManagement playlistLikeManager = new hypster_tv_DAL.playlistLikeManagement();
            playlistLikeManager.DeletePlaylistLike(playlist_id, us_id);
                

            return RedirectPermanent("/account/music#dplstLks");
        }






        //*********************************************************************************************************
        [AllowAnonymous]
        public ActionResult Confirm_Member_Account(string id)
        {
            string member_email = id;

            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_member = new hypster_tv_DAL.Member();
            curr_member = memberManager.getMemberByEmail(member_email);

            curr_member.email_confirmed = 1;

            memberManager.UpdateMemberEmailConfirmed(curr_member.username, curr_member.id, curr_member.email_confirmed);

            return View("Confirm_Member_Account_Thanks");
        }

        [AllowAnonymous]
        public ActionResult Confirm_Member_Account_Thanks()
        {
            

            return View();
        }
        //*********************************************************************************************************








        //*********************************************************************************************************
        public ActionResult history()
        {
            hypster_tv_DAL.listenHistoryManagement historyManager = new hypster_tv_DAL.listenHistoryManagement();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();


            hypster_tv_DAL.Member curr_member = new hypster_tv_DAL.Member();
            curr_member = memberManager.getMemberByUserName(User.Identity.Name);


            List<hypster_tv_DAL.ListenHistory> history_list = new List<hypster_tv_DAL.ListenHistory>();
            history_list = historyManager.GetListenHistoryUser(curr_member.id);


            return View(history_list);
        }


        [System.Web.Mvc.OutputCache(NoStore = true, Duration = 0)]
        public ActionResult resendConfirmation()
        {
            hypster_tv_DAL.Email_Manager emailManager = new hypster_tv_DAL.Email_Manager();
            hypster_tv_DAL.memberManagement memberManager = new hypster_tv_DAL.memberManagement();

            hypster_tv_DAL.Member curr_member = new hypster_tv_DAL.Member();
            memberManager.CleanMemberFromCache(User.Identity.Name);
            curr_member = memberManager.getMemberByUserName(User.Identity.Name);

            
            //-----------------------------------------------------------------------------------
            emailManager.SendWelcomeEmail("Welcome to Hypster", curr_member.email);
            //-----------------------------------------------------------------------------------


            return RedirectPermanent("Register_Thanks");
        }
        //*********************************************************************************************************




    }
}
