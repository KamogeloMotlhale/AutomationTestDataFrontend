using Automation_Test_Data_App.Pages.PolicyServicing;
using Automation_Test_Data_App.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Claims
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "UploadedFiles";
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public FileUpload fileUpload { get; set; }

        public List<PScenarioInfo> ListPsScenarios = new List<PScenarioInfo>();
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

                    SqlDataReader reader = DbConnection.readDataFromDB($"SELECT * FROM TestScenarios WHERE UserID = '{userId}' AND ProjectID = 2 ORDER BY Created_at DESC;");


                    while (reader.Read())
                    {
                        PScenarioInfo pScenarioInfo = new PScenarioInfo();
                        pScenarioInfo.id = reader["ID"].ToString();
                        pScenarioInfo.policyNo = reader["PolicyNo"].ToString();
                        pScenarioInfo.expectedResults = reader["ExpectedResults"].ToString();
                        pScenarioInfo.testDate = reader["Test_Date"].ToString();
                        pScenarioInfo.testResutls = reader["Test_Results"].ToString();
                        pScenarioInfo.comments = reader["Comments"].ToString();
                        pScenarioInfo.functionID = reader["functionID"].ToString();
                        pScenarioInfo.scenario_details = reader["functionID"].ToString();
                        ListPsScenarios.Add(pScenarioInfo);
                    }
                    DbConnection.closeDbConnection();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception:" + ex.ToString());

                }
                return;
            }
        }

        public IActionResult OnPostUpload(FileUpload fileUpload)
        {
            //Get Uploading User
            string userId = Request.Cookies["UserID"];


            //Creating upload folder  
            fullPath += $"/{userId}";
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            var formFile = fileUpload.FormFile;
            var filePath = Path.Combine(fullPath, formFile.FileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                formFile.CopyToAsync(stream);
            }
            // Process uploaded files  
            // Don't rely on or trust the FileName property without validation.
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=\"Excel 12.0;HDR=YES;\"";
            string testDataConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=\"Excel 12.0;HDR=NO;\"";
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbConnection testDataConn = new OleDbConnection(testDataConnectionString);
            // SqlConnection DBconnection = new SqlConnection(connectionString);

            try
            {
                // Open connection
                conn.Open();
                string cmdQuery = $"SELECT * FROM [Scenarios$]";
                OleDbCommand cmd = new OleDbCommand(cmdQuery, conn);
                // Create new OleDbDataAdapter
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();
                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds, "Policies");
                foreach (var row in ds.Tables[0].DefaultView)
                {
                    var scenario_id = ((System.Data.DataRowView)row).Row.ItemArray[0].ToString();
                    var policy_no = ((System.Data.DataRowView)row).Row.ItemArray[1].ToString();
                    var function = ((System.Data.DataRowView)row).Row.ItemArray[2].ToString();
                    var scenariosDeatils = ((System.Data.DataRowView)row).Row.ItemArray[3].ToString();
                    var expectedResults = ((System.Data.DataRowView)row).Row.ItemArray[4].ToString();
                    //Save scenario to DB
                    var funID = DbConnection.getFunctionID(function);


                    DbConnection.removeCreateUpdateDataOnDB("INSERT INTO TestScenarios" +
                                    "(PolicyNo,ExpectedResults,FunctionID,UserID, ProjectID, Scenario_Details ) VALUES " +
                                    $"('{policy_no}', '{expectedResults}',{funID},'{userId}',2,'{scenariosDeatils}');");
                    DbConnection.closeDbConnection();
                    //Get the DB ID of the recently added scenario
                    var dbScenarioID = String.Empty;
                    //TODO Multiple uploads at the same time might cuase troubles
                    SqlDataReader reader = DbConnection.readDataFromDB($"SELECT TOP 1 * FROM TestScenarios Order By Created_at Desc");
                    while (reader.Read())
                    {
                        dbScenarioID = reader["ID"].ToString();
                        if (!String.IsNullOrEmpty(dbScenarioID))
                        {
                            break;
                        }

                    }
                    DbConnection.closeDbConnection();



                    //Get  test data from corresponding data table

                    List<String> tblList = new List<String>();
                    tblList.Add("ClaimDetails_Data");
                    tblList.Add("BankDetails");

                    foreach (var item in tblList)
                    {
                        testDataConn.Open();
                        cmdQuery = $"SELECT * FROM [{item}$]";
                        cmd = new OleDbCommand(cmdQuery, testDataConn);
                        oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;
                        // Create a DataSet which will hold the data extracted from the worksheet.
                        DataSet testds = new DataSet();
                        // Fill the DataSet from the data extracted from the worksheet.
                        oleda.Fill(testds, "TestData");
                        int counter = 0;
                        String fields = "";

                        foreach (var testDataRow in testds.Tables[0].DefaultView)
                        {
                            String testData = $"{dbScenarioID},";
                            var colNum = ((System.Data.DataRowView)testDataRow).Row.ItemArray.Length;
                            for (int i = 0; i < colNum; i++)
                            {
                                if (counter == 0)
                                {
                                    var field = ((System.Data.DataRowView)testDataRow).Row.ItemArray[i].ToString();
                                    fields += $" {field}";
                                    if (i < colNum - 1)
                                    {
                                        fields += ",";
                                    }
                                }
                                else
                                {
                                    //Check if I
                                    if (((System.Data.DataRowView)testDataRow).Row.ItemArray[0].ToString().Equals(scenario_id))
                                    {

                                        if (i == colNum - 1)
                                        {
                                            testData += $"";
                                        }
                                        else
                                        {
                                            testData += $" '{((System.Data.DataRowView)testDataRow).Row.ItemArray[i + 1].ToString()}'";
                                        }
                                        if (i < colNum - 2)
                                        {
                                            testData += ",";
                                        }
                                    }
                                }
                            }
                            testDataConn.Close();
                            // write to DB if scenarioID Matches
                            if (testData.Length > 4)
                            {
                                //Set test data if there is any
                                var sqlQuery = $"INSERT INTO {item} ({fields}) VALUES({testData})";
                                DbConnection.removeCreateUpdateDataOnDB(sqlQuery);
                                DbConnection.closeDbConnection();
                                break;
                            }
                            counter++;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

                conn.Close();
                conn.Dispose();
            }
            // Delete file
            // Check if file exists with its full path    
            if (System.IO.File.Exists(Path.Combine(filePath)))
            {
                // If file found, delete it    
                System.IO.File.Delete(filePath);

            }
            Response.Redirect($"/Claims/");
            return Page();
        }
    }
}
