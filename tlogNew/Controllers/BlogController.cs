using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tlogNew.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
           // @Html.Raw(@WebUtility.HtmlDecode(Model.text))
            return View();
        }
    }
}