namespace AggregatorMicroService.Dto.Profiles;

public class DoctorForCreateDto
{
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public StatusDto Status { get; set; }
    public int CareerStartYear { get; set; }
    public string AccountPhoneNumber { get; set; }
    public Guid SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    public Guid OfficeId { get; set; }
}