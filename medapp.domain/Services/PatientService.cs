using Microsoft.EntityFrameworkCore;

public interface IPatientService
{
//     Task<Patient> GetPatientAsync(string id);
    Task<IEnumerable<Patient>> GetPatientsAsync();
//     Task AddPatientAsync(Patient patient);
//     Task<bool> UpdatePatientAsync(Patient patient);
//     Task<bool> DeletePatientAsync(string id);
}

public class PatientService : IPatientService
{

    public PatientService(MedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        return await _dbContext.Patients
            .Include(p => p.Addresses)
            .ToListAsync();
    }

    private readonly MedDbContext _dbContext;
}