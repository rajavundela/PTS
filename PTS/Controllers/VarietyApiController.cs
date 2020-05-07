using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTS.Controllers
{
    public class VarietyApiController : ApiController
    {
        [HttpGet]
        public IDictionary<string, string> GetPlantDetails(string varietyName)
        {

            //Get varietydetails based on varietyname
            var varietyData = new Dictionary<string, string>();
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select Plant_Id, Variety_Id, Variety_Name, Nature, TimeOfSetting, TimeOfFlowering, Rotation_Period, Propagation_Method, Tree_Height, Trunk_Color, Tree_Form, Leaf_Shape, Fragrance, Wood_Character, Fruit_Type, Bark_Color, Bark_Texture, Litter_Type, Longevity, Growing_Condition from VarietyMaster where Variety_Name=@0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@0", varietyName);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    varietyData.Add("plantId", reader[0].ToString());
                    varietyData.Add("varietyId", reader[1].ToString());

                    varietyData.Add("varietyName", reader[2].ToString());
                    varietyData.Add("nature", reader[3].ToString());
                    varietyData.Add("timeOfSetting", reader[4].ToString());
                    varietyData.Add("timeOfFlowering", reader[5].ToString());
                    varietyData.Add("rotationPeriod", reader[6].ToString());
                    varietyData.Add("propagationMethod", reader[7].ToString());
                    varietyData.Add("treeHeight", reader[8].ToString());
                    varietyData.Add("trunkColour", reader[9].ToString());
                    varietyData.Add("treeForm", reader[10].ToString());
                    varietyData.Add("leafShape", reader[11].ToString());
                    varietyData.Add("fragrance", reader[12].ToString());
                    varietyData.Add("woodCharacter", reader[13].ToString());
                    varietyData.Add("fruitType", reader[14].ToString());
                    varietyData.Add("barkColour", reader[15].ToString());
                    varietyData.Add("barkTexture", reader[16].ToString());
                    varietyData.Add("litterType", reader[17].ToString());
                    varietyData.Add("longetivity", reader[18].ToString());
                    varietyData.Add("growingConditions", reader[19].ToString());
                }
                reader.Close();
            }
            return varietyData;
        }
    }
}
