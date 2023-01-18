namespace AggregatorMicroService.Dto.Profiles;

public class DoctorDto
{
    public string Id { get; set; }
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int CareerStartYear { get; set; }
    public string AccountPhoneNumber { get; set; }
    public SpecializationDto Specialization { get; set; }
    public string OfficeId { get; set; }
    public StatusDto Status { get; set; }
}