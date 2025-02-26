using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace PatientInformation.Pages.Patients
{
    public class IndexModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();
        public void OnGet()
        {
            try
            {
              String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False";
                
              using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM patients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PatientInfo patientInfo = new PatientInfo();
                                patientInfo.id = "" + reader.GetInt32(0);
                                patientInfo.firstName = reader.GetString(1);
                                patientInfo.lastName = reader.GetString(2);
                                patientInfo.email = reader.GetString(3);
                                patientInfo.phone = reader.GetString(4);
                                patientInfo.address = reader.GetString(5);
                                patientInfo.city = reader.GetString(6);
                                patientInfo.state = reader.GetString(7);
                                patientInfo.zipCode = reader.GetString(8);
                                patientInfo.creationDate = reader.GetDateTime(9).ToString();

                                listPatients.Add(patientInfo);
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

    public class PatientInfo
    {
        public String id;
        public String firstName;
        public String lastName;
        public string email;
        public string phone;
        public string address;
        public string city;
        public string state;
        public string zipCode;
        public string creationDate;

    }
}
