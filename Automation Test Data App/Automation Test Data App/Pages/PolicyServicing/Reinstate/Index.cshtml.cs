using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Automation_Test_Data_App.Pages.PolicyServicing.Reinstate
{
    public class IndexModel : PageModel
    { 
    public List<ReinstateInfo> ListReinstate = new List<ReinstateInfo>();
    public void OnGet()
    {
        try
        {
            String connectionString = "Data Source='SRV007232, 1455';Initial Catalog=Automation;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                String sql = "SELECT * FROM Reinstate ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                        while (reader.Read())
                        {

                                ReinstateInfo ReinstateInfo = new ReinstateInfo();
                                ReinstateInfo.id = "" + reader.GetInt32(0);
                                ReinstateInfo.PolicyNo = reader.GetString(1);
                                ReinstateInfo.ReinstatementReason = reader.GetString(2);
                                ReinstateInfo.ReinstatementDate = reader.GetString(3);



                                ListReinstate.Add(ReinstateInfo);
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
public class ReinstateInfo
    {
        public String id;
        public String PolicyNo;
        public String ReinstatementReason;
        public String ReinstatementDate;
  

    }
}
