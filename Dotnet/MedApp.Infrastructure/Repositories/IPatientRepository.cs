namespace MedApp.Infrastructure.Repositories;

using MedApp.Infrastructure.Database.Entities;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
}