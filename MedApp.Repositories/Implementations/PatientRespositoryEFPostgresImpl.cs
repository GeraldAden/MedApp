using Microsoft.EntityFrameworkCore;
public class PatientRepositryEFPostgresImpl : IPatientRepository
{
    public PatientRepositryEFPostgresImpl(MedDbContext dbContext)
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