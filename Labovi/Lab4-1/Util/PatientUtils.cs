using Lab4_1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_1.Util
{
    internal class PatientUtils
    {
        public static string GetAllPatientInformation(List<Patient> patients)
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

        public static string GetAllActivePatientsInformation(List<Patient> patients)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetHeader());

            var activePatientsList = patients.Where(patient => patient.DateOfPatientDischarge == DateTime.MinValue).ToList();

            foreach (var patient in activePatientsList)
            {
                sb.AppendLine(GetPatientInfo(patient));
            }

            sb.Append(GetFooter());

            return sb.ToString();
        }


        private static string GetPatientInfo(Patient patient)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"| {patient.Id,-5} | {patient.OIB,-20} | {patient.MBO,-20} | {patient.Name,-20} | {patient.Surname,-20} " +
                $"| {patient.DateOfBirth.ToString("dd/MM/yyyy"),-20} | {patient.DateOfPatientAdmission.ToString("dd/MM/yyyy"),-20} | {patient.DateOfPatientDischarge?.ToString("dd/MM/yyyy"),-20} | {patient.Gender,-20} | {patient.Diagnosis,-20} |");
            return sb.ToString();
        }

        private static string GetHeader()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            sb.AppendLine($"| {"ID",-5} | {"OIB",-20} | {"MBO",-20} | {"Name",-20} | {"Surname",-20} | {"Date of Birth",-20} | {"Date of Admission",-20} | {"Date of Discharge",-20} | {"Gender",-20} | {"Diagnosis",-20} |");
            sb.AppendLine("---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
            return sb.ToString();
        }

        private static string GetFooter()
        {
            return "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------";
        }
    }
}
