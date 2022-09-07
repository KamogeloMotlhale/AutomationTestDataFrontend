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

        public void OnGet()
        {
        }

        public void OnPost()
        {
            CancelpolicyInfo.PolicyNo = Request.Form["PolicyNo"];
            CancelpolicyInfo.TerminationReason = Request.Form["TerminationReason"];
            CancelpolicyInfo.TerminationDate = Request.Form["TerminationDate"];
           

            if(CancelpolicyInfo.PolicyNo.Length == 0|| CancelpolicyInfo.TerminationReason.Length == 0 || CancelpolicyInfo.TerminationDate.Length == 0)
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
                                "(PolicyNo, TerminationReason, TerminationDate) VALUES" +
                                "(@PolicyNo, @TerminationReason, @TerminationDate);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PolicyNo", CancelpolicyInfo.PolicyNo);
                        command.Parameters.AddWithValue("@TerminationReason", CancelpolicyInfo.TerminationReason);
                        command.Parameters.AddWithValue("@TerminationDate", CancelpolicyInfo.TerminationDate);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                CancelpolicyInfo.PolicyNo = ""; CancelpolicyInfo.TerminationReason = ""; CancelpolicyInfo.TerminationDate = "";
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
