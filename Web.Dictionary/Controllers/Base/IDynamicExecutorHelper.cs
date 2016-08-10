using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Web.Dictionary.Controllers.Base
{
    public interface IDynamicExecutorHelper
    {
        IEnumerable<Type> GetSupportedTypes(HttpConfiguration configuration);

        HttpResponseMessage ExecuteWithFormatter(HttpConfiguration configuration, HttpRequestMessage request,
            string typeName, string methodName, object[] parameters,
            object dictionaryItem = null, MediaTypeFormatter formatter = null);

        object Execute(HttpConfiguration configuration, HttpRequestMessage request,
            string typeName, string methodName, object[] parameters,
            object dictionaryItem = null);
    }
}