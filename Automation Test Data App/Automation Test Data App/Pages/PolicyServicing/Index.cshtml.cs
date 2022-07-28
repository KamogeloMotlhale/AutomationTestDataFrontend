using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            string userId = Request.Cookies["UserID"];
            if (userId == null)
            {
                Response.Redirect("/");
            }
            else
            {
                return;

            }
        }
    }
}
