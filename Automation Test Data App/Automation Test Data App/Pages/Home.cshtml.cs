using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Automation_Test_Data_App.Pages
{
    public class HomeModel : PageModel
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
