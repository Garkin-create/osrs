#nullable enable
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using RestSharp;
using RestSharp.Serialization;

namespace OSRS.Infrastructure.Helper
{
    public class RestSharperJsonSerializer : IRestSerializer
    {
        public string? Serialize(object obj)
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch
            {
                return null;
            }
        }

        public string ContentType { get; set; } = string.Empty;

        public T? Deserialize<T>(IRestResponse response)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(response.Content);
            }
            catch
            {
                return default;
            }
        }

        public string? Serialize(Parameter parameter)
        {
            return parameter.Value != null 
                ? JsonSerializer.Serialize(parameter.Value) 
                : null;
        }

        public string[] SupportedContentTypes => new[] {"text/json","application/json"};
        public DataFormat DataFormat => DataFormat.Json;
    }

    public class RestSharperXmlSerializer : IRestSerializer
    {
        public string? Serialize(object obj)
        {
            return SerializationUtils.SerializeXml(obj);
        }

        public string ContentType { get; set; } = string.Empty;
        public T? Deserialize<T>(IRestResponse response)
        {
            using var reader = new StringReader(response.Content);
            var serializer = new XmlSerializer(typeof(T));
            return (T?) serializer.Deserialize(reader);
        }

        public string? Serialize(Parameter parameter)
        {
            return parameter.Value != null 
                ? Serialize(parameter.Value) 
                : null;
        }

        public string[] SupportedContentTypes => new[] {"application/xml", "text/xml"};
        public DataFormat DataFormat => DataFormat.Xml;
    }
}