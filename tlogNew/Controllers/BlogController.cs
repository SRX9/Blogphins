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

        public ActionResult Index()
        {
            return RedirectToAction("Browse","Home");
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
                if (blog == null)
                {
                    blog = db.microblog.FirstOrDefault(b => b.title == title);
                    if(blog==null)
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                if (Session[id.ToString()]!=null)
                {
                    string iid = Session[id.ToString()].ToString();
                    if (iid != id.ToString())
                    {
                        Microblog up = (from p in db.microblog
                                        where p.bid == id
                                        select p).FirstOrDefault();
                        if (up == null)
                        {
                            up = (from p in db.microblog
                                  where p.title == title
                                  select p).FirstOrDefault();
                            if (up == null)
                            {
                                return RedirectToAction("Index");
                            }

                        }
                        up.reads = up.reads + 1;


                        Tag tag= (from p in db.tag
                                  where p.bid == up.bid
                                  select p).FirstOrDefault();
                        if (tag == null)
                        {
                            tag = (from p in db.tag
                                   where p.title == up.title
                                   select p).FirstOrDefault();
                            if (tag == null)
                            {
                                return RedirectToAction("Index");
                            }

                        }
                        tag.reads = tag.reads+1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    Session[id.ToString()] = id;
                    Microblog up = (from p in db.microblog
                                    where p.bid == id
                                    select p).FirstOrDefault();
                    if (up == null)
                    {
                        up = (from p in db.microblog
                              where p.title == title
                              select p).FirstOrDefault();
                        if (up == null)
                        {
                            return RedirectToAction("Index");
                        }

                    }
                    up.reads = up.reads + 1;
                    
                    Tag tag = (from p in db.tag
                               where p.bid == up.bid
                               select p).FirstOrDefault();
                    if (tag == null)
                    {
                        tag = (from p in db.tag
                               where p.title == up.title
                               select p).FirstOrDefault();
                        if (tag == null)
                        {
                            return RedirectToAction("Index");
                        }

                    }
                    tag.reads = tag.reads+1;
                    db.SaveChanges();
                }


                var list = db.tag.Where(y => y.tag == blog.tag).
                    OrderByDescending(x => x.id).Take(10).ToList();

                m.blog = blog;
                m.list = list;



                List<Trend> trends = new List<Trend>();


                var t = db.tag.Select(x => x).OrderByDescending(x=>x.id).Take(10000).
                    ToList();

                var taglist = t.Select(p => p.tag).Distinct();
                
                

                int c;
                foreach(var tt in taglist)
                {
                    c = 0;
                    for(int j=0;j<t.Count;j++)
                    {
                        if(tt==t[j].tag)
                        {
                            c++;
                        }
                    }
                    Trend newt = new Trend();
                    newt.tag = tt;
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
                    blog = db.megablog.FirstOrDefault(b => b.title == title);
                    if (blog == null)
                    {
                        return RedirectToAction("Index");
                    }
                }
                if (Session[id.ToString()] != null)
                {
                    string iid = Session[id.ToString()].ToString();
                    if (iid != id.ToString())
                    {
                        Megablog up = (from p in db.megablog
                                       where p.bid == id
                                       select p).FirstOrDefault();
                        if (up == null)
                        {
                            up = db.megablog.FirstOrDefault(b => b.title == title);
                            if (up == null)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        up.reads = up.reads + 1;
                        db.SaveChanges();


                        Category cat = (from p in db.category
                                   where p.bid == up.bid
                                   select p).FirstOrDefault();
                        if (cat== null)
                        {
                            cat = db.category.FirstOrDefault(b => b.title == title);
                            if (cat == null)
                            {
                                return RedirectToAction("Index");
                            }
                        }
                        cat.reads = cat.reads + 1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    Session[id.ToString()] = id;
                    Megablog up = (from p in db.megablog
                                   where p.bid == id
                                   select p).FirstOrDefault();
                    if (up == null)
                    {
                        up = db.megablog.FirstOrDefault(b => b.title == title);
                        if (up == null)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    up.reads = up.reads + 1;
                    db.SaveChanges();


                    Category cat = (from p in db.category
                                    where p.bid == up.bid
                                    select p).FirstOrDefault();
                    if (cat == null)
                    {
                        cat = db.category.FirstOrDefault(b => b.title == title);
                        if (cat == null)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    cat.reads = cat.reads + 1;
                    db.SaveChanges();
                }




                var list = db.category.Where(t => t.tag == blog.category).OrderByDescending(x => x.id).Take(10).ToList();
                m.blog = blog;
                m.list = list;

                

                return View(m);

            }
        }
    }
}