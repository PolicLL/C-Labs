using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_1.Entities
{
    public class Patient
    {
        public string OIB { get; set; } = "";
        public string MBO { get; set; } = "";
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfPatientAdmission { get; set; }
        public DateTime? DateOfPatientDischarge { get; set; }
        public string Gender { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public int Id { get; set; }
    }
}
