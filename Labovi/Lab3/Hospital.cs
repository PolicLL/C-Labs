using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Patient
    {
        public string OIB { get; set; }
        public string MBO { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfPatientAdmission{ get; set; }
        public DateTime? DateOfPatientDischarge { get; set; }
        public string Gender { get; set; }
        public string Diagnosis { get; set; }

        public int ID { get; set; }

        private static int tempID = 0;

        public Patient()
        {
            ID = tempID++;
        }

        public Patient(string oib, string mbo, string name, string surname, DateTime dateOfBirth, DateTime dateOfPatientAdmission, DateTime? dateOfPatientDischarge, string gender, string diagnosis) : this()
        {
            OIB = oib;
            MBO = mbo;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            DateOfPatientAdmission = dateOfPatientAdmission;
            DateOfPatientDischarge = dateOfPatientDischarge;
            Gender = gender;
            Diagnosis = diagnosis;
        }
    }


    public class Hospital
    {
        private List<Patient> patients = new List<Patient>();


        public void UpdatePatient()
        {
            // Retrieve patient

            Patient patientToUpdate = GetPatientById();

            if (patientToUpdate == null)
            {
                Console.WriteLine("There is no patient with this ID.");
                return;
            }

            // Inserting values

            UpdateProperties(patientToUpdate);

        }

        public Patient GetPatientById()
        {
            Console.WriteLine("Enter ID of patient you want to update: ");

            int patientID = int.Parse(Console.ReadLine());

            return patients.FirstOrDefault(patient => patient.ID == patientID);
        }

        public Patient GetPatientById(int patientID)
        {
            return patients.FirstOrDefault(patient => patient.ID == patientID);
        }

        private void UpdateProperties(Patient patientToUpdate)
        {
            bool updateMore = true;

            do
            {
                UpdateProperty(patientToUpdate);

                Console.WriteLine("Do you want to update more properties ? (YES/NO)");

                if (Console.ReadLine() != "YES")
                    updateMore = false;
            }
            while (updateMore);
        }

        private void UpdateProperty(Patient patientToUpdate)
        {
            Console.WriteLine("Enter which value you want to update (OIB, MBO, Name, Surname, DateOfBirth, Gender, Diagnosis)");

            string propertyToUpdate = Console.ReadLine();

            switch (propertyToUpdate)
            {
                case "OIB":
                    string newOIB = InputOIB();
                    patientToUpdate.OIB = newOIB;
                    break;
                case "MBO":
                    string newMBO = InputMBO();
                    patientToUpdate.MBO = newMBO;
                    break;
                case "Name":
                    string newName = EnterNewString("first name");
                    patientToUpdate.Name = newName;
                    break;
                case "Surname":
                    string newSurname = EnterNewString("second name");
                    patientToUpdate.Surname = newSurname;
                    break;
                case "DateOfBirth":
                    DateTime newDateOfBirth = InputDateOfBirth();
                    patientToUpdate.DateOfBirth = newDateOfBirth;
                    break;
                case "Gender":
                    string newGender = InputGender();
                    patientToUpdate.Gender = newGender;
                    break;
                case "Diagnosis":
                    string newDiagnosis = EnterNewString("diagnosis");
                    patientToUpdate.Diagnosis = newDiagnosis;
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        private string InputOIB()
        {
            string OIB = EnterNewString("OIB");

            while (OIB.Length != 11)
            {
                Console.WriteLine("OIB must be 11 characters long.");
                Console.WriteLine("Enter OIB again : ");
                OIB = Console.ReadLine();
            }

            return OIB;
        }


        private string InputMBO()
        {
            string MBO = EnterNewString("MBO");

            while (MBO.Length != 9)
            {
                Console.WriteLine("MBO must be 9 characters long.");
                Console.WriteLine("Enter MBO again : ");
                MBO = Console.ReadLine();
            }

            return MBO;
        }

        private DateTime InputDateOfBirth()
        {
            Console.WriteLine("Enter date of birth (format: dd/MM/yyyy) : ");
            string dobInput = Console.ReadLine();

            DateTime dateOfBirth;
            while (!DateTime.TryParseExact(dobInput, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dateOfBirth))
            {
                Console.WriteLine("Invalid date format. Please enter date in dd/MM/yyyy format.");
                Console.Write("Enter date of birth (format: dd/MM/yyyy) : ");
                dobInput = Console.ReadLine();
            }
            return dateOfBirth;
        }


        private string InputGender()
        {
            Console.Write("Enter gender (M/F) : ");
            string genderInput = Console.ReadLine().ToUpper();

            while (genderInput != "M" && genderInput != "F")
            {
                Console.Write("Invalid gender input. Please enter M for Male or F for Female.");
                genderInput = Console.ReadLine().ToUpper();
            }

            return genderInput == "M" ? "Male" : "Female";
        }

        private string EnterNewString(string fieldName)
        {
            Console.Write($"Enter new {fieldName}: ");
            return Console.ReadLine();
        }

        // Discharge

        public void DischargePatient()
        {
            Console.WriteLine("Enter ID of patient you want to discharge: ");

            int patientID = int.Parse(Console.ReadLine());

            Patient patientToRemove = patients.FirstOrDefault(patient => patient.ID == patientID);

            if (patientToRemove != null)
            {
                patients.Remove(patientToRemove);
                Console.WriteLine("Patient removed.");
            }
            else
            {
                Console.WriteLine("Patient with this ID does not exist.");
            }
        }

        public void AddPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        // Printing
        public string GetAllPatientInformation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetHeader());

            foreach (var patient in patients)
            {
                sb.AppendLine(GetPatientInfo(patient));
            }

            sb.Append(GetFooter());

            return sb.ToString();
        }

        public string GetAllActivePatientsInformation()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetHeader());

            var activePatientsList = patients.Where(patient => patient.DateOfPatientDischarge == null).ToList();

            foreach (var patient in activePatientsList)
            {
                sb.AppendLine(GetPatientInfo(patient));
            }

            sb.Append(GetFooter());

            return sb.ToString();
        }


        private string GetPatientInfo(Patient patient)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"| {patient.ID,-5} | {patient.OIB,-20} | {patient.MBO,-20} | {patient.Name,-20} | {patient.Surname,-20} " +
                $"| {patient.DateOfBirth.ToString("dd/MM/yyyy"),-20} | {patient.DateOfPatientAdmission.ToString("dd/MM/yyyy"),-20} | {patient.DateOfPatientDischarge?.ToString("dd/MM/yyyy"),-20} | {patient.Gender,-20} | {patient.Diagnosis,-20} |");
            return sb.ToString();
        }

        private string GetHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| {"ID",-5} | {"OIB",-20} | {"MBO",-20} | {"Name",-20} | {"Surname",-20} | {"Date of Birth",-20} | {"Date of Admission",-20} | {"Date of Discharge",-20} | {"Gender",-20} | {"Diagnosis",-20} |");
            sb.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            return sb.ToString();
        }

        private string GetFooter()
        {
            return "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
        }

    }
}
