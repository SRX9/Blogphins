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
                var list = db.tag.Where(y => y.tag == blog.tag).
                    OrderByDescending(x => x.id).Take(10).ToList();
                m.blog = blog;
                m.list = list;


                List<Trend> trends = new List<Trend>();


                var t = db.tag.Select(x => x).OrderByDescending(x=>x.id).Take(10000).
                    ToList();


                var taglist = t.Select(e => e.tag).Distinct().ToList();
                

                int c;
                for (int i=0;i<taglist.Count;i++)
                {
                    c = 0;
                    for(int j=i;j<t.Count;j++)
                    {
                        if(t[i].tag==t[j].tag)
                        {
                            c++;
                        }
                    }
                    Trend newt = new Trend();
                    newt.tag = t[i].tag;
                    newt.count = c;
                    trends.Add(newt);
                }

                m.tagtrend = trends.OrderByDescending(v=>v.count).ToList();
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
                var list = db.category.Where(t => t.tag == blog.category).OrderByDescending(x => x.id).Take(10).ToList();
                m.blog = blog;
                m.list = list;

                

                return View(m);

            }
        }
    }
}