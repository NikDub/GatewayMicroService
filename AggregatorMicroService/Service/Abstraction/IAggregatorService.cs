using AggregatorMicroService.Dto.Aggregated;
using AggregatorMicroService.Entities.Models;

namespace AggregatorMicroService.Service.Abstraction;

public interface IAggregatorService
{
    Task CreatedDoctorWithPhotoAndAccountAsync(DoctorForCreatedAggregatedDto model, string attributeFromHeader);
    Task CreatedPatientProfileWithPhotoByPatient(PatientForCreatedAggregatedDto model, string attributeFromHeader);
    Task<IEnumerable<DoctorWithOfficeDto>> GetDoctorsWithOfficeAsync(string attributeFromHeader);
    Task<IEnumerable<AppointmentWithPatientPhoneAndOfficeDto>> GetAppointmentScheduleAsync(string attributeFromHeader);
    Task<IEnumerable<DoctorWithPhotoAndOffice>> GetDoctorsWithPhotoAsync(string attributeFromHeader);
    Task<IEnumerable<DateWithTimeSlots>> GetTimeSlotsWithReserved(Guid doctorID);
}