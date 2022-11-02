using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ComponentDowngradeUpgrade
{
    public class CreateModel : PageModel
    {
        public DowngradeInfo DowngradeInfo = new DowngradeInfo(); 
        public String errorMessage = "";
        public String successMessage = "";
        public String scenarioID = "";

        public void OnGet()
        {
            scenarioID = Request.Query["scenarioid"];
        }

        public void OnPost()
        {
         
            DowngradeInfo.id = Request.Query["scenarioid"];
            DowngradeInfo.Method = Request.Form["Method"];
            DowngradeInfo.comID = Request.Form["compIDNO"];
            DowngradeInfo.Cover_Amount = Request.Form["Cover_Amount"];
            DowngradeInfo.Component = Request.Form["Component"];


            if (DowngradeInfo.id.Length == 0 || DowngradeInfo.comID.Length == 0 ||  DowngradeInfo.Method.Length == 0 || DowngradeInfo.Cover_Amount.Length == 0 || DowngradeInfo.Component.Length == 0)
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
                    String sql = "INSERT INTO ComponentDowngradeUpgrade " +
                                "(Method, Component,Sum_assured,component_IDNo,ScenarioID) VALUES" +
                                "( @Method,@Component, @Cover_Amount,@compID, @scenarioId); ";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
             
                        command.Parameters.AddWithValue("@Method", DowngradeInfo.Method);
                        command.Parameters.AddWithValue("@Component", DowngradeInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", DowngradeInfo.Cover_Amount);
                        command.Parameters.AddWithValue("@compID", DowngradeInfo.comID);
                        command.Parameters.AddWithValue("@scenarioID", DowngradeInfo.id);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
           DowngradeInfo.Method = ""; DowngradeInfo.Cover_Amount = ""; DowngradeInfo.Component = ""; 
                successMessage = "New Scenario Added Successfully";
                return;

                

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/PolicyServicing/ComponentDowngradeUpgrade");


        }
    }
}
