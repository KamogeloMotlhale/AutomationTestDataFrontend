using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{
    public class EditScenarioModel : PageModel
    {

        public PScenarioInfo pScenarioInfo = new PScenarioInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public List<Function> funcList = new List<Function>();
        public String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
        public void OnGet()
        {
            String id = Request.Query["scenarioid"];
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                try
                {
                    
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
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.ToString());

                }
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = $"SELECT * FROM PS_Scenarios WHERE ID={id}";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            if (reader.Read() && (reader["UserID"].ToString()).Equals(userId))
                            {
                               
                                    pScenarioInfo.dateCreated = reader["Created_at"].ToString();
                                    pScenarioInfo.id = reader["ID"].ToString();
                                    pScenarioInfo.policyNo = reader["PolicyNo"].ToString();
                                    pScenarioInfo.expectedResults = reader["ExpectedResults"].ToString();
                                    pScenarioInfo.testDate = reader["Test_Date"].ToString();
                                    pScenarioInfo.testResutls = reader["Test_Results"].ToString();
                                    pScenarioInfo.comments = reader["Comments"].ToString();
                                    pScenarioInfo.functionID = reader["functionID"].ToString();
                                    pScenarioInfo.productName = reader["productName"].ToString();
                                    pScenarioInfo.getFuncName();
                        
                            }
                        }
                    }
                    connection.Close();
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
            string userId = Request.Cookies["UserID"];
            
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                pScenarioInfo.policyNo = Request.Form["policyNo"];
                pScenarioInfo.functionID = Request.Form["function"];
                pScenarioInfo.expectedResults = Request.Form["expResutlts"];
               


                if (pScenarioInfo.policyNo.Length == 0
                    || pScenarioInfo.expectedResults.Length == 0
                    || pScenarioInfo.functionID.Length == 0)
                {
                    errorMessage = "All the fields are required";
                    return;

                }
                //save to DB
                try
                {

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        String sql = "UPDATE PS_Scenarios " +
                                     "SET   PolicyNo=@policyNo,ExpectedResults=@expResults, FunctionID=@funcID" + 
                                     " WHERE ID=@id";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {

                            command.Parameters.AddWithValue("@policyNo", pScenarioInfo.policyNo);
                            command.Parameters.AddWithValue("@expResults", pScenarioInfo.expectedResults);
                            command.Parameters.AddWithValue("@funcID", pScenarioInfo.functionID);
                            command.Parameters.AddWithValue("@id", id);
                           
                            command.ExecuteNonQuery();
                        }
                        pScenarioInfo.policyNo = ""; pScenarioInfo.testResutls = "";pScenarioInfo.functionID = "";
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
}
