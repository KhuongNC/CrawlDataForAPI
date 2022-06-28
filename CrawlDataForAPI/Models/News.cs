using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrawlDataForAPI.Models
{
    public class News
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string TypeOfNews { get; set; }
        public string PostDate { get; set; }
    }
}