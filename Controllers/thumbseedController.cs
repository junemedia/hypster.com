using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;


namespace hypster.Controllers
{
    public class thumbseedController : Controller
    {

        //
        // GET: /thumbseed/
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            return View();
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult getSeeds()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string id = "";
            if (Request.QueryString["qq"] != null)
            {
                id = Request.QueryString["qq"].ToString();
            }

            //for href
            ViewBag.ss = id.Replace(' ', '+');


            string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=Music&sid=1692";
            using (XmlReader reader = XmlReader.Create(search_url))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "item")
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                string el_id = el.Element("id").Value;

                                string title = el.Element("title").Value;

                                XElement el_image = el.Element("image");
                                string image = el_image.Element("url").Value;
                                
                                string enclosure = el.Element("enclosure").Attribute("url").Value;


                                hypster_tv_DAL.AolSeedVideo video = new hypster_tv_DAL.AolSeedVideo();
                                video.id = el_id;
                                video.title = title;
                                video.image = image;
                                video.enclosure = enclosure;

                                videos_list.Add(video);

                                if (videos_list.Count == 5)
                                {
                                    return View(videos_list);
                                }
                            }
                        }
                    }
                }

            }



            return View(videos_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++







        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult getSeedsNews()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string id = "";
            if (Request.QueryString["qq"] != null)
            {
                id = Request.QueryString["qq"].ToString();
            }

            //for href
            ViewBag.ss = id.Replace(' ', '+');


            string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=Music&sid=1692";
            using (XmlReader reader = XmlReader.Create(search_url))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "item")
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                string el_id = el.Element("id").Value;

                                string title = el.Element("title").Value;

                                XElement el_image = el.Element("image");
                                string image = el_image.Element("url").Value;

                                string enclosure = el.Element("enclosure").Attribute("url").Value;


                                hypster_tv_DAL.AolSeedVideo video = new hypster_tv_DAL.AolSeedVideo();
                                video.id = el_id;
                                video.title = title;
                                video.image = image;
                                video.enclosure = enclosure;

                                videos_list.Add(video);

                                if (videos_list.Count == 3)
                                {
                                    return View(videos_list);
                                }
                            }
                        }
                    }
                }

            }



            return View(videos_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



       


    }
}
