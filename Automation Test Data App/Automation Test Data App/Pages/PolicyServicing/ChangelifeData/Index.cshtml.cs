using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangelifeData
{
    public class IndexModel : PageModel
    {
        public List<ChangelifeDataInfo> ListChangelifeData = new List<ChangelifeDataInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM ChangelifeData";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                ChangelifeDataInfo ChangelifeDataInfo = new ChangelifeDataInfo();
                                ChangelifeDataInfo.id =""+ reader.GetInt32(0);
                                ChangelifeDataInfo.Title = reader.GetString(1);
                                ChangelifeDataInfo.Surname = reader.GetString(2);
                                ChangelifeDataInfo.MaritalStatus = reader.GetString(3);
                                ChangelifeDataInfo.EducationLevel = reader.GetString(4);
                                ChangelifeDataInfo.Department = reader.GetString(5);
                                ChangelifeDataInfo.Profession = reader.GetString(6);


                                ListChangelifeData.Add(ChangelifeDataInfo);
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

    public class ChangelifeDataInfo
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
