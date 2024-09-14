using Lab6_2.Model;
using Lab6_2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab6_2.Pages.Patients
{
    public class DeleteModel : PageModel
    {
        private readonly HospitalApiService _apiService;

        public DeleteModel(HospitalApiService apiService)
        {
            _apiService = apiService;
        }

        [BindProperty]
        public Patient Patient { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Patient = await _apiService.GetPatientById(id);

            if (Patient == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _apiService.DeletePatient(id);

            return RedirectToPage("./Index");
        }
    }
}
