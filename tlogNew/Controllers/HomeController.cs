using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using tlogNew.Models;

namespace tlogNew.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Browse()
        {
            Browse b = new Browse();

            Latlist catlist = new Latlist();
            List<megacat> list = new List<megacat>();


            list.Add(new megacat() { cat = "DIY", link = "~/Content/images/diy.png" });
            list.Add(new megacat() { cat = "Food", link = "~/Content/images/food.png" });
            
            list.Add(new megacat() { cat = "Sports", link = "~/Content/images/sp.png" });
            list.Add(new megacat() { cat = "Health & Fitness", link = "~/Content/images/hf.png" });
            list.Add(new megacat() { cat = "Business/Finance", link = "~/Content/images/bf.png" });
            list.Add(new megacat() { cat = "Industry", link = "~/Content/images/in.png" });
            list.Add(new megacat() { cat = "Political", link = "~/Content/images/p.png" });
            list.Add(new megacat() { cat = "Books", link = "~/Content/images/boo.png" });
            list.Add(new megacat() { cat = "Current Events", link = "~/Content/images/ce.png" });
            list.Add(new megacat() { cat = "Entertainment", link = "~/Content/images/et.png" });
            list.Add(new megacat() { cat = "Music", link = "~/Content/images/mu.png" });
            list.Add(new megacat() { cat = "Celebrities", link = "~/Content/images/cel.png" });
            list.Add(new megacat() { cat = "Problem & Solution", link = "~/Content/images/ps.png" });
            list.Add(new megacat() { cat = "Artist", link = "~/Content/images/ar.png" });
            list.Add(new megacat() { cat = "Infographics", link = "~/Content/images/i.png" });
            list.Add(new megacat() { cat = "Reviews", link = "~/Content/images/rev.png" });
            list.Add(new megacat() { cat = "Comparison", link = "~/Content/images/co.png" });
            list.Add(new megacat() { cat = "Personal", link = "~/Content/images/per.png" });
            list.Add(new megacat() { cat = "Games", link = "~/Content/images/gam.png" });
            list.Add(new megacat() { cat = "Movies", link = "~/Content/images/mov.png" });
            list.Add(new megacat() { cat = "Funny", link = "~/Content/images/fun.png" });
            list.Add(new megacat() { cat = "Inspiration", link = "~/Content/images/ins.png" });
            list.Add(new megacat() { cat = "Engineering", link = "~/Content/images/eng.png" });
            list.Add(new megacat() { cat = "Computer", link = "~/Content/images/com.png" });
            list.Add(new megacat() { cat = "Tutorials/Guide", link = "~/Content/images/tg.png" });
            list.Add(new megacat() { cat = "Information & Technology", link = "~/Content/images/it.png" });
            list.Add(new megacat() { cat = "Fashion", link = "~/Content/images/f.png" });
            list.Add(new megacat() { cat = "Lifestyle", link = "~/Content/images/ls.png" });
            catlist.catlist = list;


            b.catlist = catlist;
            Milist m = new Milist();
            using (Model1 db = new Model1())
            {
                List<Trend> trends = new List<Trend>();


                var t = db.tag.Select(x => x).OrderByDescending(x => x.id).Take(10000).
                    ToList();


                var taglist = t.Select(e => e.tag).Distinct().ToList();


                int c;
                for (int i = 0; i < taglist.Count; i++)
                {
                    c = 0;
                    for (int j = i; j < t.Count; j++)
                    {
                        if (t[i].tag == t[j].tag)
                        {
                            c++;
                        }
                    }
                    Trend newt = new Trend();
                    newt.tag = t[i].tag;
                    newt.count = c;
                    trends.Add(newt);
                }
                m.tagtrend = trends.OrderByDescending(v => v.count).Take(50).ToList();

                b.milist = m;
                return View(b);

            }
        }

        public ActionResult Trends()
        {
            using (Model1 db = new Model1())
            {
                List<Trend> trends = new List<Trend>();


                var t = db.tag.Select(x => x).OrderByDescending(x => x.id).Take(10000).
                    ToList();


                var taglist = t.Select(e => e.tag).Distinct().ToList();


                int c;
                for (int i = 0; i < taglist.Count; i++)
                {
                    c = 0;
                    for (int j = i; j < t.Count; j++)
                    {
                        if (t[i].tag == t[j].tag)
                        {
                            c++;
                        }
                    }
                    Trend newt = new Trend();
                    newt.tag = t[i].tag;
                    newt.count = c;
                    trends.Add(newt);
                }
                trends = trends.OrderByDescending(v => v.count).Take(50).ToList();

                return View(trends);

            }
        }
        public ActionResult Tags(string tag)
        {
            Taglist t = new Taglist();
            ViewBag.count = 0;
            using (Model1 db = new Model1())
            {
                var taglist = db.tag.Where(q => q.tag == tag).Take(50).ToList();
                t.taglist = taglist;
                ViewBag.count = t.taglist.Count;
                ViewBag.tagname = tag;
                return View(t);
            }
            

        }

        public ActionResult CategoryAll(string cat)
        {
            BrowseCat catlist = new BrowseCat();
            ViewBag.count = 0;
            ViewBag.cat = cat;
            using (Model1 db = new Model1())
            {
                var clist = db.category.Where(q => q.tag == cat).Take(50).ToList();
                catlist.cats = clist;
                ViewBag.count = clist.Count;
              
                return View(catlist);
            }

           
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection Req)
        {
            IEnumerable<nodemicro> ans = null;
            ViewBag.q = Req["q"];
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:3000/");

                    var responseTask = client.GetAsync("search?q=" + Req["q"].ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<nodemicro>>();
                        readTask.Wait();
                        ans = readTask.Result;
                        int c = ans.ToList().Count;
                        if (c == 1)
                        {
                            ViewBag.result = "Total " + c + " Result Found for ' " + Req["q"] + " '";
                            return View(ans);
                        }
                        else
                        {
                            ViewBag.result = "Total " + c + " Results Found for  ' " + Req["q"] + " '";
                            return View(ans);
                        }

                    }
                    else
                    {

                        ans = Enumerable.Empty<nodemicro>();
                        return View(ans);

                    }
                }
            }
            catch(Exception e)
            {
                ans = Enumerable.Empty<nodemicro>();
                return View(ans);
            }


        }
    }
}