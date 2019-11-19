using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tlogNew.Models
{
    public class Milist
    {
        public Microblog blog { get; set; }
        public List<Tag> list { get; set; }
    }
}