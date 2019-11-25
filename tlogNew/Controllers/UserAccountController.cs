using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using tlogNew.Models;
using System.Net.Http;



namespace tlogNew.Controllers
{
    public class UserAccountController : Controller
    {
        //Index
        public ActionResult Index()
        {
            if (Session["uid"] != null)
            {
                return RedirectToAction("UserProfile","UserAccount",new { uid= Session["uid"] });
            }
            else
            {
                return RedirectToAction("Login");
            }
        }



        //Register
        public ActionResult Register()
        {
            if (Session["uid"] != null)
            {
                return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            ViewBag.exists = "";
            if (Session["uid"] != null)
            {
                return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
            }
            using (Model1 db = new Model1())
            {
                var err = db.user.FirstOrDefault(u => u.username == user.username);
                if(err!=null)
                {
                    ViewBag.exists = "Username Already exists";
                    return View();
                }
                else
                {
                    db.user.Add(user);
                    db.SaveChanges();
                    Session["uid"] = user.id;
                    Session["uname"] = user.username;

                    nodemicro obj = new nodemicro()
                    {
                        title = user.username,
                        id = 0,
                        type = 1
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:3000/");
                        var postTask = client.PostAsJsonAsync<nodemicro>("getSearchBuf", obj);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
                        }
                        else
                        {
                            ViewBag.post = "Sorry for technical fault." +
                                "Try Again Later.";
                            return View();
                        }
                    }
                   
                }

            }

        }




        //Login 
        public ActionResult Login()
        {
            if (Session["uid"] != null)
            {
                return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (Session["uid"] != null)
            {
                return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
            }
            ViewBag.error = "";

            using (Model1 db = new Model1())
            {
                
                var usr = db.user.FirstOrDefault(u => u.username == user.username
                                            && u.password == user.password);
               
                if (usr != null)
                {
                    Session["uid"] = usr.id.ToString();
                    Session["uname"] = usr.username.ToString();
                    return RedirectToAction("UserProfile", "UserAccount", new { uid = usr.id });
                }
                else
                {
                    Console.Write("error");
                    ViewBag.error = "Incorrect Credentials";

                }
            }
            return View();
        }



        //Whenver any error occurs due to user manually changes in url
        public ActionResult Error()
        {
            return View();
        }



        //User profile render

        public ActionResult UserProfile(int? uid,string uname)
        { 
            if(!uid.HasValue)
            {
                return RedirectToAction("Login");
            }
           
            using (Model1 db = new Model1())
            {
                    User usr = db.user.FirstOrDefault(u => u.id == uid);
                        

                    
                    if(usr==null)
                    {
                        usr = db.user.FirstOrDefault(u => u.username == uname);
                        if(usr==null)
                         {
                        return RedirectToAction("Index");
                    }
       
                    }
                    List<Microblog> microblog = db.microblog.Where(u => u.uid== usr.id).OrderByDescending(x=>x.time).Take(40).ToList();
                    List<Megablog> megablog = db.megablog.Where(u => u.uid == usr.id).OrderByDescending(x => x.time).Take(40).ToList();

                    Both obj = new Both();

                    obj.user = usr;
                    obj.microblog = microblog;
                    obj.megablog = megablog;


                    return View(obj);

            }
     
        }



        //User Account Settings
        public ActionResult Settings()
        {
            ViewBag.update = "";
            if (Session["uid"]==null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();
                    User usr = db.user.FirstOrDefault(u => u.id.ToString() == id);
                    return View(usr);
                }

            }
        }
        [HttpPost]
        public ActionResult Settings(User user)
        {
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();

                    User usr = (from p in db.user
                                where p.id.ToString() == id
                                select p).SingleOrDefault();
                    if(user.bio!=usr.bio || user.username!=usr.username||user.email!=usr.email)
                    {
                        usr.username = user.username;
                        usr.email = user.email;
                        usr.bio = user.bio;
                        db.SaveChanges();
                        Session["uname"] = user.username;
                        ViewBag.update = "Updated Successfully";
                    }

  
                    return View(usr);
                }

            }
        }

        //Change Password
        public ActionResult ChangePassword()
        {
            ViewBag.update = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();
                    User usr = db.user.FirstOrDefault(u => u.id.ToString() == id);
                    return View(usr);
                }

            }
        }
        [HttpPost]
        public ActionResult ChangePassword(FormCollection Request)
        {
            ViewBag.old = "";
            ViewBag.update = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                
                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();

                    User usr = (from p in db.user
                                where p.id.ToString() == id
                                select p).SingleOrDefault();
                    if (Request["old"] !=usr.password)
                    {
                        ViewBag.old = "Old Password incorrect!";
                        return View(usr);
                    }
                    else
                    {
                        if(Request["new"]!=Request["confirm"])
                        {
                            ViewBag.match = "Password Dosen't Match!";
                            return View(usr);
                        }
                        else
                        {
                            usr.password = Request["new"];
                            db.SaveChanges();
                            ViewBag.update = "Password Changed Succefully.";
                            return View(usr);
                        }
                    }


                }

            }
        }


        //Delete Account
        public ActionResult Delete()
        {
            ViewBag.delete = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();
                    User usr = db.user.FirstOrDefault(u => u.id.ToString() == id);
                    return View(usr);
                }

            }
        }
        [HttpPost]
        public ActionResult Delete(FormCollection Request)
        {
            ViewBag.delete = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();

                    User usr = (from p in db.user
                                where p.id.ToString() == id
                                select p).SingleOrDefault();

                    if(usr.username!=Request["confirm"])
                    {
                        ViewBag.delete = "Incorrect Username.";
                        return View(usr);
                    }
                    else
                    {
                        var obj = db.user.Where(a => a.id.ToString() == id).Single();
                        db.user.Remove(obj);
                        db.SaveChanges();
                        Session.Clear();
                        return RedirectToAction("Index");
                    }

                }

            }
        }


        //Delete Blogs
        public ActionResult DeleteMicro(int? bid)
        {   
            ViewBag.delete = "";
            if (Session["uid"] == null || !bid.HasValue)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.bid = bid;
                return View();

            }
        }
        [HttpPost]
        public ActionResult DeleteMicro(FormCollection Request)
        {
            ViewBag.delete = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();

                    User usr = (from p in db.user
                                where p.id.ToString() == id
                                select p).FirstOrDefault();

                    if (usr.username != Request["confirm"])
                    {
                        ViewBag.delete = "Incorrect Username.";
                        return View(usr);
                    }
                    else
                    {
                        string ii = Request["bid"];
                        Microblog obj = db.microblog.Where(a => a.bid.ToString() == ii )
                            .FirstOrDefault();
                       

                        Tag tag = db.tag.Where(v => v.bid.ToString() == ii).FirstOrDefault();
                        if(tag==null)
                        {
                            Tag tag1 = db.tag.Where(v => v.title==obj.title).FirstOrDefault();
                            db.tag.Remove(tag1);
                        }
                        else
                        {
                            db.tag.Remove(tag);
                        }
                        
                        db.microblog.Remove(obj);
                        
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }

            }
        }


        public ActionResult DeleteMega(int? bid)
        {
            ViewBag.delete = "";
            if (Session["uid"] == null || !bid.HasValue)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.bid = bid;
                return View();

            }
        }
        [HttpPost]
        public ActionResult DeleteMega(FormCollection Request)
        {
            ViewBag.delete = "";
            if (Session["uid"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                using (Model1 db = new Model1())
                {
                    string id = Session["uid"].ToString();

                    User usr = (from p in db.user
                                where p.id.ToString() == id
                                select p).FirstOrDefault();

                    if (usr.username != Request["confirm"])
                    {
                        ViewBag.delete = "Incorrect Username.";
                        return View(usr);
                    }
                    else
                    {
                        string ii = Request["bid"];
                        Megablog obj = db.megablog.Where(a => a.bid.ToString() == ii)
                            .FirstOrDefault();
                        

                        Category cat = db.category.Where(v => v.bid.ToString() == ii).FirstOrDefault();
                        if (cat == null)
                        {
                            Category cat1 = db.category.Where(v => v.title == obj.title).FirstOrDefault();
                            db.category.Remove(cat1);
                        }
                        else
                        {
                            db.category.Remove(cat);
                        }


                        db.megablog.Remove(obj);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }

            }
        }



        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect(Request.UrlReferrer.PathAndQuery);
        }



        //Choose Blog type
        public ActionResult Choose()
        {
            if(Session["uid"]!=null)
            {
                using (Model1 db = new Model1())
                {
                    var obj = db.megablog.FirstOrDefault();
                    return View(obj);

                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }
        
            

        //Creating Micro blogs
        public ActionResult CreateMicroBlog()
        {
       
            if(Session["uid"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpPost]
        public ActionResult CreateMicroBlog(Microblog blog)
        {
            ViewBag.post = "";
            ViewBag.error = "";
            if (Session["uid"] != null)
            {
                
                try
                {
                    if (Regex.Replace(blog.text, @"<[^>]+>|&nbsp;", "").Trim().Length > 250)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Maximum 250 characters allowed in Microblog";
                        return View();
                    }
                    else if (Regex.Replace(blog.text, @"<[^>]+>|&nbsp;", "").Trim().Length < 10)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Minimum 10 characters required";
                        return View();
                    }
                    else
                    {
                        blog.tag = blog.tag.ToLower();
                        using (Model1 db = new Model1())
                        {
                            Microblog b = new Microblog();
                            b.uid = blog.uid;
                            b.uname = blog.uname;
                            b.time = blog.time;
                            b.text = blog.text;
                            b.title = blog.title;
                            b.reads = blog.reads;
                            b.tag = blog.tag;
                            db.microblog.Add(b);
                            db.SaveChanges();

                            Microblog newB = db.microblog.FirstOrDefault(x => x.title == blog.title);
                            Tag t = new Tag();
                            t.tag = blog.tag;
                            t.bid = newB.bid;
                            t.uid = blog.uid;
                            t.uname = blog.uname;
                            t.title = blog.title;
                            t.reads = blog.reads;
                            db.tag.Add(t);
                            db.SaveChanges();

                            nodemicro obj = new nodemicro() {
                                title = blog.title,
                                id=newB.bid,
                                type=2
                            };

                            using (HttpClient client = new HttpClient())
                            {
                                client.BaseAddress = new Uri("http://localhost:3000/");
                                var postTask = client.PostAsJsonAsync<nodemicro>("getSearchBuf", obj);
                                postTask.Wait();

                                var result = postTask.Result;
                                if (result.IsSuccessStatusCode)
                                {
                                    return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
                                }
                                else
                                {
                                    ViewBag.post = "Sorry for technical fault." +
                                        "Try Again Later.";
                                    return View();
                                }
                            }


                        }
                        

                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    ViewBag.error = "Sorry for technical fault." +
                                        "Try Again Later.";
                    return View();
                }

            }
            else
            {

                return RedirectToAction("Login");
            }
        }



        //Creating MegaBlogs
        public ActionResult CreateMegaBlog()
        {

            if (Session["uid"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }

        }

        [HttpPost]
        public ActionResult CreateMegaBlog(Microblog blog)
        {
            ViewBag.error = "";
            if (Session["uid"] != null)
            {

                try
                {
                    if (Regex.Replace(blog.text, @"<[^>]+>|&nbsp;", "").Trim().Length > 20000)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Maximum 20000 characters allowed in Megablog";
                        return View();
                    }
                    else if (Regex.Replace(blog.text, @"<[^>]+>|&nbsp;", "").Trim().Length < 250)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Minimum 250 characters required";
                        return View();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    ViewBag.error = "Minimum 250 character required";
                    return View();
                }
                using (Model1 db = new Model1())
                {
                    Megablog b = new Megablog();
                    b.uid = blog.uid;
                    b.uname = blog.uname;
                    b.time = blog.time;
                    b.text = blog.text;
                    b.title = blog.title;
                    b.reads = blog.reads;
                    b.category= blog.tag;
                    db.megablog.Add(b);
                    db.SaveChanges();

                    Megablog newB = db.megablog.FirstOrDefault(x => x.title == blog.title);
                    Category c = new Category();
                    c.tag = blog.tag;
                    c.bid = newB.bid;
                    c.uid = newB.uid;
                    c.uname = newB.uname;
                    c.title = newB.title;
                    c.reads = newB.reads;
                    db.category.Add(c);
                    db.SaveChanges();

                    nodemicro obj = new nodemicro()
                    {
                        title = blog.title,
                        id = newB.bid,
                        type = 3
                    };

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:3000/");
                        var postTask = client.PostAsJsonAsync<nodemicro>("getSearchBuf", obj);
                        postTask.Wait();

                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
                        }
                        else
                        {
                            ViewBag.post = "Sorry for technical fault." +
                                "Try Again Later.";
                            return View();
                        }
                    }
                }

                

            }
            else
            {

                return RedirectToAction("Login");
            }
        }


    }
}
