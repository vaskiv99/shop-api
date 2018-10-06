using System;
using Newtonsoft.Json;
using ShopService.Common.Enums;

namespace ShopService.Common.Utils
{
    public class ErrorTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var errorType = (ErrorType)value;
            writer.WriteValue((long)errorType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return (ErrorType)((long)reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(long);
        }
    }
}