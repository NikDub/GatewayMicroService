using AggregatorMicroService.Dto;
using AggregatorMicroService.Dto.Aggregated;

namespace AggregatorMicroService.Service.Abstraction;

public interface IAggregatorService
{
    Task CreatedDoctorWithPhotoAndAccountAsync(DoctorForCreatedAggregatedDto model, string attributeFromHeader);
    Task CreatedPatientProfileWithPhotoByPatient(PatientForCreatedAggregatedDto model, string attributeFromHeader);
    Task<List<DoctorWithOfficeDto>> GetDoctorWithOfficeAsync(string attributeFromHeader);
    Task<List<AppointmentWithPatientPhoneDto>> GetAppointmentScheduleAsync(string attributeFromHeader);
}