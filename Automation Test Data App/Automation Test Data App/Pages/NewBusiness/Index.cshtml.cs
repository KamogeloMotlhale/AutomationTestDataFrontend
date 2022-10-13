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
            var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=\"Excel 12.0;HDR=YES;\"";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
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
                        var excellAge = ((System.Data.DataRowView)row).Row.ItemArray[0].ToString();
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