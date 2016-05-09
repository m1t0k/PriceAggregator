using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PriceAggregator
{
    //using System.Web.Script.Serialization;
    //using System.Json;
    //using Newtonsoft.Json;

    namespace Westwind.Web.WebApi
    {
        public class JsonNetFormatter : MediaTypeFormatter
        {
            public JsonNetFormatter()
            {
                SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            }

            public override bool CanWriteType(Type type)
            {
                // don't serialize JsonValue structure use default for that
                //if (type == typeof(JsonValue) || type == typeof(JsonObject) || type == typeof(JsonArray))
                //  return false;

                return true;
            }


            public override bool CanReadType(Type type)
            {
                // if (type == typeof (IKeyValueModel))
                //   return false;


                return true;
            }


            public override Task<Object> ReadFromStreamAsync(
                Type type,
                Stream readStream,
                HttpContent content,
                IFormatterLogger formatterLogger
                )
            {
                Task<object> task = Task<object>.Factory.StartNew(() =>
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    };

                    var sr = new StreamReader(readStream);
                    var jreader = new JsonTextReader(sr);

                    var ser = new JsonSerializer();
                    ser.Converters.Add(new IsoDateTimeConverter());

                    object val = ser.Deserialize(jreader, type);
                    return val;
                });

                return task;
            }

            public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                TransportContext transportContext)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                    };

                    string json = JsonConvert.SerializeObject(value, Formatting.Indented,
                        new JsonConverter[1] {new IsoDateTimeConverter()});

                    byte[] buf = Encoding.Default.GetBytes(json);
                    writeStream.Write(buf, 0, buf.Length);
                    writeStream.Flush();
                });

                return task;
            }
        }
    }
}