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
                                ReinstateInfo.ReinstatementReason = reader.GetString(2);
                                ReinstateInfo.ReinstatementDate = reader.GetString(3);



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
            ReinstateInfo.ReinstatementReason = Request.Form["ReinstatementReason"];
            ReinstateInfo.ReinstatementDate = Request.Form["ReinstatementDate"];
          

            if (ReinstateInfo.PolicyNo.Length == 0 || ReinstateInfo.ReinstatementReason.Length == 0 || ReinstateInfo.ReinstatementDate.Length == 0)
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
                                 "SET PolicyNo=@PolicyNo,ReinstatementReason=@ReinstatementReason, ReinstatementDate=@ReinstatementDate " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", ReinstateInfo.id);
                        command.Parameters.AddWithValue("@PolicyNo", ReinstateInfo.PolicyNo);
                        command.Parameters.AddWithValue("@ReinstatementReason", ReinstateInfo.ReinstatementReason);
                        command.Parameters.AddWithValue("@ReinstatementDate", ReinstateInfo.ReinstatementDate);
                       
                  

                        command.ExecuteNonQuery();

                    }
                    ReinstateInfo.PolicyNo = ""; ReinstateInfo.ReinstatementReason = ""; ReinstateInfo.ReinstatementDate = "";

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
