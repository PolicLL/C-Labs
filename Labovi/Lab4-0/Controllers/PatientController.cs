using Lab4_0.Data;
using Lab4_0.Entities;
using Lab4_0.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_0.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly HospitalRepository _hospitalRepository;

        public PatientController(HospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _hospitalRepository.GetAllPatients();

                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var patient = await _hospitalRepository.GetPatientById(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            try
            {
                await _hospitalRepository.AddPatient(patient);

                return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            try
            {
                patient.Id = id;

                await _hospitalRepository.UpdatePatient(patient, id);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _hospitalRepository.DeletePatientAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
