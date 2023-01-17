using AggregatorMicroService.Dto;
using AggregatorMicroService.Dto.Aggregated;

namespace AggregatorMicroService.Service.Abstraction;

public interface IAggregatorService
{
    Task CreatedDoctorWithPhotoAndAccountAsync(DoctorForCreatedAggregatedDto model, string attributeFromHeader);
    Task CreatedPatientProfileWithPhotoByPatient(PatientForCreatedAggregatedDto model, string attributeFromHeader);
    Task<IEnumerable<DoctorWithOfficeDto>> GetDoctorWithOfficeAsync(string attributeFromHeader);
    Task<IEnumerable<AppointmentWithPatientPhoneDto>> GetAppointmentScheduleAsync(string attributeFromHeader);
}