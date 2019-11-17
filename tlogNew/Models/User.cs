using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace tlogNew.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string username { get; set; }

        [Required(ErrorMessage = "Required")]
        public string bio { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Invalid Email")]
        public string email { get; set; }

        public int readers { get; set; }


        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}