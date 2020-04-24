using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;// This data provider need to make connection with SQL Server
using System.IO;
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
                    ViewBag.Img1 = reader["img1"].ToString();
                    ViewBag.Img2 = reader["img2"].ToString();
                    ViewBag.Img3 = reader["img3"].ToString();
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

        [HttpGet]
        public ActionResult AddFamily()
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
                string query = "select FamilyName from FamilyMaster";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var familyList = new List<string>();
                while (reader.Read())
                {
                    familyList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.FamilyList = familyList;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddFamily(FormCollection values)
        {
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Admin_FamilyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FamilyName", values["familyName"]));
                cmd.Parameters.Add(new SqlParameter("@FamilyCommonName", values["familyCommonName"]));
                cmd.Parameters.Add(new SqlParameter("@Habitat", values["habitat"]));
                cmd.Parameters.Add(new SqlParameter("@entryby", Session["Username"]));

                con.Open();
                cmd.ExecuteNonQuery();
                TempData["Message"] = "Family Details inserted successfully";

                //for populating suggestions
                string query = "select FamilyName from FamilyMaster";
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                var familyList = new List<string>();
                while (reader.Read())
                {
                    familyList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.FamilyList = familyList;
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeleteFamily(int familyId)
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
                SqlCommand cmd = new SqlCommand("sp_Delete_Admin_FamilyMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Family_Id", familyId));
                con.Open();
                try
                {
                    if(cmd.ExecuteNonQuery() != 0)
                    {
                        TempData["Message"] = "Family details deleted successfully.";
                    }
                }
                catch (SqlException ex)
                {
                    TempData["Message"] = "This family has Plant Detalils. Deletion failed.";
                }
            }
            return Redirect("/Plant/AddFamily/");
        }

        [HttpGet]
        public ActionResult AddPlant(int familyId)
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
                string query = "select Botonical_Name from PlantMaster where Family_Id="+familyId;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var plantList = new List<string>();
                while (reader.Read())
                {
                    plantList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.PlantList = plantList;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddPlant(int familyId, FormCollection values)
        {
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Admin_PlantDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Family_Id", familyId));
                cmd.Parameters.Add(new SqlParameter("@Common_Name", values["commonName"]));
                cmd.Parameters.Add(new SqlParameter("@Botonical_Name", values["botanicalName"]));
                cmd.Parameters.Add(new SqlParameter("@Chromosome_No", values["chromosomeNo"]));
                cmd.Parameters.Add(new SqlParameter("@Genus", values["genus"]));
                cmd.Parameters.Add(new SqlParameter("@Species", values["species"]));
                cmd.Parameters.Add(new SqlParameter("@Uses", values["uses"]));
                cmd.Parameters.Add(new SqlParameter("@Medical_Benefit", values["medicalBenefits"]));
                cmd.Parameters.Add(new SqlParameter("@Health_Hazard", values["healthHazards"]));

                con.Open();
                cmd.ExecuteNonQuery();
                TempData["Message"] = "Plant Details inserted successfully";

                //for populating suggestions

                string query = "select Botonical_Name from PlantMaster where Family_Id=" + familyId;
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                var plantList = new List<string>();
                while (reader.Read())
                {
                    plantList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.PlantList = plantList;
            }
            return View();
        }

        [HttpGet]
        public ActionResult DeletePlant(int familyId, int plantId)
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
                SqlCommand cmd = new SqlCommand("sp_Delete_Admin_PlantMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Plant_Id", plantId));
                con.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        TempData["Message"] = "Plant details deleted successfully.";
                    }
                }
                catch (SqlException ex)
                {
                    TempData["Message"] = "This Plant has Variety Details. Deletion failed.";
                }
            }
            return Redirect($"/Plant/AddPlant/?familyId={familyId}");
        }

        [HttpGet]
        public ActionResult AddVariety(int familyId, int plantId)
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
                string query = "select Variety_Name from VarietyMaster where Plant_Id=" + plantId;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var varietyList = new List<string>();
                while (reader.Read())
                {
                    varietyList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.VarietyList = varietyList;
            }
            ViewBag.FamilyId = familyId;
            ViewBag.PlantId = plantId;
            return View();
        }

        [HttpPost]
        public ActionResult AddVariety(int familyId, int plantId, FormCollection values)
        {
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Admin_VarietyDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Plant_Id", plantId));
                cmd.Parameters.Add(new SqlParameter("@Variety_Name", values["varietyName"]));
                cmd.Parameters.Add(new SqlParameter("@Nature", values["nature"]));
                cmd.Parameters.Add(new SqlParameter("@TimeOfSetting", values["timeOfSetting"]));
                cmd.Parameters.Add(new SqlParameter("@TimeOfFlowering", values["timeOfFlowering"]));
                cmd.Parameters.Add(new SqlParameter("@Rotation_Period", values["rotationPeriod"]));
                cmd.Parameters.Add(new SqlParameter("@Propagation_Method", values["propagationMethod"]));
                cmd.Parameters.Add(new SqlParameter("@Tree_Height", values["treeHeight"]));
                cmd.Parameters.Add(new SqlParameter("@Trunk_Color", values["trunkColour"]));
                cmd.Parameters.Add(new SqlParameter("@Tree_Form", values["treeForm"]));
                cmd.Parameters.Add(new SqlParameter("@Leaf_shape", values["leafShape"]));
                cmd.Parameters.Add(new SqlParameter("@fragrance", values["fragrance"]));
                cmd.Parameters.Add(new SqlParameter("@wood_character", values["woodCharacter"]));
                cmd.Parameters.Add(new SqlParameter("@fruit_type", values["fruitType"]));
                cmd.Parameters.Add(new SqlParameter("@bark_color", values["barkColour"]));
                cmd.Parameters.Add(new SqlParameter("@bark_texture", values["barkTexture"]));
                cmd.Parameters.Add(new SqlParameter("@litter_type", values["litterType"]));
                cmd.Parameters.Add(new SqlParameter("@longevity", values["longetivity"]));
                cmd.Parameters.Add(new SqlParameter("@growing_condition ", values["growingConditions"]));

                con.Open();
                cmd.ExecuteNonQuery();
                TempData["Message"] = "Variety Details inserted successfully";

                //for populating suggestions

                string query = "select Variety_Name from VarietyMaster where Plant_Id=" + plantId;
                cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                var varietyList = new List<string>();
                while (reader.Read())
                {
                    varietyList.Add(reader[0].ToString());
                }
                reader.Close();
                ViewBag.VarietyList = varietyList;
            }
            ViewBag.FamilyId = familyId;
            ViewBag.PlantId = plantId;
            return View();
        }

        [HttpGet]
        public ActionResult DeleteVariety(int familyId, int plantId, int varietyId)
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
                SqlCommand cmd = new SqlCommand("sp_Delete_Admin_VarietyMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Variety_Id", varietyId));
                con.Open();
                try
                {
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        TempData["Message"] = "Variety details deleted successfully.";
                    }
            }
                catch (SqlException ex)
            {
                TempData["Message"] = "This Variety has Location Details. Deletion failed.";
            }
        }
            return Redirect($"/Plant/AddVariety/?familyId={familyId}&plantId={plantId}");
        }

        [HttpGet]
        public ActionResult AddLocationDate(int familyId, int plantId, int varietyId)
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
            //Get all records from LocationMaster with variety and plant ids
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Display_Admin_LocationMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Plant_Id", plantId));
                cmd.Parameters.Add(new SqlParameter("@Variety_Id", varietyId));
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                var displayLocations = new List<List<string>>();
                while (reader.Read())
                {
                    displayLocations.Add(new List<string>() {
                        reader["UniquePlant_Id"].ToString(),
                        reader["DateOfPlanting"].ToString(),
                        reader["Location"].ToString()
                    });
                }
                ViewBag.DisplayLocations = displayLocations;
            }
            ViewBag.FamilyId = familyId;
            ViewBag.PlantId = plantId;
            ViewBag.VarietyId = varietyId;

            return View();
        }

        [HttpPost]
        public ActionResult AddLocationDate(int familyId, int plantId, int varietyId, FormCollection values)
        {
            string[] imagePaths = new string[3] { "", "", "" };//these are relative paths to store in database
            string datetime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (Request.Files[i].ContentLength == 0) continue;
                string fileName = Path.GetFileNameWithoutExtension(Request.Files[i].FileName);
                string extension = Path.GetExtension(Request.Files[i].FileName);
                fileName = fileName + datetime + "-" + i + extension;//preventing duplicay of filename by appending datetime and i value
                imagePaths[i] = "~/Images/" + fileName;
                string absolutePath = Path.Combine(Server.MapPath("~/Images/"), fileName);
                Request.Files[i].SaveAs(absolutePath);
            }

            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Insert_Admin_LocationDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Family_Id", familyId));
                cmd.Parameters.Add(new SqlParameter("@Plant_Id", plantId));
                cmd.Parameters.Add(new SqlParameter("@Variety_Id", varietyId));
                cmd.Parameters.Add(new SqlParameter("@DateOfPlanting", values["dateOfPlanting"]));
                cmd.Parameters.Add(new SqlParameter("@Location", values["location"]));
                //below are relative paths to files
                cmd.Parameters.Add(new SqlParameter("@img1", imagePaths[0]));
                cmd.Parameters.Add(new SqlParameter("@img2", imagePaths[1]));
                cmd.Parameters.Add(new SqlParameter("@img3", imagePaths[2]));
                cmd.Parameters.Add(new SqlParameter("@qr", ""));

                TempData["Message"] = "Location Details inserted successfully";
                con.Open();
                cmd.ExecuteNonQuery();
            }
            
            return Redirect($"/Plant/AddLocationDate/?familyId={familyId}&plantId={plantId}&varietyId={varietyId}");
        }

        [HttpGet]
        public ActionResult DeleteLocationDate(int id, int familyId, int plantId, int varietyId)
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

            // To delete a record in LocationMaster Table
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_Delete_Admin_LocationMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@uid", id));

                con.Open();
                cmd.ExecuteNonQuery();
                TempData["Message"] = "Location Details deleted successfully";
            }
            return Redirect($"/Plant/AddLocationDate/?familyId={familyId}&plantId={plantId}&varietyId={varietyId}");
        }
    }
}