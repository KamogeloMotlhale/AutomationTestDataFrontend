using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.ChangelifeData
{
    public class IndexModel : PageModel
    {
        public List<ChangelifeDataInfo> ListChangelifeData = new List<ChangelifeDataInfo>();
    }

    public class ChangelifeDataInfo
    {
        public String id;
        public String Title;
        public String Surname;
        public String MaritalStatus;
        public String EducationLevel;
        public String Department;
        public String Profession;

    }
}
