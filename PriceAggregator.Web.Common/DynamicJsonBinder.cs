using System;
using System.IO;
using System.Web.Mvc;
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
            bindingContext.ModelState.Clear();
            const string jsonContentType = "application/json";
            if (contentType.StartsWith(jsonContentType, StringComparison.OrdinalIgnoreCase))
            {
                string bodyText;
                using (var stream = controllerContext.HttpContext.Request.InputStream)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(stream))
                        bodyText = reader.ReadToEnd();
                }

                if (!string.IsNullOrEmpty(bodyText))
                {
                    return JObject.Parse(bodyText);
                }

                bindingContext.ModelState.AddModelError("error", "Can't deserialize object.");
                return null;
            }
            bindingContext.ModelState.AddModelError("error", $"ContentType should be '{jsonContentType}'");
            return null;
        }
    }
}