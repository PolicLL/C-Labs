using Lab6_2.Model;
using Lab6_2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab6_2.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly HospitalApiService _apiService;

        public IndexModel(HospitalApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Patient> Patients { get; set; }

        public async Task OnGetAsync()
        {
            Patients = await _apiService.GetAllPatients();
        }
    }
}
