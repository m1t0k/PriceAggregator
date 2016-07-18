using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataEntity.Core
{
    public class SupportedDataEntities : ISupportedDataEntities
    {
        private List<Type> _list =new List<Type>();
        public IEnumerable<Type> GetSupportedDataEntities()
        {
            if (_list.Any()) return _list;

            var entityAssembly = Assembly.GetExecutingAssembly();
            _list =
                (from type in entityAssembly.GetExportedTypes()
                    where
                        type.Namespace == typeof(Category).Namespace && type.IsClass &&
                        type.BaseType == typeof(BaseEntity)
                    select type).OrderBy(item=>item.Name).ToList();
            return _list;
        }
    }
}