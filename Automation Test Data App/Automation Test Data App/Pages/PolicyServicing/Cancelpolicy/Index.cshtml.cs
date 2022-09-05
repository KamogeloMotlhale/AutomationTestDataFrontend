using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Cancelpolicy
{
    public class IndexModel : PageModel
    {
        
    public List<CancelpolicyInfo> ListCancelpolicy = new List<CancelpolicyInfo>();
    public void OnGet()
    {
        try
        {
            String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                String sql = "SELECT * FROM Cancelpolicy ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {

                                CancelpolicyInfo CancelpolicyInfo = new CancelpolicyInfo();
                                CancelpolicyInfo.id = "" + reader.GetInt32(0);
                                CancelpolicyInfo.PolicyNo = reader.GetString(1);
                                CancelpolicyInfo.TerminationDate = reader.GetString(2);
                                CancelpolicyInfo.Reason = reader.GetString(3);



                                ListCancelpolicy.Add(CancelpolicyInfo);
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
public class CancelpolicyInfo
    {
        public String id;
        public String PolicyNo;
        public String TerminationDate;
        public String Reason;
  

    }
}
