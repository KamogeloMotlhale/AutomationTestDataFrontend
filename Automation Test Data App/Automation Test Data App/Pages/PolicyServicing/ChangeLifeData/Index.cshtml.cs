using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangeLifeData
{
    public class IndexModel : PageModel
    {
        public List<ChangeLifeDataInfo> ListChangeLifeData = new List<ChangeLifeDataInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM ChangeLifeData";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                ChangeLifeDataInfo ChangeLifeDataInfo = new ChangeLifeDataInfo();
                                ChangeLifeDataInfo.id = String.Empty + reader["Scenario_ID"].ToString(); ChangeLifeDataInfo.Title = reader.GetString(1);
                                ChangeLifeDataInfo.Surname = String.Empty + reader["Surname"].ToString();
                                ChangeLifeDataInfo.MaritalStatus = String.Empty + reader["MaritalStatus"].ToString();
                                ChangeLifeDataInfo.EducationLevel = String.Empty + reader["EducationLevel"].ToString();
                                ChangeLifeDataInfo.Department = String.Empty + reader["Department"].ToString();
                                ChangeLifeDataInfo.Profession = String.Empty + reader["Profession"].ToString();
                                ChangeLifeDataInfo.Roleplayer = String.Empty + reader["Roleplayer"].ToString();
                                ChangeLifeDataInfo.RolePlayer_idNum = String.Empty + reader["RolePlayer_idNum"].ToString();

                                ListChangeLifeData.Add(ChangeLifeDataInfo);
                         }


                    }

                }
            
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }




        }
    }

    public class ChangeLifeDataInfo
    {
        public String id;
        public String Title;
        public String Surname;
        public String MaritalStatus;
        public String EducationLevel;
        public String Department;
        public String Profession;
        public String Roleplayer;
        public String RolePlayer_idNum;



    }
}
