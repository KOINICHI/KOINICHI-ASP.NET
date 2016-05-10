using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class TweetsAPIController : ApiController
    {
        private TweetsDBContext db = new TweetsDBContext();

        // GET api/tweetsapi
        public TweetsDB GetRandomTweet()
        {
            string sqlcmd = @"select TOP(1) * from TweetsDB order by NEWID()";
            var tweet = db.Database.SqlQuery<TweetsDB>(sqlcmd).ToList<TweetsDB>().FirstOrDefault();
            return tweet;
        }

        // GET api/tweetsapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/tweetsapi
        public void Post([FromBody]string value)
        {
        }

        // PUT api/tweetsapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/tweetsapi/5
        public void Delete(int id)
        {
        }
    }
}
