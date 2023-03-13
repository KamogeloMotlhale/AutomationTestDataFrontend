 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.AddRolePlayer
{
    public class IndexModel : PageModel
    {
        public List<AddRolePlayerInfo> ListAddRolePlayer = new List<AddRolePlayerInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                ;
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddRolePlayer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {
                                AddRolePlayerInfo AddRolePlayerInfo = new AddRolePlayerInfo();
                                AddRolePlayerInfo.Scenario_ID = String.Empty + reader["Scenario_ID"].ToString();
                                AddRolePlayerInfo.Title = String.Empty + reader["Title"].ToString();
                                AddRolePlayerInfo.First_Name = String.Empty + reader["First_Name"].ToString();
                                AddRolePlayerInfo.Surname = String.Empty + reader["Surname"].ToString();
                                AddRolePlayerInfo.Initials = String.Empty + reader["Inititials"].ToString();
                                AddRolePlayerInfo.DOB = String.Empty + reader["DOB"].ToString();
                                AddRolePlayerInfo.Gender = String.Empty + reader["Gender"].ToString();
                                AddRolePlayerInfo.ID_number = String.Empty + reader["ID_number"].ToString();
                                AddRolePlayerInfo.Relationship = String.Empty + reader["Relationship"].ToString();
                                AddRolePlayerInfo.Comm_date = String.Empty + reader["Comm_date"].ToString();
                                AddRolePlayerInfo.Sum_Assured = String.Empty + reader["Sum_Assured"].ToString();
                                AddRolePlayerInfo.RolePlayerType = String.Empty + reader["RolePlayerType"].ToString();
                                ListAddRolePlayer.Add(AddRolePlayerInfo);
                         }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }




        }
    }

    public class AddRolePlayerInfo
    {
        public String Scenario_ID;
        public String Title;
        public String First_Name;
        public String Surname;
        public String Initials;
        public String DOB;
        public String Gender;
        public String ID_number;
        public String Relationship;
        public String Comm_date;
        public String Sum_Assured;
        public String RolePlayerType;


    }
}
