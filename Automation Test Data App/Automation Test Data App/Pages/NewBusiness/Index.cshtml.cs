using Automation_Test_Data_App.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace Automation_Test_Data_App.Pages.NewBusiness
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
        public void OnGet()
        {
            ViewData["SuccessMessage"] = "";
        }
        public IActionResult OnPostUpload(FileUpload fileUpload)
        {
            //Creating upload folder  
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

            String dbconnectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
            string testDataConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=\"Excel 12.0;HDR=NO;\"";
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbConnection testDataConn = new OleDbConnection(testDataConnectionString);
            // SqlConnection DBconnection = new SqlConnection(connectionString);
            string userId = Request.Cookies["UserID"];
            
   
            List<string> dataTables = new List<string>();
            dataTables.Add("AddRolePlayer");

            try
                {
                    // Open connection
                    conn.Open();
                    string cmdQuery = $"SELECT * FROM [PS_Scenarios$]";
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
                    DbConnection.removeCreateUpdateDataOnDB("INSERT INTO PS_Scenarios" +
                                    "(PolicyNo,ExpectedResults,FunctionID,UserID) VALUES " +
                                    $"('{policy_no}', '{expectedResults}',{funID},'{userId}');");
                    DbConnection.closeDbConnection();

                    var dbScenarioID = String.Empty;
                    SqlDataReader reader = DbConnection.readDataFromDB($"SELECT TOP 1 * FROM PS_Scenarios");
                    while (reader.Read())
                    {
                        dbScenarioID = reader["ID"].ToString();
                    }
                    DbConnection.closeDbConnection();

                    //Get  test data from corresponding data table
                    foreach (var table in dataTables)
                    {
                        testDataConn.Open();
                        cmdQuery = $"SELECT * FROM [{table}$]";
                        cmd = new OleDbCommand(cmdQuery, testDataConn);
                        oleda = new OleDbDataAdapter();
                        oleda.SelectCommand = cmd;
                        // Create a DataSet which will hold the data extracted from the worksheet.
                        DataSet testds = new DataSet();
                        // Fill the DataSet from the data extracted from the worksheet.
                        oleda.Fill(testds, "TestData");
                        int counter = 0;
                        String fields = "";
                        String testData = $"{dbScenarioID},";
                        bool hasScenarioId =  false;
                        foreach (var testDataRow in testds.Tables[0].DefaultView)
                        {
                            var colNum = ((System.Data.DataRowView)testDataRow).Row.ItemArray.Length;
                                for (int i = 0; i < colNum; i++)
                                {
                                    if (counter == 0)
                                    {
                                            fields += $" {((System.Data.DataRowView)testDataRow).Row.ItemArray[i].ToString()}";
                                            if(i < colNum-1)
                                            {
                                                fields += ",";
                                            }
                                    }
                                    else
                                    {
                                        if (((System.Data.DataRowView)testDataRow).Row.ItemArray[0].ToString().Equals(scenario_id))
                                        {
                                            hasScenarioId = true;
                                            if(i == colNum - 1)
                                            {
                                                testData += $"";
                                            }
                                            else
                                            {
                                                testData += $" '{((System.Data.DataRowView)testDataRow).Row.ItemArray[i + 1].ToString()}'";
                                            }
                                            if (i < colNum-2)
                                            {
                                                testData += ",";
                                            }
                                         }
                                    }
                                }
                            counter++;
                        }
                        testDataConn.Close();


                        if (hasScenarioId)
                        {
                            dataTables.Remove(table);
                        }
                        //Set test data if there is any
                        var sqlQuery = $"INSERT INTO {table} ({fields}) VALUES({testData})";
                        DbConnection.removeCreateUpdateDataOnDB(sqlQuery);
                        DbConnection.closeDbConnection();


                    }


                }
                }
                catch (Exception ex)
                {
                    throw ex;
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
             
        

            return Page();
        }
    }
    public class FileUpload
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
        public string SuccessMessage { get; set; }
    }
}