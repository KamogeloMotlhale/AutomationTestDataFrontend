using Automation_Test_Data_App.Pages.Shared;
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


                    SqlDataReader reader = DbConnection.readDataFromDB($"SELECT * FROM Functions");

                 
                      
                                while (reader.Read())
                                {
                                    Function func = new Function();

                                    func.id = reader["ID"].ToString();
                                    func.name = reader["function_name"].ToString();
                                    funcList.Add(func);
                                }
                         DbConnection.closeDbConnection();

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


                    DbConnection.removeCreateUpdateDataOnDB("INSERT INTO TestScenarios" +
                                    "(PolicyNo,ExpectedResults,FunctionID,UserID, ProjectID) VALUES " +
                                    $"('{PScenarioInfo.policyNo}', '{PScenarioInfo.expectedResults}',{PScenarioInfo.functionID},'{userId}',1);");
                    DbConnection.closeDbConnection();
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
