using AggregatorMicroService.Dto.Photos;

namespace AggregatorMicroService.Dto.Aggregated;

public class PatientForCreatedAggregatedDto
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string AccountPhoneNumber { get; set; }
    public PhotoCreatedDto Photo { get; set; }
}