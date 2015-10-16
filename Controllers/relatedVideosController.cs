using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace hypster.Controllers
{
    public class relatedVideosController : Controller
    {
        //
        // GET: /relatedVideos/



        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public ActionResult Index()
        {
            List<hypster_tv_DAL.AolSeedVideo> videos_list = new List<hypster_tv_DAL.AolSeedVideo>();


            string id = "";
            if (Request.QueryString["ss"] != null)
            {
                id = Request.QueryString["ss"].Replace('+', ' ');
            }

            string active_video = "";
            if (Request.QueryString["al"] != null)
            {
                active_video = Request.QueryString["al"];
            }




            string search_url = "http://api.5min.com/search/" + HttpUtility.UrlEncode(id) + "/videos.xml?category_id=Music&sid=1692";

            if (id == "love songs")
            {
                search_url = "http://api.5min.com/video/list/info.xml?video_group_id=159452&sid=1692";
            }

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
                                
                            }
                        }
                    }
                }

            }



            //select default video
            if (active_video != "")
            {
                foreach (var item in videos_list)
                {
                    if (item.id == active_video)
                    {
                        ViewBag.active_video = item.enclosure;
                    }
                }
            }
            else
            {
                if (videos_list.Count > 0)
                {
                    ViewBag.active_video = videos_list[0].enclosure;
                }
            }





            return View(videos_list);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++





    }
}
