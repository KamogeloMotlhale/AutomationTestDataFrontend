using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.AddRolePostDated
{
    public class IndexModel : PageModel
    {
        public List<AddRolePostDatedInfo> ListAddRolePostDated = new List<AddRolePostDatedInfo>();
        public void OnGet()
        {
            try
            { 
              String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection (connectionString))
                {

                    connection.Open();
                    String sql = "SELECT * FROM AddRolePostDated";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        while(reader.Read())
                         {

                                AddRolePostDatedInfo AddRolePostDatedInfo = new AddRolePostDatedInfo();
                                AddRolePostDatedInfo.id =""+ reader.GetInt32(0);
                                AddRolePostDatedInfo.Title = reader.GetString(1);
                                AddRolePostDatedInfo.First_Name = reader.GetString(2);
                                AddRolePostDatedInfo.Surname = reader.GetString(3);
                                AddRolePostDatedInfo.Initials = reader.GetString(4);
                                AddRolePostDatedInfo.DOB = reader.GetString(5);
                                AddRolePostDatedInfo.Gender = reader.GetString(6);
                                AddRolePostDatedInfo.ID_number = reader.GetString(7);
                                AddRolePostDatedInfo.Relationship = reader.GetString(8);
                                AddRolePostDatedInfo.Comm_date = reader.GetString(9);
                                AddRolePostDatedInfo.CoverAmount = reader.GetString(10);



                                ListAddRolePostDated.Add(AddRolePostDatedInfo);
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

    public class AddRolePostDatedInfo
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
