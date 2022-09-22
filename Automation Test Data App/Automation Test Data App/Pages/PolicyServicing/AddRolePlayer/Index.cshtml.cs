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
              String connectionString = "Data Source=(LocalDB)/MSSQLLocalDB;AttachDbFilename=C:/Users/G992107/Documents/Github/ilrsafricanautopolicyservicing/data/Automation.mdf;Integrated Security=True;Connect Timeout=30";
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
                                AddRolePlayerInfo.id =""+ reader.GetInt32(0);
                                AddRolePlayerInfo.Title = reader.GetString(1);
                                AddRolePlayerInfo.First_Name = reader.GetString(2);
                                AddRolePlayerInfo.Surname = reader.GetString(3);
                                AddRolePlayerInfo.Initials = reader.GetString(4);
                                AddRolePlayerInfo.DOB = reader.GetString(5);
                                AddRolePlayerInfo.Gender = reader.GetString(6);
                                AddRolePlayerInfo.ID_number = reader.GetString(7);
                                AddRolePlayerInfo.Relationship = reader.GetString(8);
                                AddRolePlayerInfo.Comm_date = reader.GetString(9);
                                AddRolePlayerInfo.CoverAmount = reader.GetString(10);



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
        public String id;
        public String Title;
        public String First_Name;
        public String Surname;
        public String Initials;
        public String DOB;
        public String Gender;
        public String ID_number;
        public String Relationship;
        public String Comm_date;
        public String CoverAmount;


    }
}
