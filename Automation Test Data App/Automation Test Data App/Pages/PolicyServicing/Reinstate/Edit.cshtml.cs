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
                                ReinstateInfo.PolicyNo = reader.GetString(1);
                                ReinstateInfo.Date = reader.GetString(2);
                                ReinstateInfo.Reason = reader.GetString(3);



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
            ReinstateInfo.PolicyNo = Request.Form["PolicyNo"];
            ReinstateInfo.Date = Request.Form["Date"];
            ReinstateInfo.Reason = Request.Form["Reason"];
          

            if (ReinstateInfo.PolicyNo.Length == 0 || ReinstateInfo.Date.Length == 0 || ReinstateInfo.Reason.Length == 0)
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
                    String sql = "UPDATE ReinstateInfo " +
                                 "SET PolicyNo=@PolicyNo,Date=@Date, Reason=@Reason " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", ReinstateInfo.id);
                        command.Parameters.AddWithValue("@PolicyNo", ReinstateInfo.PolicyNo);
                        command.Parameters.AddWithValue("@Date", ReinstateInfo.Date);
                        command.Parameters.AddWithValue("@Reason", ReinstateInfo.Reason);
                       
                  

                        command.ExecuteNonQuery();

                    }
                    ReinstateInfo.PolicyNo = ""; ReinstateInfo.Date = ""; ReinstateInfo.Reason = "";

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
