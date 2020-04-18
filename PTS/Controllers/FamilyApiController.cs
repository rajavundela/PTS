using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTS.Controllers
{
    public class FamilyApiController : ApiController
    {
        [HttpGet]
        public IDictionary<string, string> GetFamilyDetails(string familyName)
        {

            //Get family details based on family name
            var familyData = new Dictionary<string, string>();
            string connectionString = "server=pts69dbserver.database.windows.net;user id=pts;password=group7@infotech;database=pts";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"select Family_Id,FamilyName,FamilyCommonName, Habitat from FamilyMaster where FamilyName='{familyName}'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.Read())
                {
                    familyData.Add("familyId", reader[0].ToString());
                    familyData.Add("familyName", reader[1].ToString());
                    familyData.Add("familyCommonName", reader[2].ToString());
                    familyData.Add("habitat", reader[3].ToString());
                }
                reader.Close(); 
            }
            return familyData;
        }
    }
}
