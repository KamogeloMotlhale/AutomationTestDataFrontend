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

            if (function.Equals("IncreaseSumAssured") || function.Equals("DecreaseSumAssured"))
            {
                function = "ComponentDowngradeUpgrade";
            }
            try
            {
                Response.Redirect($"/PolicyServicing/{function}/Edit?scenarioid={scenarioID}");
            }
            catch (Exception)
            {

                Response.Redirect("/PolicyServicing");
            }

        }
    }
}
