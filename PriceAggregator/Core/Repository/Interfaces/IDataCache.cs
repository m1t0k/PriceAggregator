using System;

namespace PriceAggregator.Core.Repository.Interfaces
{
    public interface IDataCache : IDisposable
    {
        void PopulateCache();
    }
}