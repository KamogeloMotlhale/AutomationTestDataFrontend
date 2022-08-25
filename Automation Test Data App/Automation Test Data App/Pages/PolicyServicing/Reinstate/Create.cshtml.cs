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
            ReinstateInfo.Reason = Request.Form["Reason"];
           

            if(ReinstateInfo.Date.Length == 0|| ReinstateInfo.Reason.Length == 0)
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
                                "(Date,  Reason) VALUES" +
                                "(@Date, @Reason);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", ReinstateInfo.Date);
                        command.Parameters.AddWithValue("@Reason", ReinstateInfo.Reason);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                ReinstateInfo.Date = ""; ReinstateInfo.Reason = "";
                successMessage = "New Reinstatement Info  Added Successfully";
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
