﻿using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Baza.Experiment.Models
{
    public class DateTimeRequest
    {
        public string Name { get; set; }

        //[JsonConverter(typeof(MyDateTimeConvert))]
        public DateTime Date { get; set; }
    }

    public class MyDateTimeConvert: DateTimeConverterBase
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
}
