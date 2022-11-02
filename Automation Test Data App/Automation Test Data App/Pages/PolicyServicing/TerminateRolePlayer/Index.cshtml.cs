using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.TerminateRolePlayer
{
    public class IndexModel : PageModel
    { 
    public List<Terminateinfo> ListTerminateinfo = new List<Terminateinfo>();
    public void OnGet()
    {
        try
        {
            String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                String sql = "SELECT * FROM TerminateRole ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {

                            Terminateinfo Terminateinfo = new Terminateinfo();
                                Terminateinfo.id = String.Empty + reader["ID_no"].ToString();
                                Terminateinfo.RoleType = String.Empty+reader["Relationship"].ToString();
                                Terminateinfo.Scenario_ID =String.Empty+ reader["Scenario_ID"].ToString();
                          



                                ListTerminateinfo.Add(Terminateinfo);
                        }


                }

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception:" + ex.ToString());

        }




    }
}
public class Terminateinfo
    {
        public String id;
        public String RoleType;
        public String Scenario_ID;
        


    }
}
