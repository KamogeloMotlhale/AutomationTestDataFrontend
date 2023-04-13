using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation_Test_Data_App.Pages.Claims
{
    public class AddTestDataModel : PageModel
    {
		public String errorMessage = "";
		public String successMessage = "";
		public String scenarioID = "";
        public ClaimsData cLaimsData = new ClaimsData();
		public void OnGet()
        {
        }
    }
}
