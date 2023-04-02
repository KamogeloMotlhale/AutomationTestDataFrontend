using Automation_Test_Data_App.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages
{
    public class DataMappingModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "UploadedFiles";
        public DataMappingModel(ILogger<IndexModel> logger)
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
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=\"Excel 12.0;HDR=NO;\"";
            String dbconnectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
            OleDbConnection conn = new OleDbConnection(connectionString);

            try
            {
                // Open connection
                conn.Open();
                string cmdQuery = $"SELECT * FROM [Serenity_Funeral_Plan_(2000)$]";
                OleDbCommand cmd = new OleDbCommand(cmdQuery, conn);
                // Create new OleDbDataAdapter
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                oleda.SelectCommand = cmd;
                // Create a DataSet which will hold the data extracted from the worksheet.
                DataSet ds = new DataSet();
                // Fill the DataSet from the data extracted from the worksheet.
                oleda.Fill(ds, "Policies");
                var colCount = ds.Tables[0].Columns.Count;
                var rowCount = 0;
                var insertString = "INSERT INTO Serenity_Funeral_Plan_2000 (";
                var createString = "CREATE TABLE Serenity_Funeral_Plan_2000 (";
                var dataString = "";
                foreach (var row in ds.Tables[0].DefaultView)
                {
                    if (rowCount == 0)
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            insertString = insertString + $"{((System.Data.DataRowView)row).Row.ItemArray[i].ToString()}";
                            createString = createString + $"{((System.Data.DataRowView)row).Row.ItemArray[i].ToString()} varchar(225)";
                            if (i == colCount - 1)
                            {
                                insertString = insertString + ") VALUES ";
                                createString = createString + ");";
                            }
                            else
                            {
                                insertString = insertString + ",";
                                createString = createString + ",";
                            }
                        }
                        //Create table

                        DbConnection.removeCreateUpdateDataOnDB(createString);
                        DbConnection.closeDbConnection();
                    }
                    else
                    {
                        for (int i = 0; i < colCount; i++)
                        {
                            if (i == 0) {
                                dataString = "("; 
                            }
                            dataString = dataString + $"'{((System.Data.DataRowView)row).Row.ItemArray[i].ToString()}'";
                            if(i == colCount - 1)
                            {
                                dataString = dataString + ")";
                            }
                            else
                            {
                                dataString = dataString + ",";
                            }


                        }
                        var insertCmd =  insertString + dataString;
                        DbConnection.removeCreateUpdateDataOnDB(insertCmd);
                        DbConnection.closeDbConnection();
                    }
                  
                    rowCount++;

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
        public class FileUpload
        {
            [Required]
            [Display(Name = "File")]
            public IFormFile FormFile { get; set; }
            public string SuccessMessage { get; set; }
        }
    }
}
