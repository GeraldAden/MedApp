namespace MedApp.Domain.Abstractions.Repositories;

using MedApp.Domain.Entities;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
}