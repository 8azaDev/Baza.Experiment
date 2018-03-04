using System;

namespace Baza.Experiment.Models
{
    public class UserCultureInfo
    {
        public TimeZoneInfo TimeZone { get; set; }

        public UserCultureInfo()
        {
            TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/London");
        }

        public DateTime GetUtcTime(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, TimeZone).ToUniversalTime();
        }
    }
}
