using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using PatientInformation.Pages.Patients;

namespace PatientInformation.Pages.Appointments
{
   
    public class CreateApptModel : PageModel
    {
        public List<PatientInfo> listPatients = new List<PatientInfo>();

        public ApptInfo apptInfo = new ApptInfo();
        public String errorMessage = "";
        public String successMessage = "";
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
        

        public void OnPost()
        {
            apptInfo.appt_time = Request.Form["appt_time"];
            apptInfo.appt_date = Request.Form["appt_date"];
            apptInfo.patient_id = Request.Form["patient_id"];
            
           


            if(apptInfo.appt_time.Length == 0 || apptInfo.appt_date.Length==0 ||
               apptInfo.patient_id.Length==0 )
            {
                @errorMessage = "Please fill all required fields!";
                return;
            }

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO appointments " +
                        "(appt_time,appt_date, patient_id) VALUES" +
                        "(@appt_time, @appt_date, @patient_id);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@appt_time", apptInfo.appt_time);
                        command.Parameters.AddWithValue("@appt_date", apptInfo.appt_date);
                        command.Parameters.AddWithValue("@patient_id", apptInfo.patient_id);


                        command.ExecuteNonQuery();


                    }

                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            apptInfo.appt_time = "";
            apptInfo.appt_date = "";
            apptInfo.patient_id = "";
            

            successMessage = "Appointment Scheduled!";

            Response.Redirect("/Appointments/Index");
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
