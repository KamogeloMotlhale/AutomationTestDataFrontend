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
                                ChangeLifeDataInfo.id =""+ reader.GetInt32(0);
                                ChangeLifeDataInfo.Title = reader.GetString(1);
                                ChangeLifeDataInfo.Surname = reader.GetString(2);
                                ChangeLifeDataInfo.MaritalStatus = reader.GetString(3);
                                ChangeLifeDataInfo.EducationLevel = reader.GetString(4);
                                ChangeLifeDataInfo.Department = reader.GetString(5);
                                ChangeLifeDataInfo.Profession = reader.GetString(6);


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
        

    }
}
