using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Reinstate
{
    public class CreateModel : PageModel
    {
        public ReinstateInfo ReinstateInfo = new ReinstateInfo(); 
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            ReinstateInfo.Date = Request.Form["Date"];
            ReinstateInfo.Component = Request.Form["Component"];
            ReinstateInfo.Cover_Amount = Request.Form["Reason"];
           

            if(ReinstateInfo.Date.Length == 0|| ReinstateInfo.Component.Length == 0 || ReinstateInfo.Reason.Length == 0)
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
                    String sql = "INSERT INTO Reinstate " +
                                "(Date, Component, Reason) VALUES" +
                                "(@Date, @Component, @Reason);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", ReinstateInfo.Date);
                        command.Parameters.AddWithValue("@Component", ReinstateInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", ReinstateInfo.Reason);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                ReinstateInfo.Date = ""; ReinstateInfo.Component = ""; ReinstateInfo.Reason = "";
                successMessage = "New Downgrade Component Added Successfully";
                return;

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

           
            
            Response.Redirect("/PolicyServicing/Reinstate");
        }
    }
}
