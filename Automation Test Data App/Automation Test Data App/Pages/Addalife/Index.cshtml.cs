using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.Addalife
{
    public class IndexModel : PageModel
    {
        public List<AddaLifeInfo> ListAddaLife = new List<AddaLifeInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddaLife";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                AddaLifeInfo addaLifeInfo = new AddaLifeInfo();
                                addaLifeInfo.id =""+ reader.GetString(0);
                                addaLifeInfo.Title = reader.GetString(1);
                                addaLifeInfo.First_Name = reader.GetString(2);
                                addaLifeInfo.Surname = reader.GetString(3);
                                addaLifeInfo.Initials = reader.GetString(4);
                                addaLifeInfo.dob = reader.GetString(5);
                                addaLifeInfo.Gender = reader.GetString(6);
                                addaLifeInfo.ID_number = reader.GetString(7);
                                addaLifeInfo.Relationship = reader.GetString(8);
                                addaLifeInfo.Comm_date = reader.GetString(9);
                                addaLifeInfo.created_at = reader.GetDateTime(10).ToString();

                                ListAddaLife.Add(addaLifeInfo);
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

    public class AddaLifeInfo 
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
        public String created_at;
    }
}
