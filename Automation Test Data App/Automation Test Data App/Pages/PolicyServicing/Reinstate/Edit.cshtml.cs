using Automation_Test_Data_App.Pages.PolicyServicing.Reinstate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Reinstate
{
    public class EditModel : PageModel
    { 
    public ReinstateInfo ReinstateInfo = new ReinstateInfo();
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
                    String sql = "SELECT * FROM Reinstate WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                ReinstateInfo.id = "" + reader.GetInt32(0);
                                ReinstateInfo.Date = reader.GetString(1);
                                ReinstateInfo.Component = reader.GetString(2);
                                ReinstateInfo.Cover_Amount = reader.GetString(3);
                           
                              

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
            ReinstateInfo.id = Request.Form["id"];
            ReinstateInfo.Date = Request.Form["Date"];
            ReinstateInfo.Component = Request.Form["Component"];
            ReinstateInfo.Cover_Amount = Request.Form["Cover_Amount"];
          

            if (ReinstateInfo.Date.Length == 0 || ReinstateInfo.Component.Length == 0 || ReinstateInfo.Cover_Amount.Length == 0)
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
                    String sql = "UPDATE Reinstate " +
                                 "SET Date=@Date, Component=@Component, Cover_Amount=@Cover_Amount " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", ReinstateInfo.id);
                        command.Parameters.AddWithValue("@Date", ReinstateInfo.Date);
                        command.Parameters.AddWithValue("@Component", ReinstateInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", ReinstateInfo.Cover_Amount);
                       
                  

                        command.ExecuteNonQuery();

                    }
                    ReinstateInfo.Date = ""; ReinstateInfo.Component = ""; ReinstateInfo.Reason = "";

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
