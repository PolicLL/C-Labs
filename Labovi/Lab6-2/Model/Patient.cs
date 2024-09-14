
using System.ComponentModel.DataAnnotations;

namespace Lab6_2.Model
{
    public class Patient
    {
        [Required(ErrorMessage = "OIB is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "OIB must be numeric.")]
        [StringLength(11, ErrorMessage = "OIB must be 11 characters long.", MinimumLength = 11)]
        public string OIB { get; set; }

        [Required(ErrorMessage = "MBO is required.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "MBO must be numeric.")]
        [StringLength(9, ErrorMessage = "MBO must be 9 characters long.", MinimumLength = 9)]
        public string MBO { get; set; }

        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [ValidateDateIsNotInFuture(ErrorMessage = "Date of Birth cannot be in the future.")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Patient Admission")]
        [ValidateDateIsNotInFuture(ErrorMessage = "Date of Admission cannot be in the future.")]
        public DateTime DateOfPatientAdmission { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Patient Discharge")]
        [ValidateDateIsNotInFuture(ErrorMessage = "Date of Discharge cannot be in the future.")]
        public DateTime? DateOfPatientDischarge { get; set; }
        public string Gender { get; set; } = "";
        public string Diagnosis { get; set; } = "";
        public int Id { get; set; }

        public static implicit operator List<object>(Patient v)
        {
            throw new NotImplementedException();
        }

        private class ValidateDateIsNotInFuture : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value == null) return true;
                DateTime dateOfAdmission = (DateTime)value;
                return dateOfAdmission <= DateTime.Now;
            }
        }
    }
}