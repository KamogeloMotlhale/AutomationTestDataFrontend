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
                                DowngradeInfo.Method = reader.GetString(2);
                                DowngradeInfo.Cover_Amount = reader.GetString(3);
                                DowngradeInfo.Component = reader.GetString(4);
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
            DowngradeInfo.Method = Request.Form["Method"];
            DowngradeInfo.Cover_Amount = Request.Form["Cover_Amount"];
            DowngradeInfo.Cover_Amount = Request.Form["Component"];


            if ( DowngradeInfo.Method.Length == 0 || DowngradeInfo.Cover_Amount.Length == 0 || DowngradeInfo.Component.Length == 0)
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
                    String sql = "UPDATE ComponentDowngradeUpgrade " +
                                 "SET Date=@Date, Method=@Method, Cover_Amount=@Cover_Amount, Component=@Component  " +
                                 "WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", DowngradeInfo.id);
                        command.Parameters.AddWithValue("@Method", DowngradeInfo.Method);
                        command.Parameters.AddWithValue("@Cover_Amount", DowngradeInfo.Cover_Amount);



                        command.ExecuteNonQuery();

                    }
                 DowngradeInfo.Method = ""; DowngradeInfo.Cover_Amount = "";

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
