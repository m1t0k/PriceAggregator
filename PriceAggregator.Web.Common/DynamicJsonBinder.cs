using System;
using System.IO;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PriceAggregator.Web.Common
{
    public class DynamicJsonAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new DynamicJsonBinder();
        }
    }

    public class DynamicJsonBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var contentType = controllerContext.HttpContext.Request.ContentType;
            if (!contentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
                return null;

            string bodyText;
            using (var stream = controllerContext.HttpContext.Request.InputStream)
            {
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(stream))
                    bodyText = reader.ReadToEnd();
            }

            return string.IsNullOrEmpty(bodyText) ? null : JObject.Parse(bodyText);
        }
    }
}