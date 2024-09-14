using Lab6.Data;
using Lab6.Entities;
using Lab6.Repository;
using Lab6.Service;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {

        private readonly HospitalService _hospitalService;

        public HospitalController(HospitalService hospitalService)
        {
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPatients(
            [FromQuery] string? search = null,
            [FromQuery] string? filter = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] bool? isAscending = null)
        {
            try
            {
                var patients = await _hospitalService.GetAllPatients(search, filter, sortBy, isAscending);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            try
            {
                var patient = await _hospitalService.GetPatientById(id);

                return Ok(patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            try
            {
                await _hospitalService.AddPatient(patient);

                return CreatedAtAction(nameof(GetPatientById), new { id = patient.Id }, patient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            try
            {
                patient.Id = id;

                await _hospitalService.UpdatePatient(patient, id);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                await _hospitalService.DeletePatientAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}/discharge")]
        public async Task<IActionResult> DischargePatient(int id, [FromBody] DateTime dischargeDate)
        {
            try
            {
                await _hospitalService.DischargePatient(id, dischargeDate);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("status/{isActive}")]
        public async Task<IActionResult> GetPatientsByStatus(bool isActive)
        {
            try
            {
                var patients = await _hospitalService.GetPatientsByStatus(isActive);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
