namespace MedApp.Domain.Services;

using MedApp.Domain.Data.Models;
using MedApp.Repositories.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
//     Task<Patient> GetPatientAsync(string id);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}

public class PatientService : IPatientService
{

    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        return await _patientRepository.GetPatientsAsync();
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _patientRepository.AddPatientAsync(patient);
    }

    private readonly IPatientRepository _patientRepository;
}