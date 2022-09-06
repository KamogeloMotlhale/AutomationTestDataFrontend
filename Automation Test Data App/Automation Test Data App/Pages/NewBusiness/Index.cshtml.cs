using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.NewBusiness
{

    
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private string fullPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "UploadFiles";
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
            ViewData["SuccessMessage"] = formFile.FileName.ToString() + " files uploaded!!";
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

