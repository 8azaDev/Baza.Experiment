using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace Baza.Experiment.Models
{
    public class DateTimeRequest
    {
        public string Name { get; set; }

        //[JsonConverter(typeof(MyDateTimeConvert))]
        public DateTime Date { get; set; }
    }

    public class MyDateTimeConvert : DateTimeConverterBase
    {
        public MyDateTimeConvert()
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string dateText = reader.Value.ToString();
            return DateTime.Parse(dateText, CultureInfo.CurrentCulture, DateTimeStyles.RoundtripKind);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public class EsItem
    {
        public string Name { get; set; }

        [Date(Format = "yyyy-MM-dd HH:mm:ss.SSS")]
        public DateTime Date { get; set; }

        [Date]
        public long Ticks { get; set; }
    }
}
