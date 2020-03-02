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
            //creating connection for infotech group7 databse
            //SqlConnection con = new SqlConnection("server=172.19.2.52;user id=group7;password=group7;database=group7");
            SqlConnection con = new SqlConnection("server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts");


            //Register
            if (RegisterUsername != null)
            {   

                
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
                    return View();
                }

                else
                {   
                    //insert data into database
                    con.Open();
                    query = $"insert into userDetails(userName, emailId, password) values('{RegisterUsername}','{RegisterEmail}','{RegisterPassword}')";
                    SqlCommand cmd2 = new SqlCommand(query);
                    cmd2.Connection = con;
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    //send verication link
                    //
                    return RedirectToAction("RegisterSuccess");
                }
                
            }

            //Login
            else
            {
                //check if the LoginUsername and LoginPassword are valid and create a session
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
                        con.Close();
                        return Redirect("~/Home/Index");
                    }

                    else
                    {
                        //Error: Prompt user that it is invalid password
                        con.Close();
                        return View();
                    }
                }
                else
                {
                    //Error: Prompt User does not exist. Create an account now?
                    con.Close();
                    return View();
                }
                
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