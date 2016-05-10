using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using LinqToTwitter;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        private TweetsDBContext tweetdb = new TweetsDBContext();

        public ActionResult Index()
        {
            string sqlcmd = @"select TOP(1) * from TweetsDB order by NEWID()";
            var tweet = tweetdb.Database.SqlQuery<TweetsDB>(sqlcmd).ToList<TweetsDB>().FirstOrDefault();
            return View(tweet);
        }
    }
}
