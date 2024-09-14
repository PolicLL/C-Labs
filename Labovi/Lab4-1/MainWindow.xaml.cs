using Lab4_1.Entities;
using Lab4_1.Util;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Lab4_1
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        // Event handler for the "Send Data" button click
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            // Validate input values
            if (!ValidateInput())
            {
                return; // Validation failed, exit the method
            }


            Patient newPatient = new Patient
            {
                OIB = oibTextBox.Text,
                MBO = mboTextBox.Text,
                Name = nameTextBox.Text,
                Surname = surnameTextBox.Text,
                DateOfBirth = dateOfBirthDatePicker.SelectedDate ?? DateTime.MinValue,
                DateOfPatientAdmission = dateOfPatientAdmissionPicker.SelectedDate ?? DateTime.MinValue,
                DateOfPatientDischarge = DateTime.MinValue,
                Gender = (genderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Diagnosis = (diagnosisComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            // TODO -> Add Patient 

            string patientJson = JsonConvert.SerializeObject(newPatient);

            Console.WriteLine("Patient JSON : " + patientJson);

            using var client = new HttpClient();

            var content = new StringContent(patientJson, Encoding.UTF8, "application/json");

            string url = "https://localhost:7192/api/Patient/";

            HttpResponseMessage response = await client.PostAsync(url, content);

            Console.WriteLine("Making POST request.");
            Console.WriteLine("Content : " + content);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Patient added successfully!");
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Patient was not added successfully!");
            }
        }

        private async void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            string patientId = PromptUserForPatientId();

            if (!string.IsNullOrEmpty(patientId))
            {

                using var client = new HttpClient();

                string url = "https://localhost:7192/api/Patient/" + patientId;

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {

                    string responseData = await response.Content.ReadAsStringAsync();

                    Patient patient = JsonConvert.DeserializeObject<Patient>(responseData);

                    PatientDataWindow patientdatawindow = new PatientDataWindow(patient);
                    patientdatawindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Patient was not updated successfully!");
                }
            }
        }

        private string PromptUserForPatientId()
        {
            InputDialog inputDialog = new InputDialog("Enter Patient ID:");
            if (inputDialog.ShowDialog() == true)
            {
                return inputDialog.Answer;
            }
            else
            {
                return null;
            }
        }



        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllPatientsAsync();
        }

        private async Task DisplayAllPatientsAsync()
        {
            using var client = new HttpClient();

            string url = "https://localhost:7192/api/Patient";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to list of Patient objects
                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(responseData);

                CustomMessageBox customMessageBox = new CustomMessageBox(PatientUtils.GetAllPatientInformation(patients));
                customMessageBox.ShowDialog();

            }
            else
            {
                MessageBox.Show("Patients cannot be retrieved.");
            }
        }

        private void ShowActivePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllActivePatientsAsync();
        }

        private async Task DisplayAllActivePatientsAsync()
        {
            using var client = new HttpClient();

            string url = "https://localhost:7192/api/Patient";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to list of Patient objects
                List<Patient> patients = JsonConvert.DeserializeObject<List<Patient>>(responseData);

                CustomMessageBox customMessageBox = new CustomMessageBox(PatientUtils.GetAllActivePatientsInformation(patients));
                customMessageBox.ShowDialog();

            }
            else
            {
                MessageBox.Show("Active patients cannot be retrieved.");
            }
        }



        private void ClearInputFields()
        {
            oibTextBox.Clear();
            mboTextBox.Clear();
            nameTextBox.Clear();
            surnameTextBox.Clear();
            dateOfBirthDatePicker.SelectedDate = null;
            dateOfPatientAdmissionPicker.SelectedDate = null;
            genderComboBox.SelectedIndex = -1;
            diagnosisComboBox.SelectedIndex = -1;
        }

        private bool ValidateInput()
        {
            if (!Regex.IsMatch(oibTextBox.Text, "^[0-9]*$") || oibTextBox.Text.Length != 11)
            {
                MessageBox.Show("OIB must be numeric and 11 characters long.");
                return false;
            }

            if (!Regex.IsMatch(mboTextBox.Text, "^[0-9]*$") || mboTextBox.Text.Length != 9)
            {
                MessageBox.Show("MBO must be numeric and 9 characters long.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(nameTextBox.Text))
            {
                MessageBox.Show("Name cannot be empty.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(surnameTextBox.Text))
            {
                MessageBox.Show("Surname cannot be empty.");
                return false;
            }

            if (genderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a gender.");
                return false;
            }

            if (dateOfBirthDatePicker.SelectedDate == null || dateOfBirthDatePicker.SelectedDate > DateTime.Today)
            {
                MessageBox.Show("Date of Birth cannot be in the future.");
                return false;
            }

            if (dateOfPatientAdmissionPicker.SelectedDate == null || dateOfPatientAdmissionPicker.SelectedDate > DateTime.Today)
            {
                MessageBox.Show("Date of Admission cannot be in the future.");
                return false;
            }


            if (diagnosisComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a diagnosis.");
                return false;
            }

            return true;
        }

    }
}
