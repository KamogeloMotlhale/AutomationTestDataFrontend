using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{
    public class IndexModel : PageModel
    {
        public List<PScenarioInfo> ListPsScenarios = new List<PScenarioInfo>();
        public void OnGet()
        {

            String dt = (DateTime.Now.StartOfWeek(DayOfWeek.Monday)).ToString("dd/MM/yyyy");


            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                try
                {
                    String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = $"SELECT * FROM PS_Scenarios";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                {
                                    PScenarioInfo pScenarioInfo = new PScenarioInfo();
                                    pScenarioInfo.id = reader["ID"].ToString();
                                    pScenarioInfo.policyNo = reader["PolicyNo"].ToString();
                                    pScenarioInfo.expectedResults = reader["ExpectedResults"].ToString();
                                    pScenarioInfo.testDate = reader["Test_Date"].ToString();
                                    pScenarioInfo.testResutls = reader["Test_Results"].ToString();
                                    pScenarioInfo.comments = reader["Comments"].ToString();
                                    pScenarioInfo.functionID  = reader["functionID"].ToString();
                                    pScenarioInfo.productName = reader["productName"].ToString();
                                    pScenarioInfo.getFuncName();
                                    ListPsScenarios.Add(pScenarioInfo);
                                }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.ToString());

                }
                return;
            }
        }
    }
    public class PScenarioInfo
    {
        public String id;
        public String policyNo;
        public String expectedResults;
        public String testResutls;
        public String comments;
        public String testDate;
        public String functionID;
        public String functionName;
        public String userID;
        public String productName;
        public String dateCreated;

        public void getFuncName()
        {
            try
            {
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = $"SELECT function_name FROM Functions where ID ={functionID}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {

                               functionName = reader["function_name"].ToString();
                              
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
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
    public class Function
    {
        public String id;
        public String name;
    }
}
