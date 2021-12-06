using System;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;

namespace Kafka
{
    internal sealed class KafkaSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            if (typeof(T) == typeof(Null))
                return null;

            if (typeof(T) == typeof(Ignore))
                throw new NotSupportedException("Not Supported.");

            var json = JsonConvert.SerializeObject(data);

            return Encoding.UTF8.GetBytes(json);
        }
    }

    internal sealed class KafkaSerializerAvro<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
           // var str = System.Text.Json.JsonSerializer.Serialize(data); //you can also use Newtonsoft here
            //var bytes = Encoding.UTF8.GetBytes(str);
            return SolTechnology.Avro.AvroConvert.Serialize(data);
        }
    }
}