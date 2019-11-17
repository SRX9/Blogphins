using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tlogNew.Models
{
    public class Tag
    {
        [Key]
        public int id { get; set; }

        public string tag { get; set; }

        public int bid { get; set; }

        public int uid { get; set; }
        
        public string uname { get; set; }

        public string title { get; set; }

        public int reads { get; set; }
    }
}