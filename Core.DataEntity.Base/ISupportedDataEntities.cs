using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceAggregator.Core.DataEntity.Base
{
    public interface ISupportedDataEntities
    {
        IEnumerable<Type> GetSupportedDataEntities();
    }
}
