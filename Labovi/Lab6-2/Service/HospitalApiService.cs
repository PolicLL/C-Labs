using Lab6_2.Model;

namespace Lab6_2.Service
{
    public class HospitalApiService
    {

        private readonly HttpClient _httpClient;

        public HospitalApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5239");
        }

        public async Task<List<Patient>> GetAllPatients(string? search = null, string? filter = null, string? sortBy = null, bool? isAscending = null)
        {
            var query = $"?search={search}&filter={filter}&sortBy={sortBy}&isAscending={isAscending}";
            return await _httpClient.GetFromJsonAsync<List<Patient>>($"api/Hospital{query}");
        }

        public async Task<Patient> GetPatientById(int id)
        {
            return await _httpClient.GetFromJsonAsync<Patient>($"api/Hospital/{id}");
        }

        public async Task CreatePatient(Patient patient)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Hospital", patient);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdatePatient(int id, Patient patient)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Hospital/{id}", patient);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletePatient(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Hospital/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task DischargePatient(int id, DateTime dischargeDate)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Hospital/{id}/discharge", dischargeDate);
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<Patient>> GetPatientsByStatus(bool isActive)
        {
            return await _httpClient.GetFromJsonAsync<List<Patient>>($"api/Hospital/status/{isActive}");
        }

    }
}
