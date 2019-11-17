using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tlogNew.Models
{
    public class Microblog
    {
        [Key]
        public int bid { get; set; }

        public int uid { get; set; }

 
        public string uname { get; set; }


        public string time { get; set; }


        public string title { get; set; }

        [AllowHtml]
        public string text { get; set; }

        public int reads { get; set; }


        public string tag { get; set; }
    }
}