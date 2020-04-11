using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
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
                SqlCommand cmd = new SqlCommand("sp_Search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Search", searchString));

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if(reader["UniquePlant_Id"].ToString() == "")
                        {
                            continue;
                        }
                        else
                        {
                            var record = new List<string>() {
                                                          reader["Botonical_Name"].ToString(),
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
                }
                if(searchResults.Count == 0)
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
                SqlCommand cmd = new SqlCommand("sp_Details_Search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@uid", id));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
            }

            //var reader = cmd.ExecuteReader();

            //var columns = new List<string>();

            //for (int i = 0; i < reader.FieldCount; i++)
            //{
            //    columns.Add(reader.GetName(i));
            //}
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Username"] == null)
            {
                TempData["Message"] = "You must login before accessing this page.";
                return RedirectToAction("LoginRegister", "Account");
            }
            if (!Session["UserType"].Equals("2")) // not admin
            {
                TempData["Message"] = "You do not have rights to modify data. Please contact an Admin.";
                return Redirect($"/Plant/Details/{id}");
            }

            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Details_Search", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@uid", id));

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
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
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection values)
        {
            if (!Session["UserType"].Equals("2")) // not admin
            {
                TempData["Message"] = "You do not have rights to modify data. Please contact an Admin.";
                return Redirect($"/Plant/Details/{id}");
            }

            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Update_Admin_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FamilyName", values["FamilyName"]));
                cmd.Parameters.Add(new SqlParameter("@FamilyCommonName", values["FamilyCommonName"]));
                cmd.Parameters.Add(new SqlParameter("@Habitat", values["Habitat"]));
                cmd.Parameters.Add(new SqlParameter("@Common_Name", values["CommonName"]));
                cmd.Parameters.Add(new SqlParameter("@Botonical_Name", values["BotanicalName"]));
                cmd.Parameters.Add(new SqlParameter("@Chromosome_No", values["ChromosomeNo"]));
                cmd.Parameters.Add(new SqlParameter("@Genus", values["Genus"]));
                cmd.Parameters.Add(new SqlParameter("@Species", values["Species"]));
                cmd.Parameters.Add(new SqlParameter("@Uses", values["Uses"]));
                cmd.Parameters.Add(new SqlParameter("@Medical_Benefit", values["MedicalBenefit"]));
                cmd.Parameters.Add(new SqlParameter("@Health_Hazard", values["HealthHazard"]));
                cmd.Parameters.Add(new SqlParameter("@Variety_Name", values["VarietyName"]));
                cmd.Parameters.Add(new SqlParameter("@Nature", values["Nature"]));
                cmd.Parameters.Add(new SqlParameter("@TimeOfSetting", values["TimeOfSetting"]));
                cmd.Parameters.Add(new SqlParameter("@TimeOfFlowering", values["TimeOfFlowering"]));
                cmd.Parameters.Add(new SqlParameter("@Rotation_Period", values["RotationPeriod"]));
                cmd.Parameters.Add(new SqlParameter("@Propagation_Method", values["PropagationMethod"]));
                cmd.Parameters.Add(new SqlParameter("@Tree_height", values["TreeHeight"]));
                cmd.Parameters.Add(new SqlParameter("@Trunk_Color", values["TrunkColor"]));
                cmd.Parameters.Add(new SqlParameter("@Tree_Form", values["TreeForm"]));
                cmd.Parameters.Add(new SqlParameter("@Leaf_Shape", values["LeafShape"]));
                cmd.Parameters.Add(new SqlParameter("@Fragrance", values["Fragrance"]));
                cmd.Parameters.Add(new SqlParameter("@Wood_Character", values["WoodCharacter"]));
                cmd.Parameters.Add(new SqlParameter("@Fruit_Type", values["FruitType"]));
                cmd.Parameters.Add(new SqlParameter("@Bark_Color", values["BarkColor"]));
                cmd.Parameters.Add(new SqlParameter("@Bark_texture", values["BarkTexture"]));
                cmd.Parameters.Add(new SqlParameter("@Litter_Type", values["LitterType"]));
                cmd.Parameters.Add(new SqlParameter("@Longevity", values["Longevity"]));
                cmd.Parameters.Add(new SqlParameter("@Growing_Condition", values["GrowingCondition"]));
                cmd.Parameters.Add(new SqlParameter("@DateOfPlanting", values["DateOfPlanting"]));
                cmd.Parameters.Add(new SqlParameter("@Location", values["LocationOfPlant"]));

                cmd.Parameters.Add(new SqlParameter("@ModifyBy", Session["Username"]));
                cmd.Parameters.Add(new SqlParameter("@uid", id));
                cmd.Parameters.Add(new SqlParameter("@ModifiedDate", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")));
                cmd.Parameters.Add(new SqlParameter("@OtherDetails", ""));

                con.Open();
                cmd.ExecuteNonQuery();
            }

            TempData["Message"] = "Sucessfully updated.";
            return Redirect($"/Plant/Details/{id}");
        }
    }
}