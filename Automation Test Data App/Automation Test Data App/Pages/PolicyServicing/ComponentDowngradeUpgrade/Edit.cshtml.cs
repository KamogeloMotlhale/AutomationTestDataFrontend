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
            String id = Request.Query["scenarioid"];

            try
            {

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM ComponentDowngradeUpgrade WHERE ScenarioID=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                DowngradeInfo.id = id;
                                DowngradeInfo.Method = reader["Method"].ToString();
                                DowngradeInfo.Cover_Amount = reader["Sum_assured"].ToString();
                                DowngradeInfo.Component = reader["Component"].ToString();
                                DowngradeInfo.comID = reader["component_IDNo"].ToString();
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
            String id = Request.Query["scenarioid"];
            DowngradeInfo.Method = Request.Form["method"];
            DowngradeInfo.Cover_Amount = Request.Form["sumAssured"];
            DowngradeInfo.Component = Request.Form["component"];
            DowngradeInfo.comID = Request.Form["compId"];


            if ( DowngradeInfo.Method.Length == 0 
                || DowngradeInfo.Cover_Amount.Length == 0 
                || DowngradeInfo.Component.Length == 0
                || DowngradeInfo.comID.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;

            }
            //save to DB
            try
            {

                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE ComponentDowngradeUpgrade " +
                                 "SET  Method=@Method, Sum_assured=@Cover_Amount, Component=@Component,  component_IDNo=@compID" +
                                 " WHERE ScenarioID=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                   
                        command.Parameters.AddWithValue("@Method", DowngradeInfo.Method);
                        command.Parameters.AddWithValue("@Cover_Amount", DowngradeInfo.Cover_Amount);
                        command.Parameters.AddWithValue("@Component", DowngradeInfo.Component);
                        command.Parameters.AddWithValue("@compID", DowngradeInfo.comID);
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                 DowngradeInfo.Method = ""; DowngradeInfo.Cover_Amount = "";
                 successMessage = "Test data Succesfully Updated";
                 

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
