namespace AggregatorMicroService.Entities.Models
{
    public class DateWithTimeSlots
    {
        public DateTime Date { get; set; }
        public List<TimeWithStatus> timeWithStatuses { get; set; } = new List<TimeWithStatus>();
        public bool isFull { get; set; }
    }
}
