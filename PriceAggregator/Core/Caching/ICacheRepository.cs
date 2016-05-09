using System;
using System.Collections.Generic;
using PriceAggregator.Core.Entities;

namespace PriceAggregator.Core.Caching
{
    public interface ICacheRepository
    {
        string GenerateKey<T>(T item) where T : BaseEntity;
        string GenerateKey<T>(int itemId) where T : BaseEntity;

        T GetItem<T>(int itemId, Antlr.Runtime.Misc.Func<int, T> getItemFromDb) where T : BaseEntity;

        void AddItem<T>(T item) where T : BaseEntity;
        void AddAll<T>(IList<Tuple<string, T>> items) where T : BaseEntity;

        void DeleteItem<T>(int itemId) where T : BaseEntity;
        void DeleteAll<T>(List<int> itemIds) where T : BaseEntity;

        List<T> GetAll<T>(Func<List<T>> getItemsFromD)
            where T : BaseEntity;

        void PopulateCache<T>(Func<List<T>> getItemsFromDb)
            where T : BaseEntity;
    }
}