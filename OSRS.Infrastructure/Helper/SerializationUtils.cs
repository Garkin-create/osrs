using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Formatting = Newtonsoft.Json.Formatting;

namespace OSRS.Infrastructure.Helper
{
    public class SerializationUtils
    {
        public static string SerializeXml(object value)
        {
            var emptyNamespace = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(value.GetType());
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            using var stream = new StringWriter();
            using var writer = XmlWriter.Create(stream, settings);
            serializer.Serialize(writer, value, emptyNamespace);
            return stream.ToString().Replace("\r\n", "");
        }
        
        public static T DeserializeXml<T>(string value)
        {
            var serializer = new XmlSerializer(typeof(T));
            var settings = new XmlReaderSettings()
            {
            };
            using var stream = new MemoryStream();
            using var sWriter = new StreamWriter(stream);
            sWriter.Write(value);
            sWriter.Flush();
            stream.Position = 0;
            using var writer = XmlReader.Create(stream, settings);
            return (T)serializer.Deserialize(writer);
        }
        
        public static string SerializeJson(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj,
                    new JsonSerializerSettings {
                        NullValueHandling = NullValueHandling.Ignore, 
                        Formatting = Formatting.Indented,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    });
            }
            catch
            {
                return string.Empty;
            }
        }

        public static T DeserializeJson<T>(string obj)
        {

            try
            {

                return JsonConvert.DeserializeObject<T>(obj,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch
            {
                return default;
            }
        }

    }
}