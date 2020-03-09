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
            return View();
        }
    }
}