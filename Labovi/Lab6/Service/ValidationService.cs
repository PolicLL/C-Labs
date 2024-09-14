using Lab6.Entities;
using System.Text.RegularExpressions;

namespace Lab6.Service
{
    public class ValidationService
    {

        public void ValidateInput(Patient inputPatient)
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
    }
}
