using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


        public ActionResult LoginRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginRegister(string LoginUsername, string LoginPassword, string RegisterUsername, string RegisterEmail, string RegisterPassword)
        {
            //creating connection
            SqlConnection con = new SqlConnection("server=172.19.2.52;user id=group7;password=group7;database=group7");

            //open connection if state is closed
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            //Register
            if (RegisterUsername != null)
            {
                // 1.Check if the RegisterUsername already exists in the database
                // if exists prompt him to change username
                //else
                //insert data into database and send verification link

                //SqlCommand cmd = new SqlCommand("");
                //cmd.Connection = con;
                //cmd.ExecuteNonQuery();

                con.Close();
                return RedirectToAction("RegisterSuccess");
            }

            //Login
            else
            {
                //check if the LoginUsername and LoginPassword are valid and create a session
                //SqlCommand cmd = new SqlCommand("");
                //cmd.Connection = con;
                //cmd.ExecuteNonQuery();
                con.Close();
                return Redirect("~/Home/Index");
            }



        }

        
        public ActionResult RegisterSuccess(string username, string password)
        {
            // put a message saying registration success.
            return View();
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