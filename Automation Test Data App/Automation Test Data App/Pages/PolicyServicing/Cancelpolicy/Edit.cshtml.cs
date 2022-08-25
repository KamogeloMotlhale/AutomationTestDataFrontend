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
                                CancelpolicyInfo.Date = reader.GetString(1);
                                CancelpolicyInfo.Reason = reader.GetString(3);



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
            CancelpolicyInfo.Date = Request.Form["Date"];
            CancelpolicyInfo.Reason = Request.Form["Reason"];
          

            if (CancelpolicyInfo.Date.Length == 0 ||  CancelpolicyInfo.Reason.Length == 0)
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
                                 "SET Date=@Date, Component=@Component, Reason=@Reason " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", CancelpolicyInfo.id);
                        command.Parameters.AddWithValue("@Date", CancelpolicyInfo.Date);
                        command.Parameters.AddWithValue("@Reason", CancelpolicyInfo.Reason);
                       
                  

                        command.ExecuteNonQuery();

                    }
                    CancelpolicyInfo.Date = ""; CancelpolicyInfo.Reason = "";

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
