using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tlogNew.Models
{
    public class Both
    {
        public User user { get; set; }
        public List<Microblog> microblog { get; set; }
        public List<Megablog> megablog { get; set; }
        public List<Save> save { get; set; }

    }
}