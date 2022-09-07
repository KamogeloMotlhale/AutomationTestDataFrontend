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
            ReinstateInfo.PolicyNo = Request.Form["PolicyNo"];
            ReinstateInfo.ReinstatementReason = Request.Form["ReinstatementReason"];
            ReinstateInfo.ReinstatementDate = Request.Form["ReinstatementDate"];
           

            if(ReinstateInfo.PolicyNo.Length == 0|| ReinstateInfo.ReinstatementReason.Length == 0 || ReinstateInfo.ReinstatementDate.Length == 0)
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
                                "(PolicyNo, ReinstatementReason, ReinstatementDate) VALUES" +
                                "(@PolicyNo, @ReinstatementReason, @ReinstatementDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PolicyNo", ReinstateInfo.PolicyNo);
                        command.Parameters.AddWithValue("@ReinstatementReason", ReinstateInfo.ReinstatementReason);
                        command.Parameters.AddWithValue("@ReinstatementDate", ReinstateInfo.ReinstatementDate);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                ReinstateInfo.PolicyNo = ""; ReinstateInfo.ReinstatementReason = ""; ReinstateInfo.ReinstatementDate = "";
                successMessage = "New Downgrade Component Added Successfully";
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
