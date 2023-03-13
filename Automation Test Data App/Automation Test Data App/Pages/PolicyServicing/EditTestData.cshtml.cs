using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{
    public class EditTestDataModel : PageModel
    {
       
        public void OnGet()
        {
            var scenarioID = Request.Query["scenarioid"].ToString();
            var function = Request.Query["function"].ToString();

            switch (function)
            {
                case "IncreaseSumAssured":
                    Response.Redirect($"/PolicyServicing/ComponentDowngradeUpgrade/Edit?scenarioid={scenarioID}");
                    break;

                default:
                    Response.Redirect("/PolicyServicing");
                    break;
            }

        }
    }
}
