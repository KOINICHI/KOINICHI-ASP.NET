using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using LinqToTwitter;

namespace MvcApplication1.Controllers
{
    public class TweetsController : Controller
    {
        private TweetsDBContext db = new TweetsDBContext();

        //
        // GET: /Tweets/

        public ActionResult Index()
        {
            return View(db.Tweets.ToList());
        }


        //
        // GET: /Tweets/Load/

        public ActionResult Load()
        {
            var auth = new MvcAuthorizer
            {
                CredentialStore = new SessionStateCredentialStore()
            };

            if (!auth.CredentialStore.HasAllCredentials()) {
                auth.CredentialStore = new SessionStateCredentialStore
                {
                    ConsumerKey = ConfigurationManager.AppSettings["consumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"]
                };

            }

            var ctx = new TwitterContext(auth);

            var statusTweets =
                (from tweet in ctx.Status
                where tweet.Type == StatusType.User &&
                      tweet.ScreenName == "KOINICHI" &&
                      tweet.Count == 200
                select tweet).ToList();

            string sqlcmd = @"insert into TweetsDB values (@value1, @value2, @value3)";
            SqlCeConnection conn = new SqlCeConnection(db.Database.Connection.ConnectionString);
            foreach (var tweet in statusTweets) {
                SqlCeCommand cmd = new SqlCeCommand(sqlcmd, conn);
                cmd.Parameters.AddWithValue("@value1", tweet.StatusID);
                cmd.Parameters.AddWithValue("@value2", tweet.Text);
                cmd.Parameters.AddWithValue("@value3", tweet.CreatedAt);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Tweets/Details/5

        public ActionResult Details(string id = null)
        {
            TweetsDB tweetsdb = db.Tweets.Find(id);
            if (tweetsdb == null)
            {
                return HttpNotFound();
            }
            return View(tweetsdb);
        }

        //
        // GET: /Tweets/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Tweets/Create

        [HttpPost]
        public ActionResult Create(TweetsDB tweetsdb)
        {
            if (ModelState.IsValid)
            {
                db.Tweets.Add(tweetsdb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tweetsdb);
        }

        //
        // GET: /Tweets/Edit/5

        public ActionResult Edit(string id = null)
        {
            TweetsDB tweetsdb = db.Tweets.Find(id);
            if (tweetsdb == null)
            {
                return HttpNotFound();
            }
            return View(tweetsdb);
        }

        //
        // POST: /Tweets/Edit/5

        [HttpPost]
        public ActionResult Edit(TweetsDB tweetsdb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tweetsdb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tweetsdb);
        }

        //
        // GET: /Tweets/Delete/5

        public ActionResult Delete(string id = null)
        {
            TweetsDB tweetsdb = db.Tweets.Find(id);
            if (tweetsdb == null)
            {
                return HttpNotFound();
            }
            return View(tweetsdb);
        }

        //
        // POST: /Tweets/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            TweetsDB tweetsdb = db.Tweets.Find(id);
            db.Tweets.Remove(tweetsdb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}