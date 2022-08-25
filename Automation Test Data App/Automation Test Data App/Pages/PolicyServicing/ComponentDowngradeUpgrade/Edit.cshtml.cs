using Automation_Test_Data_App.Pages.PolicyServicing.ComponentDowngradeUpgrade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ComponentDowngradeUpgrade
{
    public class EditModel : PageModel
    {
        public DowngradeInfo DowngradeInfo = new DowngradeInfo();
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
                    String sql = "SELECT * FROM ComponentDowngradeUpgrade WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                DowngradeInfo.id = "" + reader.GetInt32(0);
                                DowngradeInfo.Date = reader.GetString(1);
                                DowngradeInfo.Method = reader.GetString(2);
                                DowngradeInfo.Cover_Amount = reader.GetString(3);



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
            DowngradeInfo.id = Request.Form["id"];
            DowngradeInfo.Date = Request.Form["Date"];
            DowngradeInfo.Method = Request.Form["Method"];
            DowngradeInfo.Cover_Amount = Request.Form["Cover_Amount"];


            if (DowngradeInfo.Date.Length == 0 || DowngradeInfo.Method.Length == 0 || DowngradeInfo.Cover_Amount.Length == 0)
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
                    String sql = "UPDATE Downgrade " +
                                 "SET Date=@Date, Method=@Method, Cover_Amount=@Cover_Amount " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", DowngradeInfo.id);
                        command.Parameters.AddWithValue("@Date", DowngradeInfo.Date);
                        command.Parameters.AddWithValue("@Component", DowngradeInfo.Method);
                        command.Parameters.AddWithValue("@Cover_Amount", DowngradeInfo.Cover_Amount);



                        command.ExecuteNonQuery();

                    }
                    DowngradeInfo.Date = ""; DowngradeInfo.Method = ""; DowngradeInfo.Cover_Amount = "";

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
