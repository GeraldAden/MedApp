namespace MedApp.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MedApp.Domain.Abstractions.Repositories;
using MedApp.Domain.Entities;
using MedApp.Infrastructure.Persistence.Entities;

public class PatientRepositoryEFPostgresImpl : IPatientRepository
{
    public PatientRepositoryEFPostgresImpl(MedDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        var patientEntities = await _dbContext.Patients
            .Include(p => p.Addresses)
            .ToListAsync();
   
        return _mapper.Map<IEnumerable<Patient>>(patientEntities);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        var patientEntity = _mapper.Map<PatientEntity>(patient);
        patientEntity.DateOfBirth = patient.DateOfBirth.ToUniversalTime();
        await _dbContext.Patients.AddAsync(patientEntity);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}