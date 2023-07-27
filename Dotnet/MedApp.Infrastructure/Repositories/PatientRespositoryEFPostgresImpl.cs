namespace MedApp.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using MedApp.Infrastructure.Database;
using MedApp.Infrastructure.Database.Entities;

public class PatientRepositoryEFPostgresImpl : IPatientRepository
{
    public PatientRepositoryEFPostgresImpl(MedDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        var patients = await _dbContext.Patients
            .Include(p => p.Addresses)
            .ToListAsync();

        return patients;
    }

    public async Task AddPatientAsync(Patient patient)
    {
        patient.DateOfBirth = patient.DateOfBirth.ToUniversalTime();
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
}