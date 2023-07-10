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
        return await _dbContext.Patients
            .Include(p => p.Addresses)
            .ToListAsync();
    }

    public async Task AddPatientAsync(Patient patient)
    {
        var patientEntity = 
        patient.DateOfBirth = patient.DateOfBirth.ToUniversalTime();
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();
    }

    private readonly MedDbContext _dbContext;
    private readonly IMapper _mapper;
}