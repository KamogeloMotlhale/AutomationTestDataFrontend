using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Downgrade
{
    public class IndexModel : PageModel
    {
    }


    public class DowngradeInfo
    {
        public String id;
        public String Date;
        public String Component;
        public String Cover_Amount;
  

    }
}
