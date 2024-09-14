using Lab4_1.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Lab4_1
{
    public partial class PatientDataWindow : Window
    {

        private int patientID;

        public PatientDataWindow(Patient patient)
        {
            InitializeComponent();

            populateControlsWithPatientData(patient);

            this.patientID = patient.Id;
        }

        private void populateControlsWithPatientData(Patient patient)
        {
            if (patient != null)
            {
                oibTextBox.Text = patient.OIB;
                mboTextBox.Text = patient.MBO;
                nameTextBox.Text = patient.Name;
                surnameTextBox.Text = patient.Surname;
                dateOfBirthDatePicker.SelectedDate = patient.DateOfBirth;
                dateOfPatientAdmissionPicker.SelectedDate = patient.DateOfPatientAdmission;
                dateOfPatientDischargePicker.SelectedDate = patient.DateOfPatientDischarge;
                genderComboBox.Text = patient.Gender;
                diagnosisComboBox.Text = patient.Diagnosis;
            }
        }

        private async void UpdatePatientSubmit_Click(object sender, RoutedEventArgs e)
        {
            Patient updatedPatient = new Patient
            {
                OIB = oibTextBox.Text,
                MBO = mboTextBox.Text,
                Name = nameTextBox.Text,
                Surname = surnameTextBox.Text,
                DateOfBirth = dateOfBirthDatePicker.SelectedDate ?? DateTime.MinValue,
                DateOfPatientAdmission = dateOfPatientAdmissionPicker.SelectedDate ?? DateTime.MinValue,
                DateOfPatientDischarge = dateOfPatientDischargePicker.SelectedDate ?? DateTime.MinValue,
                Gender = (genderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Diagnosis = (diagnosisComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };


            string patientJson = JsonConvert.SerializeObject(updatedPatient);

            using var client = new HttpClient();

            var content = new StringContent(patientJson, Encoding.UTF8, "application/json");

            string url = "https://localhost:7192/api/Patient/" + patientID;

            HttpResponseMessage response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Patient updated successfully!");
            }


            this.Close();
        }

    }
}
