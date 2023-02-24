namespace AggregatorMicroService.Entities.Models
{
    public class TimeWithStatus
    {
        public TimeSpan Time { get; set; }
        public bool IsFree { get; set; } = true;
    }
}
