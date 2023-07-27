namespace MedApp.Domain.Services;

using AutoMapper;
using MedApp.Domain.Models;
using MedApp.Infrastructure.Repositories;
using Entities = MedApp.Infrastructure.Database.Entities;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetPatientsAsync();
    Task AddPatientAsync(Patient patient);
}

public class PatientService : IPatientService
{

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Patient>> GetPatientsAsync()
    {
        var patients = await _patientRepository.GetPatientsAsync();

        return _mapper.Map<IEnumerable<Patient>>(patients);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        var patientEntity = _mapper.Map<Entities.Patient>(patient);
        await _patientRepository.AddPatientAsync(patientEntity);
    }

    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
}