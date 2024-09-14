using Lab4_0.Data;
using Lab4_0.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lab4_0.Repository
{
    public class HospitalRepository
    {
        private readonly DataContext _context;

        public HospitalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patient)
        {
            ValidateInput(patient);

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
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

            if (inputPatient.DateOfPatientDischarge == null || inputPatient.DateOfPatientDischarge > DateTime.Today)
            {
                throw new Exception("Date of discharge cannot be in the future.");
            }


            if (!IsValidDiagnosis(inputPatient.Diagnosis))
            {
                string validItemsString = string.Join(", ", ValidDiagnosis);
                throw new Exception($"Please select a valid diagnosis item. Valid items are: {validItemsString}");
            }

        }

        private static readonly HashSet<string> ValidDiagnosis= new HashSet<string>
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
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
            {
                throw new Exception($"Patient with ID {id} not found.");
            }

            return patient;
        }


        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await GetPatientById(id);

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatient(Patient patientToUpdate, int id)
        {
            await CheckIfPatientExists(id);

            ValidateInput(patientToUpdate);

            _context.Patients.Attach(patientToUpdate);

            patientToUpdate.Id = id;

            _context.Entry(patientToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


        public async Task<bool> CheckIfPatientExists(int id)
        {
            if (!await _context.Patients.AnyAsync(p => p.Id == id))
            {
                throw new Exception($"Patient with ID {id} not found.");
            }

            return true;
        }



    }
}
