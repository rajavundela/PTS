using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace PTS.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "You must login before accessing this page.";
                return RedirectToAction("LoginRegister", "Account");
            }

            return View();
        }


        public ActionResult LoginRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginRegister(string LoginUsername, string LoginPassword, string RegisterUsername, string RegisterEmail, string RegisterPassword)
        {
            //creating connection for infotech group7 databse
            //SqlConnection con = new SqlConnection("server=172.19.2.52;user id=group7;password=group7;database=group7");
            SqlConnection con = new SqlConnection("server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts");


            //Register
            if (RegisterUsername != null)
            {
                ViewBag.RegisterUsername = RegisterUsername;
                ViewBag.Email = RegisterEmail;
                con.Open();
                string query = $"SELECT userName, emailId FROM userDetails WHERE userName='{RegisterUsername}'";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                // Executing query
                SqlDataReader sdr = cmd.ExecuteReader();

                bool isExist = sdr.Read();//reads next record (returns false if there is no record to read)
                con.Close();
                if (isExist)
                {
                    //Error message: username already exists
                    TempData["Message"] = "Username already exists. Enter a different one.";
                    return View();
                }

                else
                {
                    //insert data into database
                    con.Open();
                    query = $"insert into userDetails(userName, emailId, password, dateCreated) values('{RegisterUsername}','{RegisterEmail}','{RegisterPassword}','{DateTime.Now.ToString("yyy-MM-dd hh:mm:ss")}')";
                    SqlCommand cmd2 = new SqlCommand(query);
                    cmd2.Connection = con;
                    try
                    {
                        cmd2.ExecuteNonQuery();
                        TempData["Message"] = $"Account is created successfully with username {RegisterUsername}.";
                    }

                    catch (SqlException ex)
                    {
                        TempData["Message"] = "An account with that email already exists.";
                    }
                    con.Close();

                    //send verication link

                    return View();
                }

            }

            //Login
            else
            {
                //check if the LoginUsername and LoginPassword are valid and create a session
                ViewBag.LoginUsername = LoginUsername;
                con.Open();

                string query = $"SELECT userName, password FROM userDetails WHERE userName='{LoginUsername}'";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                // Executing query 
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    if (sdr["userName"].ToString() == LoginUsername && sdr["password"].ToString() == LoginPassword)
                    {
                        //create session here
                        //
                        Session["Username"] = LoginUsername;

                        TempData["Message"] = $"Welcome {LoginUsername}..!";

                        con.Close();
                        return Redirect("~/Home/Index");
                    }

                    else
                    {
                        //Error: Prompt user that it is invalid password
                        TempData["Message"] = "Invalid Password.";
                        con.Close();
                        return View();
                    }
                }
                else
                {
                    //Error: Prompt User does not exist. Create an account now?
                    TempData["Message"] = "User does not exist. Please create an account now.";
                    con.Close();
                    return View();
                }

            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            TempData["Message"] = "You have been logged out. Looking forward to see you again.";
            return RedirectToAction("Index", "Home");
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