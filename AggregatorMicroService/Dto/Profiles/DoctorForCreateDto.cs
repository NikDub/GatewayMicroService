namespace AggregatorMicroService.Dto.Profiles;

public class DoctorForCreateDto
{
    public string AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string StatusId { get; set; }
    public int CareerStartYear { get; set; }
    public string AccountPhoneNumber { get; set; }
    public string SpecializationId { get; set; }
    public string SpecializationName { get; set; }
    public string OfficeId { get; set; }
}