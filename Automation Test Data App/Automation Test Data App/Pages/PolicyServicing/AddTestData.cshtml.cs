using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Automation_Test_Data_App.Pages.PolicyServicing
{
    public class AddTestDataModel : PageModel
    {
        public void OnGet()
        {
            var scenarioID = Request.Query["scenarioid"].ToString();
            var function = Request.Query["function"].ToString();

            switch (function)
            {
                case "AddRolePlayer":
                    Response.Redirect($"/PolicyServicing/ComponentDowngradeUpgrade/create?scenarioid={scenarioID}");
                    break;

                default:
                    Response.Redirect("/PolicyServicing");
                    break;
            }

        }
    }
}
