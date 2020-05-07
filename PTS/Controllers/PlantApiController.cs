using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTS.Controllers
{
    public class PlantApiController : ApiController
    {
        [HttpGet]
        public IDictionary<string, string> GetPlantDetails(string botanicalName)
        {

            //Get plant details based on botanical name
            var plantData = new Dictionary<string, string>();
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select Family_Id,Plant_Id,Common_Name, Botonical_Name, Chromosome_No, Genus, Species, Uses, Medical_Benefit, Health_Hazard from PlantMaster where Botonical_Name=@0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@0", botanicalName);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    plantData.Add("familyId", reader[0].ToString());
                    plantData.Add("plantId", reader[1].ToString());

                    plantData.Add("commonName", reader[2].ToString());
                    plantData.Add("botanicalName", reader[3].ToString());
                    plantData.Add("chromosomeNo", reader[4].ToString());
                    plantData.Add("genus", reader[5].ToString());
                    plantData.Add("species", reader[6].ToString());
                    plantData.Add("uses", reader[7].ToString());
                    plantData.Add("medicalBenefits", reader[8].ToString());
                    plantData.Add("healthHazards", reader[9].ToString());
                }
                reader.Close();
            }
            return plantData;
        }
    }
}
