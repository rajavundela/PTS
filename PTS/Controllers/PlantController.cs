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

        public ActionResult Search(string searchString)
        {
            var searchResults = new List<List<string>>(); // 2d list
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Create the command and set its properties.
                SqlCommand cmd = new SqlCommand($"exec sp_Search '{searchString}'", con);
                //cmd.Connection = con;
                //cmd.CommandType = commandType.StoredProcedure;

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var record = new List<string>() { reader["Botonical_Name"].ToString(),
                                                          reader["Common_Name"].ToString(),
                                                          reader["FamilyName"].ToString(),
                                                          reader["Variety_Name"].ToString(),                                                          
                                                          reader["UniquePlant_Id"].ToString(),
                                                          reader["Location"].ToString(),
                                                          reader["DateOfPlanting"].ToString()
                                                         };
                        searchResults.Add(record);
                    }
                }
                else
                {
                    TempData["Message"] = "No Search data found. Please enter different keywords.";
                }
                reader.Close();
                ViewBag.SearchString = searchString;
                ViewBag.SearchResults = searchResults;
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            ViewBag.Id = id;
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"EXEC sp_Details_Search {id}", con);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> data = new List<string>();
                if (reader.Read())
                {
                    ViewBag.FamilyName = reader["FamilyName"].ToString();
                    ViewBag.FamilyCommonName = reader["FamilyCommonName"].ToString();
                    ViewBag.Habitat = reader["Habitat"].ToString();
                    ViewBag.CommonName = reader["Common_Name"].ToString();
                    ViewBag.BotanicalName = reader["Botonical_Name"].ToString();
                    ViewBag.ChromosomeNo = reader["Chromosome_No"].ToString();
                    ViewBag.Genus = reader["Genus"].ToString();
                    ViewBag.Species = reader["Species"].ToString();
                    ViewBag.Uses = reader["Uses"].ToString();
                    ViewBag.MedicalBenefit = reader["Medical_Benefit"].ToString();
                    ViewBag.HealthHazard = reader["Health_Hazard"].ToString();
                    ViewBag.VarietyName = reader["Variety_Name"].ToString();
                    ViewBag.Nature = reader["Nature"].ToString();
                    ViewBag.TimeOfSetting = reader["TimeOfSetting"].ToString();
                    ViewBag.TimeOfFlowering = reader["TimeOfFlowering"].ToString();
                    ViewBag.RotationPeriod = reader["Rotation_Period"].ToString();
                    ViewBag.PropagationMethod = reader["Propagation_Method"].ToString();
                    ViewBag.TreeHeight = reader["Tree_height"].ToString();
                    ViewBag.TrunkColor = reader["Trunk_Color"].ToString();
                    ViewBag.TreeForm = reader["Tree_Form"].ToString();
                    ViewBag.LeafShape = reader["Leaf_Shape"].ToString();
                    ViewBag.Fragrance = reader["Fragrance"].ToString();
                    ViewBag.WoodCharacter = reader["Wood_Character"].ToString();
                    ViewBag.FruitType = reader["Fruit_Type"].ToString();
                    ViewBag.BarkColor = reader["Bark_Color"].ToString();
                    ViewBag.BarkTexture = reader["Bark_texture"].ToString();
                    ViewBag.LitterType = reader["Litter_Type"].ToString();
                    ViewBag.Longevity = reader["Longevity"].ToString();
                    ViewBag.GrowingCondition = reader["Growing_Condition"].ToString();
                    ViewBag.DateOfPlanting = reader["DateOfPlanting"].ToString();
                    ViewBag.LocationOfPlant = reader["Location"].ToString();
                }
                else
                {
                    TempData["Message"] = "Something went wrong.Try again later";
                }
                reader.Close();
                ViewBag.DetailsData = data;
            }

            //var reader = cmd.ExecuteReader();

            //var columns = new List<string>();

            //for (int i = 0; i < reader.FieldCount; i++)
            //{
            //    columns.Add(reader.GetName(i));
            //}
            return View();
        }
    }
}