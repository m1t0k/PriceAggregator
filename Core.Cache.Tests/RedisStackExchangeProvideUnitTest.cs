using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.Cache;
using PriceAggregator.Core.Cache.Interface;
using PriceAggregator.Core.DataEntity;

namespace Core.Cache.Tests
{
    [TestClass]
    public class RedisStackExchangeProvideUnitTest
    {
        private IDataCacheProvider<Category> _cacheProvider;
        private Category _category;
        private List<Category> _categoryList;

        [TestInitialize]
        public void SetUp()
        {
            var loggerMock = new Mock<ILoggingService>();
            _category = new Category
            {
                Id = 0,
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
            var connectionString = ConfigurationManager.AppSettings["Redis.ConnectionString"];
            _cacheProvider = new RedisStackExchangeProvider<Category>(connectionString);

            _categoryList = new List<Category>();
            for (var i = 1; i < 10000; i++)
            {
                _categoryList.Add(new Category
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Description = Guid.NewGuid().ToString()
                });
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _cacheProvider.Dispose();
        }

        [TestMethod]
        public void DoRedisStackMultipleItemsSyncTest()
        {
            Assert.IsNotNull(_cacheProvider);

            var result = _cacheProvider.AddItems(_categoryList);
            Assert.IsTrue(result);

            var resultList = _cacheProvider.GetItems(_categoryList.Select(item => item.Id));
            Assert.IsNotNull(resultList);
            Assert.IsTrue(_categoryList.SequenceEqual(resultList));

            var replaceResult = _cacheProvider.ReplaceItems(_categoryList);
            Assert.IsFalse(replaceResult.Any(item=>!item.Item2));
            
            _cacheProvider.RemoveItems(_categoryList.Select(item => item.Id));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoRedisStackMultipleItemsAsyncTest()
        {
            Assert.IsNotNull(_cacheProvider);

            var result = _cacheProvider.AddItemsAsync(_categoryList);
            result.Wait();
            Assert.IsTrue(result.Result);

            var resultList = _cacheProvider.GetItemsAsync(_categoryList.Select(item => item.Id));
            Assert.IsNotNull(resultList.Result);
            Assert.IsTrue(_categoryList.SequenceEqual(resultList.Result));

            var replaceResult = _cacheProvider.ReplaceItemsAsync(_categoryList);
            Task.WaitAll(replaceResult.Select(item => item.Item2).ToArray());
            Assert.IsFalse(replaceResult.Any(item => !item.Item2.Result));

            var task=_cacheProvider.RemoveItemsAsync(_categoryList.Select(item => item.Id));
            Assert.IsTrue(task.IsCompleted);
        }


        [TestMethod]
        public void DoRedisStackSyncSingleItemTest()
        {
            Assert.IsNotNull(_cacheProvider);

            var result = _cacheProvider.AddItem(_category);
            Assert.IsTrue(result);

            var item = _cacheProvider.GetItem(_category.Id);
            Assert.IsNotNull(item);
            Assert.AreEqual(_category.Id, item.Id);
            Assert.AreEqual(_category.Name, item.Name);
            Assert.AreEqual(_category.Description, item.Description);

            item.Name += DateTime.Now.ToString();
            result = _cacheProvider.ReplaceItem(item);
            Assert.IsTrue(result);

            result = _cacheProvider.RemoveItem(item.Id);
            Assert.IsTrue(result);
        }
    }
}