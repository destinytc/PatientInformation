using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace PatientInformation.Pages.Appointments
{
    public class IndexModel : PageModel
    {
        public List<ApptInfo> listAppts = new List<ApptInfo>();
        public void OnGet()
        {
            try
            {
              String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False";
                
              using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM appointments";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ApptInfo apptInfo = new ApptInfo();
                                apptInfo.appt_id = "" + reader.GetInt32(0);
                                apptInfo.appt_time = reader.GetTimeSpan(1).ToString();
                                apptInfo.appt_date = reader.GetDateTime(2).ToString();
                                apptInfo.patient_id = "" + reader.GetInt32(3);
                               
                           
                                listAppts.Add(apptInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class ApptInfo
    {
        public String appt_id;
        public String appt_time;
        public String appt_date;
        public String patient_id;
        

    }
}
