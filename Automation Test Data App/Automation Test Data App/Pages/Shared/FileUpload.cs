using System.ComponentModel.DataAnnotations;

namespace Automation_Test_Data_App.Pages.Shared
{
    public class FileUpload
    {

        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
        public string SuccessMessage { get; set; }
    }
}
