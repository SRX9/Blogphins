using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tlogNew.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            if(Session["admin"]!=null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }
        }

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(FormCollection req)
        {
            if(req["uname"]=="mark" && req["pass"]=="hanumanrajsrk")
            {
                Session["admin"] = "yippy";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Incorrect Credentials";
                return View();
            }
            
        }

        public ActionResult LogoutAdmin()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}