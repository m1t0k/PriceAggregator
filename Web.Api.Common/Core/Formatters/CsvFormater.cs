using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using PriceAggregator.Core.DataEntity.Base;

namespace Web.Api.Common.Core.Formatters
{
    public class CsvFormater : BufferedMediaTypeFormatter
    {
        public CsvFormater()
        {
            // Add the supported media type.
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));

            SupportedEncodings.Add(new UTF8Encoding(false));
            SupportedEncodings.Add(Encoding.GetEncoding("iso-8859-1"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            if (type.BaseType == typeof(BaseEntity))
            {
                return true;
            }

            var enumerableType = typeof(IEnumerable<BaseEntity>);
            return enumerableType.IsAssignableFrom(type);
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            var items = value as IEnumerable<BaseEntity>;

            if (value == null || (type.BaseType != typeof(BaseEntity) && (items == null || !items.Any())))
                return;


            var itemType = items != null ? items.FirstOrDefault()?.GetType() : type;
            if (itemType == null)
            {
                throw new ArgumentNullException(nameof(itemType));
            }

            var propertyList =
                itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(item => item.Name).Union(
                    type.GetFields(BindingFlags.Instance | BindingFlags.Public).Select(item => item.Name))
                    .OrderBy(item => item)
                    .ToArray();

            var effectiveEncoding = SelectCharacterEncoding(content.Headers);
            using (var writer = new StreamWriter(writeStream, effectiveEncoding))
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        WriteItem(item, item.GetType(), propertyList, writer);
                    }
                }
                else
                {
                    WriteItem(value, type, propertyList, writer);
                }
            }
        }

        private void WriteItem(object entity, Type type, string[] propertyList, StreamWriter writer)
        {
            var csvItem = new StringBuilder();
            const char separator = ';';

            for (var i = 0; i < propertyList.Length; i++)
            {
                csvItem.Append(type.GetProperty(propertyList[i]).GetValue(entity));
                if (i + 1 != propertyList.Length)
                    csvItem.Append(separator);
            }
            writer.Write(csvItem.ToString());
        }
    }
}