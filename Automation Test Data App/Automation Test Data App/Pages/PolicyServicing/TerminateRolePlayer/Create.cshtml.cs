using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.TerminateRolePlayer
{
    public class CreateModel : PageModel
    {
        public  Terminateinfo Terminateinfo = new Terminateinfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            
        }

        public void OnPost()
        {
            Terminateinfo.id = Request.Form["ID"];
            Terminateinfo.Scenario_ID = Request.Query["scenarioid"].ToString();
            Terminateinfo.RoleType = Request.Form["RoleType"];
            
            


            if (Terminateinfo.id.Length == 0|| Terminateinfo.RoleType.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
                  
            }
            //save new Life assured to DB
            try 
            {
               
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    String sql = "INSERT INTO TerminateRole " +
                                "(ID_No, Scenario_ID, Relationship) VALUES" +
                                "(@ID, @Scenario_ID, @RoleType);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ID", Terminateinfo.id);
                        command.Parameters.AddWithValue("@Scenario_ID", Terminateinfo.Scenario_ID);
                        command.Parameters.AddWithValue("@RoleType", Terminateinfo.RoleType);
                       
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                Terminateinfo.id = ""; Terminateinfo.RoleType = ""; Terminateinfo.Scenario_ID = ""; 
                successMessage = "Test data added successfully";
                return;

                

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/PolicyServicing/TerminateRolePlayer");


        }
    }
}
