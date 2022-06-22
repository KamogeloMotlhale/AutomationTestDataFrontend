using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Downgrade
{
    public class CreateModel : PageModel
    {
        public DowngradeInfo DowngradeInfo = new DowngradeInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            DowngradeInfo.Date = Request.Form["Date"];
            DowngradeInfo.Component = Request.Form["Component"];
            DowngradeInfo.Cover_Amount = Request.Form["Cover_Amount"];
           

            if(DowngradeInfo.Date.Length == 0|| DowngradeInfo.Component.Length == 0 || DowngradeInfo.Cover_Amount.Length == 0)
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
                    String sql = "INSERT INTO Downgrade " +
                                "(Date, Component, Cover_Amount) VALUES" +
                                "(@Date, @Component, @Cover_Amount);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", DowngradeInfo.Date);
                        command.Parameters.AddWithValue("@Component", DowngradeInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", DowngradeInfo.Cover_Amount);
                        ;

                        command.ExecuteNonQuery();

                    }


                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            DowngradeInfo.Date = ""; DowngradeInfo.Component = ""; DowngradeInfo.Cover_Amount = "";
            successMessage = "New Downgrade Component Added Successfully";
            Response.Redirect("/PolicyServicing/Downgrade");
        }
    }
}
