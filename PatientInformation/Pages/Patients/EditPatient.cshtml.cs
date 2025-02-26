using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace PatientInformation.Pages.Patients
{
    public class EditPatientModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage ="";
        public String successMessage = "";

        public void OnGet()
        {
            String id = Request.Query["id"];

            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=PatientInfo;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM patients WHERE id =@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
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
            patientInfo.id = Request.Form["id"];
            patientInfo.firstName = Request.Form["firstName"];
            patientInfo.lastName = Request.Form["lastName"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];
            patientInfo.city = Request.Form["city"];
            patientInfo.state = Request.Form["state"];
            patientInfo.zipCode = Request.Form["zipCode"];

            if (patientInfo.firstName.Length == 0 || patientInfo.lastName.Length == 0 ||
              patientInfo.email.Length == 0 || patientInfo.phone.Length == 0 ||
              patientInfo.address.Length == 0 || patientInfo.city.Length == 0 ||
              patientInfo.state.Length == 0 || patientInfo.zipCode.Length == 0)
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
                    String sql = "UPDATE patients " +
                        "SET firstName= @firstName, lastName= @lastName, email=@email, phone=@phone, address=@address, city=@city, state=@state, zipCode=@zipCode " +
                        "WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", patientInfo.id);
                        command.Parameters.AddWithValue("@firstName", patientInfo.firstName);
                        command.Parameters.AddWithValue("@lastName", patientInfo.lastName);
                        command.Parameters.AddWithValue("@email", patientInfo.email);
                        command.Parameters.AddWithValue("@phone", patientInfo.phone);
                        command.Parameters.AddWithValue("@address", patientInfo.address);
                        command.Parameters.AddWithValue("@city", patientInfo.city);
                        command.Parameters.AddWithValue("@state", patientInfo.state);
                        command.Parameters.AddWithValue("@zipCode", patientInfo.zipCode);

                        command.ExecuteNonQuery();




                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Patients/Index");
        }
    }
}
