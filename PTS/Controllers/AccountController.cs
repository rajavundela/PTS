﻿using System;
using System.Collections.Generic;
using System.Data;
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
            if(Session["Username"] == null)
            {
                TempData["Message"] = "You must login before accessing this page.";
                return RedirectToAction("LoginRegister", "Account");
            }

            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select emailId, datecreated from userdetails where username=@0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@0", Session["Username"].ToString());
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ViewBag.Email = reader["EmailId"].ToString();
                    ViewBag.DateCreated = reader["DateCreated"].ToString();
                }
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
                string query = "SELECT userName, emailId FROM userDetails WHERE userName=@0";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@0", RegisterUsername);
                // Executing query
                SqlDataReader sdr = cmd.ExecuteReader();

                bool isExist = sdr.Read();//reads next record (returns false if there is no record to read)
                sdr.Close();
                if (isExist)
                {
                    //Error message: username already exists
                    TempData["Message"] = "Username already exists. Enter a different one.";
                    con.Close();
                    return View();
                }

                else
                {   
                    //insert data into database
                    SqlCommand cmd2 = new SqlCommand("InsertUserDetails", con);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.Add(new SqlParameter("@UserName", RegisterUsername));
                    cmd2.Parameters.Add(new SqlParameter("@Email", RegisterEmail));
                    cmd2.Parameters.Add(new SqlParameter("@Password", RegisterPassword));
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

                string query = "SELECT userName, password, Role_Id FROM userDetails WHERE userName=@0";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@0", LoginUsername);
                // Executing query 
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    if (sdr["userName"].ToString() == LoginUsername && sdr["password"].ToString() == LoginPassword)
                    {
                        //create session here
                        Session["Username"] = LoginUsername;
                        Session["UserType"] = sdr["Role_Id"].ToString();

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

        [HttpGet]
        public ActionResult RoleChange()
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "You must login before accessing this page.";
                return RedirectToAction("LoginRegister", "Account");
            }
            if (!Session["UserType"].Equals("2")) // not admin
            {
                TempData["Message"] = "You do not have rights to modify data. Please contact an Admin.";
                return Redirect("/Home/Index");
            }

            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create the command and set its properties.
                SqlCommand cmd = new SqlCommand("select role from rolemaster", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var roles = new List<string>();
                while (reader.Read())
                {
                    roles.Add(reader["role"].ToString());
                }
                ViewBag.Roles = roles;
            }

            return View();
        }

        [HttpPost]
        public ActionResult RoleChange(FormCollection values)
        {
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Admin_Role_Change", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@role", values["role"]));
                cmd.Parameters.Add(new SqlParameter("@email", values["email"]));
                con.Open();
                if (cmd.ExecuteNonQuery() == 0)
                {
                    TempData["Message"] = "Invalid email address";
                }
                else
                {
                    TempData["Message"] = "Access rights changed successfully.";
                }

            }
            return Redirect("/Account/RoleChange");
        }

    }
}