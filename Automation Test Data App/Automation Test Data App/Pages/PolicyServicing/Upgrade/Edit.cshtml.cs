using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Upgrade
{
    public class EditModel : PageModel
    {
        public UpgradeInfo UpgradeInfo = new UpgradeInfo();
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
                    String sql = "SELECT * FROM Upgrade WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))

                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {

                                UpgradeInfo.id = "" + reader.GetInt32(0);
                                UpgradeInfo.Date = reader.GetString(1);
                                UpgradeInfo.Component = reader.GetString(2);
                                UpgradeInfo.Cover_Amount = reader.GetString(3);
                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }
        }
        public void OnPost()
        {

            UpgradeInfo.id = Request.Form["id"];
            UpgradeInfo.Date = Request.Form["date"];
            UpgradeInfo.Component = Request.Form["component"];
            UpgradeInfo.Cover_Amount = Request.Form["cover_amount"];

            if (UpgradeInfo.Date.Length == 0 || UpgradeInfo.Component.Length == 0 || UpgradeInfo.Cover_Amount.Length == 0)
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
                    String sql = "UPDATE UpGrade " +
                                "SET date=@Date, componenet=@Component, cover_amount=@Cover_Amount" +
                                 "WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Date", UpgradeInfo.Date);
                        command.Parameters.AddWithValue("@Component", UpgradeInfo.Component);
                        command.Parameters.AddWithValue("@Cover_Amount", UpgradeInfo.Cover_Amount);
                        command.Parameters.AddWithValue("@id", UpgradeInfo.id);



                        command.ExecuteNonQuery();

                    }


                }
                UpgradeInfo.Date = ""; UpgradeInfo.Component = ""; UpgradeInfo.Cover_Amount = "";
                successMessage = " Component Upadated Successfully";
                return;


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

        }

    }
}


