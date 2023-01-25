namespace AggregatorMicroService.Dto.Profiles;

public class PatientDto
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string AccountPhoneNumber { get; set; }
}