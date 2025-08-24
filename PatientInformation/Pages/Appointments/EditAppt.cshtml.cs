using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using PatientInformation.Pages.Appointments;

namespace PatientInformation.Pages.Appointments
{
    public class EditApptModel : PageModel
    {
        public ApptInfo apptInfo = new ApptInfo();
        public String errorMessage ="";
        public String successMessage = "";

        public void OnGet()
        {
            String appt_id = Request.Query["appt_id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM appointments WHERE appt_id =@appt_id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@appt_id", appt_id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                apptInfo.appt_id = "" + reader.GetInt32(0);
                                apptInfo.appt_time = reader.GetDateTime(9).ToString();
                                apptInfo.appt_date = reader.GetDateTime(9).ToString();
                                apptInfo.patient_id = reader.GetString(3);
                               

                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            apptInfo.appt_id = Request.Form["appt_id"];
            apptInfo.appt_time = Request.Form["appt_time"];
            apptInfo.appt_date = Request.Form["appt_date"];
            apptInfo.patient_id = Request.Form["patient_id"];
            

            if (apptInfo.appt_time.Length == 0 || apptInfo.appt_date.Length == 0 ||
              apptInfo.patient_id.Length == 0  )
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
                    String sql = "UPDATE appointments " +
                        "SET appt_time= @appt_time, appt_date= @appt_date, patient_id=@patient_id " +
                        "WHERE appt_id=@appt_id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@appt_id", apptInfo.appt_id);
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

            Response.Redirect("/Appointments/Index");
        }
    }
}
