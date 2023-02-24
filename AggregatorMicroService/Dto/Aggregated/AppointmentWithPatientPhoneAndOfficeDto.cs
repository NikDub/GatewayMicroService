namespace AggregatorMicroService.Dto.Aggregated;

public class AppointmentWithPatientPhoneAndOfficeDto : AppointmentDto
{
    public string AccountPhoneNumber { get; set; }
    public OfficeDto Office { get; set; }
}