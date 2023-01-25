namespace AggregatorMicroService.Dto.Profiles;

public class DoctorDto
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int CareerStartYear { get; set; }
    public string AccountPhoneNumber { get; set; }
    public SpecializationDto Specialization { get; set; }
    public Guid OfficeId { get; set; }
    public StatusDto Status { get; set; }
}