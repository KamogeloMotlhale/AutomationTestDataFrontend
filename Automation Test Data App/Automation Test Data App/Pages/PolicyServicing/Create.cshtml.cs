using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{

    public class CreateModel : PageModel
    {
        public PScenarioInfo PScenarioInfo = new PScenarioInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public String scenarioID = "";
        public List<Function> funcList = new List<Function>();

        public void OnGet()
        {
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
                        String sql = $"SELECT * FROM Functions";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                                while (reader.Read())
                                {
                                    Function func = new Function();

                                    func.id = reader["ID"].ToString();
                                    func.name = reader["function_name"].ToString();
                                    funcList.Add(func);
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

        public void OnPost() {


            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                PScenarioInfo.policyNo = Request.Form["policyno"];
                PScenarioInfo.expectedResults = Request.Form["expResults"];
                PScenarioInfo.functionID = Request.Form["function"];

                if (PScenarioInfo.policyNo.Length == 0 || PScenarioInfo.expectedResults.Length == 0 || PScenarioInfo.functionID.Length == 0)
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
                        String sql = "INSERT INTO PS_Scenarios" +
                                    "(PolicyNo,ExpectedResults,FunctionID,UserID) VALUES" +
                                    "(@policyNo, @expResults,@funcID, @userID); ";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            command.Parameters.AddWithValue("@policyNo", PScenarioInfo.policyNo);
                            command.Parameters.AddWithValue("@expResults", PScenarioInfo.expectedResults);
                            command.Parameters.AddWithValue("@funcID", PScenarioInfo.functionID);
                            command.Parameters.AddWithValue("@userID", userId);
                            ;

                            command.ExecuteNonQuery();

                        }


                    }
                    PScenarioInfo.policyNo = ""; PScenarioInfo.expectedResults = ""; PScenarioInfo.functionID = "";
                    successMessage = "New Scenario Added Successfully";
                    Redirect("/PolicyServicing");



                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }


            }


        }
    }
    
}
