using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
    public partial class MainWindow : Window
    {

        private Hospital hospital = new Hospital();

        public MainWindow()
        {
            InitializeComponent();

            InitializeValues();
        }

        private void InitializeValues()
        {
            Console.WriteLine("Initial values.");
            hospital.AddPatient(new Patient("12312312312", "907829345", "Pera", "Peric", new DateTime(), new DateTime(2020, 10, 20), new DateTime(2020, 11, 20), "Male", "A01"));
            hospital.AddPatient(new Patient("52334563423", "867893457", "John", "Doe", new DateTime(), new DateTime(2023, 1, 20), null, "Female", "B02"));
            hospital.AddPatient(new Patient("58748953345", "893414234", "Mike", "Smith", new DateTime(), new DateTime(2021, 5, 20), new DateTime(2021, 11, 20), "Female", "A03"));
            hospital.AddPatient(new Patient("68934535345", "568974589", "Johnny", "Trevis", new DateTime(), new DateTime(2021, 2, 20), null, "Male", "C01"));
        }

       

        // Event handler for the "Send Data" button click
        private void SendButton_Click(object sender, RoutedEventArgs e)
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
                DateOfPatientDischarge = dateOfPatientDischargePicker.SelectedDate ?? DateTime.MinValue,
                Gender = (genderComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                Diagnosis = (diagnosisComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            hospital.AddPatient(newPatient);

            ClearInputFields();

            MessageBox.Show("Patient added successfully!");
        }

        private void UpdatePatient_Click(object sender, RoutedEventArgs e)
        {
            string patientId = PromptUserForPatientId();

            if (!string.IsNullOrEmpty(patientId))
            {
                Patient tempPatient = hospital.GetPatientById(int.Parse(patientId));
                PatientDataWindow patientDataWindow = new PatientDataWindow(hospital, tempPatient);
                patientDataWindow.ShowDialog();
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
            DisplayAllPatients();
        }

        private void DisplayAllPatients()
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(hospital.GetAllPatientInformation());
            customMessageBox.ShowDialog();
        }

        private void ShowActivePatientsButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayAllActivePatients();
        }

        private void DisplayAllActivePatients()
        {
            CustomMessageBox customMessageBox = new CustomMessageBox(hospital.GetAllActivePatientsInformation());
            customMessageBox.ShowDialog();
        }



        private void ClearInputFields()
        {
            oibTextBox.Clear();
            mboTextBox.Clear();
            nameTextBox.Clear();
            surnameTextBox.Clear();
            dateOfBirthDatePicker.SelectedDate = null;
            dateOfPatientAdmissionPicker.SelectedDate = null;
            dateOfPatientDischargePicker.SelectedDate = null;
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

            if (dateOfPatientDischargePicker.SelectedDate == null || dateOfPatientDischargePicker.SelectedDate > DateTime.Today)
            {
                MessageBox.Show("Date of discharge cannot be in the future.");
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
