using AggregatorMicroService.Dto;
using AggregatorMicroService.Entities.Models;

namespace AggregatorMicroService.Static
{
    public static class TimeSlots
    {
        private static int NumberOfDaysToRecord = 8;
        private static int TimeIntervalInMinutes = 10;
        private static TimeSpan StartWorkTime = new TimeSpan(8, 0, 0);
        private static TimeSpan EndWorkTime = new TimeSpan(17, 0, 0);
        private static TimeSpan Interval = TimeSpan.FromMinutes(TimeIntervalInMinutes);
        private static int SlotCount = (int)((EndWorkTime - StartWorkTime).TotalMinutes / Interval.TotalMinutes);

        private static List<DateWithTimeSlots> dateWithTimeSlots = new List<DateWithTimeSlots>();
        private static List<DateWithTimeSlots> getDatesWithTimes()
        {
            var nowDate = DateTime.UtcNow;

            if (dateWithTimeSlots.Count == 0 || dateWithTimeSlots.First().Date.Date != nowDate.Date)            
            {
                for (int i = 0; i < NumberOfDaysToRecord; i++)
                {
                    var dateWithTime = new DateWithTimeSlots { Date = nowDate.AddDays(i).Date };
                    for (int j = 0; j < SlotCount; j++)
                    {
                        TimeSpan time = StartWorkTime.Add(TimeSpan.FromMinutes(j * Interval.TotalMinutes));
                        dateWithTime.timeWithStatuses.Add(new TimeWithStatus { Time = time });
                    }

                    dateWithTimeSlots.Add(dateWithTime);
                }
            }
            return dateWithTimeSlots;
        }

        public static List<DateWithTimeSlots> GetTimeSlotsWithReservedTime(List<AppointmentDto> appointments)
        {
            var dates = getDatesWithTimes();

            foreach (AppointmentDto appointment in appointments)
            {
                var date = dates.First(d => d.Date == appointment.Date);
                var dateIndex = dates.IndexOf(date);

                var time = date.timeWithStatuses.First(t => t.Time == appointment.Time);
                var timeIndex = date.timeWithStatuses.IndexOf(time);

                for (int i = 0; i < ((int)appointment.Duration) / TimeIntervalInMinutes; i++)
                {
                    dates[dateIndex].timeWithStatuses[timeIndex + i].IsFree = false;
                }
            }

            dates.ForEach(d => d.isFull = d.timeWithStatuses.All(t => t.IsFree == false));

            return dates;
        }
    }
}
