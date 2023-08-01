namespace MedApp.Infrastructure;

using AutoMapper;
using MedApp.Application.Abstractions.Models;
using MedApp.Domain.Entities;
using Entities = MedApp.Infrastructure.Persistence.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, Entities.PatientEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Entities.PatientEntity, Patient>();

        CreateMap<Address, Entities.AddressEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PatientId, opt => opt.Ignore())
            .ForMember(dest => dest.Patient, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Entities.AddressEntity, Address>();

        CreateMap<User, Entities.UserEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Entities.UserEntity, User>();
    }
}