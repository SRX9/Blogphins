using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tlogNew.Models
{
    public class Melist
    {
        public Megablog blog { get; set; }
        public List<Category> list { get; set; }
        public List<Tag> tagtrend { get; set; }
    }
}