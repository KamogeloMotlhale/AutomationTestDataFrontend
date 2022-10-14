using Automation_Test_Data_App.Pages.PolicyServicing.Cancelpolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Cancelpolicy
{
    public class EditModel : PageModel
    { 
    public CancelpolicyInfo CancelpolicyInfo = new CancelpolicyInfo();
    public String errorMessage = "";
    public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM Cancelpolicy WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                CancelpolicyInfo.id = "" + reader.GetInt32(0);
                                CancelpolicyInfo.PolicyNo = reader.GetString(1);
                                CancelpolicyInfo.TerminationReason = reader.GetString(2);
                                



                            }
                        }


                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            } 

        }

        public void OnPost()
        {
            CancelpolicyInfo.id = Request.Form["id"];
            CancelpolicyInfo.PolicyNo = Request.Form["PolicyNo"];
            CancelpolicyInfo.TerminationReason = Request.Form["TerminationReason"];
          

            if (CancelpolicyInfo.PolicyNo.Length == 0 || CancelpolicyInfo.TerminationReason.Length == 0)
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
                    String sql = "UPDATE Cancelpolicy " +
                                 "SET PolicyNo=@PolicyNo, TerminationDate=@TerminationDate,  Reason=@Reason " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", CancelpolicyInfo.id);
                        command.Parameters.AddWithValue("@PolicyNo", CancelpolicyInfo.PolicyNo);
                        command.Parameters.AddWithValue("@TerminationDate", CancelpolicyInfo.TerminationReason);
                       
                  

                        command.ExecuteNonQuery();

                    }
                    CancelpolicyInfo.PolicyNo = ""; CancelpolicyInfo.TerminationReason = ""; 

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }



        }


    }
}
