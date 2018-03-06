using System;
using System.Linq;

namespace Baza.Experiment.Models
{
    public class UserCultureInfo
    {
        public TimeZoneInfo TimeZone { get; set; }

        public UserCultureInfo()
        {
            TimeZone = TimeZoneInfo.GetSystemTimeZones().First();
        }

        public DateTime GetUtcTime(DateTime datetime)
        {
            return TimeZoneInfo.ConvertTime(datetime, TimeZone).ToUniversalTime();
        }
    }
}
