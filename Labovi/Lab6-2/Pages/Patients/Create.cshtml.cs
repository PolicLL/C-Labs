using Lab6_2.Model;
using Lab6_2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab6_2.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly HospitalApiService _apiService;

        public CreateModel(HospitalApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _apiService.CreatePatient(patient);

            return RedirectToPage("./Index");
        }
    }
}
