using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace PatientInformation.Pages.Patients
{
    public class CreatePatientModel : PageModel
    {
        public PatientInfo patientInfo = new PatientInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            patientInfo.firstName = Request.Form["firstName"];
            patientInfo.lastName = Request.Form["lastName"];
            patientInfo.email = Request.Form["email"];
            patientInfo.phone = Request.Form["phone"];
            patientInfo.address = Request.Form["address"];
            patientInfo.city = Request.Form["city"];
            patientInfo.state = Request.Form["state"];
            patientInfo.zipCode = Request.Form["zipCode"];


            if(patientInfo.firstName.Length == 0 || patientInfo.lastName.Length==0 ||
               patientInfo.email.Length==0 || patientInfo.phone.Length==0 ||
               patientInfo.address.Length==0 || patientInfo.city.Length==0 ||
               patientInfo.state.Length==0 || patientInfo.zipCode.Length==0)
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
                    String sql = "INSERT INTO patients " +
                        "(firstName,lastName, email, phone, address, city, state, zipCode) VALUES" +
                        "(@firstName, @lastName, @email, @phone, @address,@city, @state, @zipCode);";

                    using(SqlCommand command = new SqlCommand(sql, connection))
                    {
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

            patientInfo.firstName = "";
            patientInfo.lastName = "";
            patientInfo.email = "";
            patientInfo.phone = "";
            patientInfo.address = "";
            patientInfo.city = "";
            patientInfo.state = "";
            patientInfo.zipCode = "";

            successMessage = "Patient Added!";

            Response.Redirect("/Patients/Index");
        }
    }
}
