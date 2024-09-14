using Lab6.Entities;
using Lab6.Repository;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Lab6.Extensions;
using System.Linq.Expressions;
using Lab6.Utils;
using System.Linq;
using System.Reflection;


namespace Lab6.Service
{
    public class HospitalService
    {
        private readonly HospitalRepository _repository;
        private readonly ValidationService _validationService;

        public HospitalService(HospitalRepository repository, ValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        public async Task AddPatient(Patient patient)
        {
            _validationService.ValidateInput(patient);

            patient.DateOfPatientDischarge = null;

            await _repository.AddPatient(patient);
        }

        

        public async Task<Patient> GetPatientById(int id)
        {
            return await _repository.GetPatientById(id);
        }

        public async Task<List<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }
        public async Task<List<Patient>> GetAllPatients(
            string? search = null,
            string? filter = null,
            string? sortBy = null,
            bool? isAscending = null)
        {
            IQueryable<Patient> query = _repository.GetAllPatientsQueryable();

            var searchedQuery = ApplySearch(query, search);
            var filteredQuery = ApplyFilter(searchedQuery, filter);
            var sortedQuery = ApplySorting(filteredQuery, sortBy, isAscending);

            return sortedQuery.ToList(); // Convert IQueryable to List<Patient>
        }

        public IQueryable<Patient> FilterPatientsByDate(string propertyName, string propertySearchValue, IQueryable<Patient> query)
        {
            DateTime filterDate = DateUtils.ParseDate(propertySearchValue);
            var propertyInfo = typeof(Patient).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo != null && propertyInfo.PropertyType == typeof(DateTime))
            {
                var filteredPatients = query.ToList()
                    .Where(p => DateUtils.areDatesEqual((DateTime)propertyInfo.GetValue(p), filterDate))
                    .AsQueryable();

                return filteredPatients;
            }
            else
            {
                return query.Where(p => false);
            }
        }



        private IQueryable<Patient> ApplySearch(IQueryable<Patient> query, string? search)
        {
            if (string.IsNullOrEmpty(search))
                return query;

            var propertyValue = search.Split('=');
            if (propertyValue.Length != 2)
                return query;

            var propertyName = propertyValue[0].ToLower();
            var propertySearchValue = propertyValue[1];

            if (propertyName.Equals("dateofpatientadmission") || propertyName.Equals("dateofpatientdischarge"))
            {
                return FilterPatientsByDate(propertyName, propertySearchValue, query);
            }

            switch (propertyName)
            {
                case "name":
                    return query.Where(p => p.Name.Contains(propertySearchValue));
                case "surname":
                    return query.Where(p => p.Surname.Contains(propertySearchValue));
                case "oib":
                    return query.Where(p => p.OIB.Contains(propertySearchValue));
                case "mbo":
                    return query.Where(p => p.MBO.Contains(propertySearchValue));
                default:
                    return query; // Property not supported for search
            }
        }

        private IQueryable<Patient> ApplyFilter(IQueryable<Patient> query, string? filter)
        {
            if (string.IsNullOrEmpty(filter))
                return query;

            var propertyValue = filter.Split('=');
            if (propertyValue.Length != 2)
                return query;

            var propertyName = propertyValue[0].ToLower();
            var propertyFilterValue = propertyValue[1];

            if (propertyName.Equals("dateofpatientadmission") || propertyName.Equals("dateofpatientdischarge"))
            {
                return FilterPatientsByDate(propertyName, propertyFilterValue, query);
            }

            switch (propertyName)
            {
                case "name":
                    return query.Where(p => p.Name == propertyFilterValue);
                case "surname":
                    return query.Where(p => p.Surname == propertyFilterValue);
                case "oib":
                    return query.Where(p => p.OIB == propertyFilterValue);
                case "mbo":
                    return query.Where(p => p.MBO == propertyFilterValue);
                case "diagnosis":
                    return query.Where(p => p.Diagnosis == propertyFilterValue);
                default:
                    return query; // Property not supported for filtering
            }
        }

        private IQueryable<Patient> ApplySorting(IQueryable<Patient> query, string? sortBy, bool? isAscending)
        {
            if (string.IsNullOrEmpty(sortBy))
                return query;

            return isAscending.HasValue && isAscending.Value ?
                query.OrderByProperty(sortBy) :
                query.OrderByPropertyDescending(sortBy);
        }


        public async Task DeletePatientAsync(int id)
        {
            await _repository.DeletePatientAsync(id);
        }

        public async Task UpdatePatient(Patient patientToUpdate, int id)
        {
            _validationService.ValidateInput(patientToUpdate);

            await _repository.UpdatePatient(patientToUpdate, id);
        }

        public async Task<bool> CheckIfPatientExists(int id)
        {
            return await _repository.CheckIfPatientExists(id);
        }

        public async Task DischargePatient(int id, DateTime dischargeDate)
        {
            if (dischargeDate.Date > DateTime.Today)
            {
                throw new ArgumentException("Discharge date must not be in the future.");
            }

            var patient = await GetPatientById(id);

            if (patient.DateOfPatientDischarge.HasValue)
            {
                throw new InvalidOperationException("Patient is already discharged.");
            }

            if (dischargeDate.Date < patient.DateOfPatientAdmission.Date)
            {
                throw new ArgumentException("Discharge date cannot be before the admission date.");
            }

            patient.DateOfPatientDischarge = dischargeDate;

            await UpdatePatient(patient, id);
        }

        public async Task<List<Patient>> GetPatientsByStatus(bool isActive)
        {
            return await _repository.GetPatientsByStatus(isActive);
        }


    }
}
