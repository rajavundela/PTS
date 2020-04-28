using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetRecentlyAdded", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var recentlyAdded = new List<List<string>>();
                while (reader.Read())
                {
                    recentlyAdded.Add(new List<string>() {
                        reader["Location"].ToString(),
                        reader["FamilyName"].ToString(),
                        reader["Botonical_Name"].ToString(),
                        reader["Variety_Name"].ToString(),
                        reader["UniquePlant_Id"].ToString(),
                        reader["img1"].ToString(),
                        reader["DateOfPlanting"].ToString(),
                        reader["LocEntryBy"].ToString()
                    });
                }
                ViewBag.RecentlyAdded = recentlyAdded;
            }
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
    }
}