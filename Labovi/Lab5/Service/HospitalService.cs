using Lab5.Entities;
using Lab5.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lab5.Service
{
    public class HospitalService
    {
        private readonly HospitalRepository _repository;

        public HospitalService(HospitalRepository repository)
        {
            _repository = repository;
        }

        public async Task AddPatient(Patient patient)
        {
            ValidateInput(patient);

            patient.DateOfPatientDischarge = null;

            await _repository.AddPatient(patient);
        }

        private void ValidateInput(Patient inputPatient)
        {
            if (!Regex.IsMatch(inputPatient.OIB, "^[0-9]*$") || inputPatient.OIB.Length != 11)
            {
                throw new Exception("OIB must be numeric and 11 characters long.");
            }

            if (!Regex.IsMatch(inputPatient.MBO, "^[0-9]*$") || inputPatient.MBO.Length != 9)
            {
                throw new Exception("MBO must be numeric and 9 characters long.");
            }

            if (string.IsNullOrWhiteSpace(inputPatient.Name))
            {
                throw new Exception("Name cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(inputPatient.Surname))
            {
                throw new Exception("Surname cannot be empty.");
            }

            if (!(inputPatient.Gender.Equals("Male") || inputPatient.Gender.Equals("Female")))
            {
                throw new Exception("Please select a gender.");
            }

            if (inputPatient.DateOfBirth > DateTime.Today)
            {
                throw new Exception("Date of Birth cannot be in the future.");
            }

            if (inputPatient.DateOfPatientAdmission > DateTime.Today)
            {
                throw new Exception("Date of Admission cannot be in the future.");
            }

            if (!IsValidDiagnosis(inputPatient.Diagnosis))
            {
                string validItemsString = string.Join(", ", ValidDiagnosis);
                throw new Exception($"Please select a valid diagnosis item. Valid items are: {validItemsString}");
            }

        }

        private static readonly HashSet<string> ValidDiagnosis = new HashSet<string>
        {
            "A01", "A02", "A03",
            "B01", "B02", "B03",
            "C01", "C02", "C03",
            "D01", "D02", "D03"
        };

        public static bool IsValidDiagnosis(string input)
        {
            return ValidDiagnosis.Contains(input);
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _repository.GetPatientById(id);
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }

        public async Task DeletePatientAsync(int id)
        {
            await _repository.DeletePatientAsync(id);
        }

        public async Task UpdatePatient(Patient patientToUpdate, int id)
        {
            ValidateInput(patientToUpdate);

            await _repository.UpdatePatient(patientToUpdate, id);
        }

        public async Task<bool> CheckIfPatientExists(int id)
        {
            return await _repository.CheckIfPatientExists(id);
        }

        public async Task DischargePatient(int id, DateTime dischargeDate)
        {
            if (dischargeDate.Date > DateTime.Today)
            {
                throw new ArgumentException("Discharge date must not be in the future.");
            }

            var patient = await GetPatientById(id);

            if (patient.DateOfPatientDischarge.HasValue)
            {
                throw new InvalidOperationException("Patient is already discharged.");
            }

            if (dischargeDate.Date < patient.DateOfPatientAdmission.Date)
            {
                throw new ArgumentException("Discharge date cannot be before the admission date.");
            }

            patient.DateOfPatientDischarge = dischargeDate;

            await UpdatePatient(patient, id);
        }

        public async Task<List<Patient>> GetPatientsByStatus(bool isActive)
        {
            return await _repository.GetPatientsByStatus(isActive);
        }


    }
}
