using AggregatorMicroService.Dto.Photos;

namespace AggregatorMicroService.Dto.Aggregated
{
    public class DoctorWithPhotoAndOffice
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
        public StatusDto Status { get; set; }
        public OfficeDto Office { get; set; }
        public string PhotoUrl { get; set; }
        public Guid PhotoId { get; set; }
    }
}
