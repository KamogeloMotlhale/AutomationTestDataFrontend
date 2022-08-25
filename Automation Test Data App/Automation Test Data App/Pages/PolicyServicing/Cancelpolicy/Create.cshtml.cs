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
            CancelpolicyInfo.Date = Request.Form["Date"];
            CancelpolicyInfo.Reason = Request.Form["Reason"];
           

            if(CancelpolicyInfo.Date.Length == 0|| CancelpolicyInfo.Reason.Length == 0)
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
                                "(Date,  Reason) VALUES" +
                                "(@Date, @Reason);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", CancelpolicyInfo.Date);
                        command.Parameters.AddWithValue("@Reason", CancelpolicyInfo.Reason);
                        ;

                        command.ExecuteNonQuery();

                    }


                }
                CancelpolicyInfo.Date = "";  CancelpolicyInfo.Reason = "";
                successMessage = "New Downgrade Component Added Successfully";
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
