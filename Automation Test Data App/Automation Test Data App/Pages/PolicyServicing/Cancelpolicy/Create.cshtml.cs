using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Cancelpolicy
{
    public class CreateModel : PageModel
    {
        public CancelpolicyInfo CancelpolicyInfo = new CancelpolicyInfo(); 
        public String errorMessage = "";
        public String successMessage = "";
        public String scenarioID = String.Empty;

        public void OnGet()
        {
            scenarioID = Request.Query["scenarioid"];
            CancelpolicyInfo.id = scenarioID;
        }

        public void OnPost()
        {
            scenarioID = Request.Query["scenarioid"];
            CancelpolicyInfo.TerminationReason = Request.Form["TerminationReason"];
           

            if(CancelpolicyInfo.TerminationReason.Length == 0)
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
                    String sql = "INSERT INTO Cancelpolicy " +
                                "(Scenario_ID, TerminationReason) VALUES" +
                                "(@scenarioID, @TerminationReason);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@scenarioID", scenarioID);
                        command.Parameters.AddWithValue("@TerminationReason", CancelpolicyInfo.TerminationReason);
                        command.ExecuteNonQuery();

                    }
                }
                 CancelpolicyInfo.TerminationReason = ""; 
                successMessage = "New Termination policy Added Successfully";
                return;
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/PolicyServicing/Cancelpolicy");
        }
    }
}
