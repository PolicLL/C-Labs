using Lab6_2.Model;
using Lab6_2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lab6_2.Pages.Patients
{
    public class StatusModel : PageModel
    {
        private readonly HospitalApiService _apiService;

        public StatusModel(HospitalApiService apiService)
        {
            _apiService = apiService;
        }

        public List<Patient> Patients { get; set; }
        public bool IsActive { get; private set; }

        public async Task OnGetAsync(bool isActive)
        {
            IsActive = isActive;
            Patients = await _apiService.GetPatientsByStatus(isActive);
        }
    }
}
