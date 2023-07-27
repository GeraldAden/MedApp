namespace MedApp.Domain;

using AutoMapper;
using MedApp.Domain.Models;
using Entities = MedApp.Infrastructure.Database.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, Entities.Patient>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Entities.Patient, Patient>();

        CreateMap<Address, Entities.Address>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Entities.Address, Address>();
    }
}