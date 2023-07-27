namespace MedApp.Domain.Services;

using MedApp.Domain.Models;
using MedApp.Domain.Repositories;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
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