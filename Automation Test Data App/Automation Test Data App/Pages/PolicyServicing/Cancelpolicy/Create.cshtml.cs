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
            CancelpolicyInfo.TerminationDate = Request.Form["TerminationDate"];
            CancelpolicyInfo.Reason = Request.Form["Reason"];
           

            if(CancelpolicyInfo.PolicyNo.Length == 0 || CancelpolicyInfo.TerminationDate.Length == 0|| CancelpolicyInfo.Reason.Length == 0)
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
                                "(PolicyNo, TerminationDate,  Reason) VALUES" +
                                "(@PolicyNo, @TerminationDate, @Reason);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PolicyNo", CancelpolicyInfo.PolicyNo);
                        command.Parameters.AddWithValue("@TerminationDate", CancelpolicyInfo.TerminationDate);
                        command.Parameters.AddWithValue("@Reason", CancelpolicyInfo.Reason);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                CancelpolicyInfo.PolicyNo = ""; CancelpolicyInfo.TerminationDate = "";  CancelpolicyInfo.Reason = "";
                successMessage = "New Cancelation Policy Added Successfully";
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
