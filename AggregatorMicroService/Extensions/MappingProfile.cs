using AggregatorMicroService.Dto;
using AggregatorMicroService.Dto.Aggregated;
using AggregatorMicroService.Dto.Profiles;
using AutoMapper;

namespace AggregatorMicroService.Extensions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DoctorDto, DoctorWithOfficeDto>().ReverseMap();
        CreateMap<PatientForCreateDto, PatientForCreatedAggregatedDto>().ReverseMap();
        CreateMap<DoctorForCreateDto, DoctorForCreatedAggregatedDto>().ReverseMap();
        CreateMap<AppointmentDto, AppointmentWithPatientPhoneDto>().ReverseMap();
    }
}