using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MedApp.Repositories.Interfaces;
using MedApp.Infrastructure.Database;
using Entities = MedApp.Infrastructure.Database.Entities;
using MedApp.Domain.Data.Models;

namespace MedApp.Repositories.Implementations;

public class PatientRepositryEFPostgresImpl : IPatientRepository
{
    public PatientRepositryEFPostgresImpl(MedDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        var patientEntities = await _dbContext.Patients
            .Include(p => p.Addresses)
            .ToListAsync();

        var patients = _mapper.Map<IEnumerable<Patient>>(patientEntities);

        return patients;
    }

    public async Task AddPatientAsync(Patient patient)
    {
        var patientToAdd = patient with { DateOfBirth = patient.DateOfBirth.ToUniversalTime() };
        var patientEntity = _mapper.Map<Entities.Patient>(patient);
        await _dbContext.Patients.AddAsync(patientEntity);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}