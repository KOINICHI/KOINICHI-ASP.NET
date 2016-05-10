using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data.Entity;

namespace MvcApplication1.Models
{
    public class TweetsDB
    {
        [DisplayName("ID")]
        public string ID { get; set; }

        [DisplayName("Tweet")]
        public string Tweet { get; set; }

        [DisplayName("Date")]
        public DateTime Date { get; set; }
    }

    public class TweetsDBContext : DbContext
    {
        public DbSet<TweetsDB> Tweets { get; set; }
    }
}