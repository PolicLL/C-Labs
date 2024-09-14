using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using Lab6_2.Service;
using Lab6_2.Model;

namespace Lab6_2.Pages.Patients
{
    public class DischargeModel : PageModel
    {
        private readonly HospitalApiService _apiService;

        public DischargeModel(HospitalApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public DateTime DischargeDate { get; set; }

        [BindProperty]
        public int PatientId { get; set; }

        [BindProperty]
        public string PatientName { get; set; }

        [BindProperty]
        public string PatientSurname { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PatientId = id;
            Patient PatientForDischarge = await _apiService.GetPatientById(PatientId);
            PatientName = PatientForDischarge.Name;
            PatientSurname = PatientForDischarge.Surname;
            DischargeDate = DateTime.Today; // Set the default discharge date to today

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Console.WriteLine(PatientName);
            Console.WriteLine(PatientSurname);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.DischargePatient(id, DischargeDate);
            return RedirectToPage("./Index");
        }
    }
}
