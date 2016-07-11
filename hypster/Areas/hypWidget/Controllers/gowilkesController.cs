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

namespace hypster.Areas.hypWidget.Controllers
{
    public class gowilkesController : Controller
    {
        //
        // GET: /hypWidget/gowilkes/


        [OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string category = "";
            if (Request.QueryString["category"] != null)
            {
                category = Request.QueryString["category"].ToString();
            }


            string id = "";
            if (Request.QueryString["qq"] != null)
            {
                id = Request.QueryString["qq"].ToString();
            }



            string headline = "";
            if (Request.QueryString["headline"] != null)
            {
                headline = Request.QueryString["headline"].ToString();

                ViewBag.headline = headline.Replace('+', ' ');
            }



            //for href
            ViewBag.ss = id.Replace(' ', '+');



            string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=" + category + "&sid=1692&sort=most_recent";


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

                                string p_pubDate = el.Element("pubDate").Value;

                                hypster_tv_DAL.AolSeedVideo video = new hypster_tv_DAL.AolSeedVideo();
                                video.id = el_id;
                                video.title = title;
                                video.image = image;
                                video.enclosure = enclosure;
                                video.pub_date = p_pubDate;


                                videos_list.Add(video);

                                if (videos_list.Count == 12)
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


        //[OutputCache(Duration = 60)]
        public ActionResult Video()
        {
            return (GetSplitFrames());
        }

        //[OutputCache(Duration = 60)]
        public ActionResult Thumbs()
        {
            return (GetSplitFrames());
        }



        public ActionResult GetSplitFrames()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string category = "";
            if (Request.QueryString["category"] != null)
            {
                category = Request.QueryString["category"].ToString();
            }


            string id = "";
            if (Request.QueryString["qq"] != null)
            {
                id = Request.QueryString["qq"].ToString();
            }



            string headline = "";
            if (Request.QueryString["headline"] != null)
            {
                headline = Request.QueryString["headline"].ToString();

                ViewBag.headline = headline.Replace('+', ' ');
            }


            if (Request.QueryString["apl"] != null)
            {
                ViewBag.apl = Request.QueryString["apl"].ToString();
            }


            //for href
            ViewBag.ss = id.Replace(' ', '+');






            System.Runtime.Caching.ObjectCache i_chache = System.Runtime.Caching.MemoryCache.Default;
            if (i_chache["videos.xml?category_id=" + category] != null)
            {
                videos_list = (List<hypster_tv_DAL.AolSeedVideo>)i_chache["videos.xml?category_id=" + category];
            }
            else
            {
                //---------------------------------------------------------------------------------------------
                string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=" + category + "&sid=1692&sort=most_recent";
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

                                    string p_pubDate = el.Element("pubDate").Value;

                                    hypster_tv_DAL.AolSeedVideo video = new hypster_tv_DAL.AolSeedVideo();
                                    video.id = el_id;
                                    video.title = title;
                                    video.image = image;
                                    video.enclosure = enclosure;
                                    video.pub_date = p_pubDate;


                                    videos_list.Add(video);

                                    if (videos_list.Count == 12)
                                    {
                                        return View(videos_list);
                                    }
                                }
                            }
                        }
                    }
                }
                //---------------------------------------------------------------------------------------------

                i_chache.Add("videos.xml?category_id=" + category, videos_list, DateTime.Now.AddSeconds(120));
            }



            return View(videos_list);
        }







        //[OutputCache(Duration = 60)]
        public ActionResult Test()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string category = "";
            if (Request.QueryString["category"] != null)
            {
                category = Request.QueryString["category"].ToString();
            }


            string id = "";
            if (Request.QueryString["qq"] != null)
            {
                id = Request.QueryString["qq"].ToString();
            }



            string headline = "";
            if (Request.QueryString["headline"] != null)
            {
                headline = Request.QueryString["headline"].ToString();

                ViewBag.headline = headline.Replace('+', ' ');
            }



            //for href
            ViewBag.ss = id.Replace(' ', '+');


            
            string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=" + category + "&sid=1692&sort=most_recent";


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

                                string p_pubDate = el.Element("pubDate").Value;

                                hypster_tv_DAL.AolSeedVideo video = new hypster_tv_DAL.AolSeedVideo();
                                video.id = el_id;
                                video.title = title;
                                video.image = image;
                                video.enclosure = enclosure;
                                video.pub_date = p_pubDate;


                                videos_list.Add(video);

                                if (videos_list.Count == 12)
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





    }
}
