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
    public String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

        public void OnGet()
        {
            String id = Request.Query["scenarioid"];


            try
            {

                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM Cancelpolicy WHERE Scenario_ID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                CancelpolicyInfo.id = reader["Scenario_ID"].ToString();
                                CancelpolicyInfo.TerminationReason = reader["TerminationReason"].ToString();

                            }
                            else
                            {

                                errorMessage = "No test data for this sceanrio, please add test data before editing";
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
            CancelpolicyInfo.TerminationReason = Request.Form["TerminationReason"];

            if (CancelpolicyInfo.TerminationReason.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            //save new Life assured to DB
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Cancelpolicy " +
                                 "SET TerminationReason=@TerminationReason " +
                                 "WHERE Scenario_ID=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                   
                        command.Parameters.AddWithValue("@id", CancelpolicyInfo.id);
                        command.Parameters.AddWithValue("@TerminationReason", CancelpolicyInfo.TerminationReason);
                        command.ExecuteNonQuery();

                    }
                     CancelpolicyInfo.TerminationReason = "";
                    successMessage = "Test data successfully updated";

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
