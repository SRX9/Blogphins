using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tlogNew.Models;
namespace tlogNew.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Microblog(string title, int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Milist m = new Milist();
            using (Model1 db = new Model1())
            {
                var blog = db.microblog.FirstOrDefault(b => b.bid == id);
                if(blog==null)
                {
                    return RedirectToAction("Index");
                }
                var list = db.tag.Where(t => t.tag == blog.tag).ToList();
                m.blog = blog;
                m.list = list;

                return View(m);
                
            }
        }


        public ActionResult Megablog(string title, int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            Melist m = new Melist();
            using (Model1 db = new Model1())
            {
                var blog = db.megablog.FirstOrDefault(b => b.bid == id);
                if (blog == null)
                {
                    return RedirectToAction("Index");
                }
                var list = db.category.Where(t => t.tag == blog.category).ToList();
                m.blog = blog;
                m.list = list;

                return View(m);

            }
        }
    }
}