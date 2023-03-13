using Automation_Test_Data_App.Pages.PolicyServicing.TerminateRolePlayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.TerminateRolePlayer
{
    public class EditModel : PageModel
    {
        public Terminateinfo Terminateinfo = new Terminateinfo();
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
                    String sql = "SELECT * FROM TerminateRole WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read())
                            {

                                Terminateinfo.id = String.Empty + reader["ID_no"].ToString();
                                Terminateinfo.RoleType = String.Empty + reader["Relationship"].ToString();
                                Terminateinfo.Scenario_ID = String.Empty + reader["Scenario_ID"].ToString();





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
            Terminateinfo.id = Request.Form["id"];
            Terminateinfo.RoleType = Request.Form["RoleType"];
            Terminateinfo.Scenario_ID = Request.Form["Scenario_ID"];
            

            if (Terminateinfo.id.Length == 0 || Terminateinfo.RoleType.Length == 0 || Terminateinfo.Scenario_ID.Length == 0 )
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
                        command.Parameters.AddWithValue("@id", Terminateinfo.id);
                        command.Parameters.AddWithValue("@Date", Terminateinfo.RoleType);
                        command.Parameters.AddWithValue("@Method", Terminateinfo.Scenario_ID);
                       


                        command.ExecuteNonQuery();

                    }
                    Terminateinfo.id = ""; Terminateinfo.RoleType = ""; Terminateinfo.Scenario_ID = "";

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
