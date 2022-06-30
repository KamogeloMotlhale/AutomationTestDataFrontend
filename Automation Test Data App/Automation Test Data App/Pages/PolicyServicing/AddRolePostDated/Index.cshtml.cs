using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.AddRolePostDated
{
    public class IndexModel : PageModel
    {
        public List<AddRolePlayerInfo> ListAddRolePlayer = new List<AddRolePlayerInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddRolePlayer";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                            while (reader.Read())
                            {

                                AddRolePlayerInfo addRoleplayerInfo = new AddRolePlayerInfo();
                                addRoleplayerInfo.id = "" + reader.GetInt32(0);
                                addRoleplayerInfo.Title = reader.GetString(1);
                                addRoleplayerInfo.First_Name = reader.GetString(2);
                                addRoleplayerInfo.Surname = reader.GetString(3);
                                addRoleplayerInfo.Initials = reader.GetString(4);
                                addRoleplayerInfo.dob = reader.GetString(5);
                                addRoleplayerInfo.Gender = reader.GetString(6);
                                addRoleplayerInfo.ID_number = reader.GetString(7);
                                addRoleplayerInfo.Relationship = reader.GetString(8);
                                addRoleplayerInfo.Comm_date = reader.GetString(9);

                                ListAddRolePlayer.Add(addRoleplayerInfo);
                            }


                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());

            }




        }
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
