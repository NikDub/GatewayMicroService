using System.ComponentModel.DataAnnotations;

namespace AggregatorMicroService.Dto.Profiles;

public class PatientForCreateDto
{
    public string AccountId { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    public string MiddleName { get; set; }

    [Required] public DateTime DateOfBirth { get; set; }

    public string AccountPhoneNumber { get; set; }
}