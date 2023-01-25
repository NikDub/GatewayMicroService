using AggregatorMicroService.Dto.Photos;

namespace AggregatorMicroService.Dto.Aggregated;

public class DoctorForCreatedAggregatedDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int CareerStartYear { get; set; }
    public Guid SpecializationId { get; set; }
    public Guid StatusId { get; set; }
    public Guid OfficeId { get; set; }
    public string Email { get; set; }
    public PhotoCreatedDto Photo { get; set; }
}