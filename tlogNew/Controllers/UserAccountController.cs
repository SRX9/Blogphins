using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tlogNew.Models;

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
                    return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });
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
        public ActionResult UserProfile(int uid)
        { 
            using (Model1 db = new Model1())
            {
                try
                {
                    User usr = db.user.FirstOrDefault(u => u.id == uid);
                    if(usr==null)
                    {
                        return RedirectToAction("Error");
                    }
                    List<Microblog> microblog = db.microblog.Where(u => u.uid == uid).ToList();
                    List<Megablog> megablog = db.megablog.Where(u => u.uid == uid).ToList();
                    List<Save> save = db.save.Where(u => u.uid == uid).ToList();

                    Both obj = new Both();

                    obj.user = usr;
                    obj.microblog = microblog;
                    obj.megablog = megablog;
                    obj.save = save;


                    return View(obj);
                }
                catch(Exception e)
                {
                    return RedirectToAction("Error");
                }

            }
     
        }



        //Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
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
            ViewBag.error = "";
            if (Session["uid"] != null)
            {
     
                try
                {
                    if (blog.text.ToString().Trim().Length > 250)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Maximum 250 characters allowed in Microblog";
                        return View();
                    }
                    else if (blog.text.ToString().Trim().Length < 10)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Minimum 10 characters required";
                        return View();
                    }
                    else
                    {
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
                        }
                        return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });

                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                    ViewBag.error = "Minimum 10 character required";
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
                    if (blog.text.ToString().Trim().Length > 20000)
                    {
                        ViewBag.display = "";
                        ViewBag.error = "Maximum 20000 characters allowed in Megablog";
                        return View();
                    }
                    else if (blog.text.ToString().Trim().Length < 250)
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
                }
                return RedirectToAction("UserProfile", "UserAccount", new { uid = Session["uid"] });

            }
            else
            {

                return RedirectToAction("Login");
            }
        }


        //Settings
        public ActionResult Settings()
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
    }
}
