using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;// This data provider need to make connection with SQL Server
//Connection object for SQL Server is Sqlconnection

namespace PTS.Controllers
{
    public class PlantController : Controller
    {
        // GET: Plant
        public ActionResult Index()
        {   
            //creating connection
            SqlConnection con = new SqlConnection("server=172.19.2.52;user id=group7;password=group7;database=group7");

            //open connection if state is closed
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            //writing sql queries
            SqlCommand cmd1 = new SqlCommand("insert into sample values('raja reddy')");

            //set connection object to command
            cmd1.Connection = con;

            //Execute SQL Query
            cmd1.ExecuteNonQuery();

            //close connection
            con.Close();
            return View();
        }
    }
}