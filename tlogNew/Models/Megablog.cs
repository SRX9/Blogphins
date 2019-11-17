using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tlogNew.Models
{
    public class Megablog
    {
        [Key]
        public int bid { get; set; }

        [Required(ErrorMessage = "Required")]
        public int uid { get; set; }

        [Required(ErrorMessage = "Required")]
        public string uname { get; set; }

        [Required(ErrorMessage = "Required")]
        public string time { get; set; }

        [Required(ErrorMessage = "Required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Required")]
        [AllowHtml]
        public string text { get; set; }

        public int reads { get; set; }

        [Required(ErrorMessage = "Required")]
        public string category { get; set; }
    }
}