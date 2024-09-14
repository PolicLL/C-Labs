using Lab5.Data;
using Lab5.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Lab5.Repository
{
    public class HospitalRepository
    {
        private readonly DataContext _context;

        public HospitalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task<Patient> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient is null)
            {
                throw new Exception($"Patient with ID {id} not found.");
            }

            return patient;
        }


        public async Task<List<Patient>> GetAllPatients()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await GetPatientById(id);

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatient(Patient patientToUpdate, int id)
        {
            await CheckIfPatientExists(id);

            _context.Patients.Attach(patientToUpdate);

            patientToUpdate.Id = id;

            _context.Entry(patientToUpdate).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }


        public async Task<bool> CheckIfPatientExists(int id)
        {
            if (!await _context.Patients.AnyAsync(p => p.Id == id))
            {
                throw new Exception($"Patient with ID {id} not found.");
            }

            return true;
        }

        public async Task<List<Patient>> GetPatientsByStatus(bool isActive)
        {
            if (isActive)
            {
                return await _context.Patients.Where(p => p.DateOfPatientDischarge == null).ToListAsync();
            }
            else
            {
                return await _context.Patients.Where(p => p.DateOfPatientDischarge != null).ToListAsync();
            }
        }



    }
}
