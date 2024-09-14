using System;
using System.Windows;
using System.Windows.Controls;

namespace Lab3
{
    public partial class PatientDataWindow : Window
    {

        private Hospital hospital;
        private int patientID;

        public PatientDataWindow(Hospital _hospital, Patient patient)
        {
            InitializeComponent();

            populateControlsWithPatientData(patient);

            this.hospital = _hospital;
            this.patientID = patient.ID;
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
            }
        }

        private void UpdatePatientSubmit_Click(object sender, RoutedEventArgs e)
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

            var tempPatient = hospital.GetPatientById(patientID);

            updatedPatient.ID = tempPatient.ID;
            hospital.GetAllPatients().Remove(tempPatient);
            hospital.AddPatient(updatedPatient);

            MessageBox.Show("Patient added successfully!");

            this.Close();
        }

    }
}
