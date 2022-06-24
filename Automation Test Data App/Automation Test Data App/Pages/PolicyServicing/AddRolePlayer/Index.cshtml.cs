using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.AddRolePlayer
{
    public class IndexModel : PageModel
    {
    
    }

    public class AddRolePlayerInfo
    {
        public String id;
        public String Title;
        public String First_Name;
        public String Surname;
        public String Initials;
        public String dob;
        public String Gender;
        public String ID_number;
        public String Relationship;
        public String Comm_date;

    }

}
