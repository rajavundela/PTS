using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {   
            //insert login data into database

            return RedirectToRoute("Home/Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult LoginRegister()
        {
            return View();
        }

        [HttpPost]
        public void Register(string username, string password)
        {
            // insert register form data into database and send verification link
            // put a message saying registration success.
        }
        
        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Verify(string id)
        {   //if the id is correct put success, else no success
            //put sucess or no success flags in ViewBag
            return View();
        }
    }
}