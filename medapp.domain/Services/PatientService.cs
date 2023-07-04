using Microsoft.EntityFrameworkCore;

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

    public async Task AddPatientAsync(Patient patient)
    {
        patient.DateOfBirth = patient.DateOfBirth.ToUniversalTime();
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
}