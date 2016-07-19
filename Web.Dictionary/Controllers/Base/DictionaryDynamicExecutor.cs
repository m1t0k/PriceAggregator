using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http.Dependencies;
using Newtonsoft.Json;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DictionaryProvider.Interfaces;

namespace Web.Dictionary.Controllers.Base
{
    public class DictionaryDynamicExecutor
    {
        private readonly Type _type;


        public DictionaryDynamicExecutor(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentNullException(nameof(typeName));

            var assembly = Assembly.GetAssembly(typeof(Category));
            _type = assembly.GetTypes()
                .FirstOrDefault(
                    item => string.Compare(item.Name, typeName, StringComparison.InvariantCultureIgnoreCase) == 0);

            if (_type == null)
                throw new NullReferenceException(nameof(_type));
        }

        public object DesserializeInstance(string instance)
        {
            var serializer = new JsonSerializer();
            return serializer.Deserialize(new StringReader(instance), _type);
        }

        public object Execute(IDependencyResolver dependencyResolver, string methodName, object[] parameters)
        {
            if (dependencyResolver == null)
                throw new ArgumentNullException(nameof(dependencyResolver));

            if (string.IsNullOrWhiteSpace(methodName))
                throw new ArgumentNullException(nameof(methodName));

            var template = typeof(IDictionaryProvider<>);
            var genericType = template.MakeGenericType(_type);
            var method = genericType.GetMethod(methodName);

            var instance = dependencyResolver.GetService(genericType);
            return method.Invoke(instance, parameters);
        }


        public bool IsResultEmpty(object result)
        {
            if (result == null || (result is bool && !(bool) result))
                return true;

            var genericCollectionTemplate = typeof(IEnumerable<>);
            var genericCollectionType = genericCollectionTemplate.MakeGenericType(_type);
            if (!genericCollectionType.IsInstanceOfType(result)) return false;

            var anyMethod = typeof(Enumerable)
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .FirstOrDefault(mi => mi.Name == "Any");
            // we need to specialize it 
            if(anyMethod==null)
                throw new ArgumentNullException(nameof(anyMethod));
             
            anyMethod = anyMethod.MakeGenericMethod(_type);

            return !(bool) anyMethod.Invoke(null, new object[] {result});
        }
    }
}