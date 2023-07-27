namespace MedApp.Domain.Repositories;

using MedApp.Domain.Models;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
}