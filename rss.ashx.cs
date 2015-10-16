using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.ServiceModel;
using System.ServiceModel.Syndication;


namespace Hypster
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    public class rss : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var Response = context.Response;

            // Prepare response
            Response.Buffer = false;
            Response.Clear();
            Response.ContentType = "application/rss+xml";
            Response.Cache.SetExpires(DateTime.Now.AddMinutes(10));
            Response.Cache.SetCacheability(HttpCacheability.Public);



            // Create an XmlWriter to write the feed into it
            using (XmlWriter writer = XmlWriter.Create(Response.OutputStream))
            {
                // Set the feed properties
                SyndicationFeed feed = new SyndicationFeed("hypster.com", "Breaking news from hypster", new Uri("http://hypster.com"));

                // Add authors
                feed.Authors.Add(new SyndicationPerson("info@hypster.com", "Mister Zergo", "http://hypster.com"));

                // Add categories
                //NewsType[] categories = (NewsType[])Enum.GetValues(typeof(NewsType)); // NewsType is a enum I use, which is custom created
                //hypster_tv_DAL.newsPost[] categories = (hypster_tv_DAL.newsPost[])Enum.GetValues(typeof(hypster_tv_DAL.newsPost));
                /*foreach (var category in categories)
                {
                    feed.Categories.Add(new SyndicationCategory(category.GetDescription()));
                }*/



                // Set copyright
                feed.Copyright = new TextSyndicationContent("© Copyright 2014 Baronsmedia");

                // Set generator
                feed.Generator = "hypster.com";

                // Set language
                feed.Language = "en-US";

                // Add post items
                List<SyndicationItem> items = new List<SyndicationItem>();

                hypster_tv_DAL.newsManagement newsManager = new hypster_tv_DAL.newsManagement();

                List<hypster_tv_DAL.newsPost> newsList = new List<hypster_tv_DAL.newsPost>();
                newsList = newsManager.GetLatestNews_cache(15);


                //var newsList = News.GetLatest(20);
                foreach (var news in newsList)
                {
                    string url = "http://hypster.com/breaking/details/" + news.post_guid;
                    SyndicationItem item = new SyndicationItem();
                    item.Id = news.post_id.ToString();
                    item.Title = TextSyndicationContent.CreatePlaintextContent(news.post_title);
                    item.Content = SyndicationContent.CreateXhtmlContent(news.post_content);
                    item.PublishDate = news.post_date.Value;
                    //item.Categories.Add(new SyndicationCategory(news.NewsType.Value.GetDescription()));
                    item.Links.Add(new SyndicationLink(new Uri(url), "alternate", news.post_title, "text/html", 1000));
                    items.Add(item);
                }
                feed.Items = items;



                // Write the feed to output
                Rss20FeedFormatter formatter = new Rss20FeedFormatter(feed);
                formatter.WriteTo(writer);

                writer.Flush();
            }


            Response.End();
        }






        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
