using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity;

namespace Core.DataAccess.Tests
{
    [TestClass]
    public class MsSqlDataAccessProviderUnitTest
    {
        private Category _category;
        private IGenericDataAccessProvider<Category> _dbContext;

        [TestInitialize]
        public void SetUp()
        {
            var loggerMock = new Mock<Lazy<ILoggingService>>();
            _category = new Category
            {
                Id = 0,
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
            _dbContext = new MsSqlDataAccessProvider<Category>(loggerMock.Object);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _dbContext?.Dispose();
        }

        #region AsyncTest
        [TestMethod]
        public void DoMsSqlDataAccessProviderAsyncTest()
        {
            GetCategoryAsync();
            InsertCategoryAsync();
            UpdateCategoryAsync();
            DeleteCategoryAsync();
        }

        private  void InsertCategoryAsync()
        {
            var result= _dbContext.CreateItemAsync(_category);
            result.Wait(); 
            Assert.AreEqual(true,result.IsCompleted);
            Assert.AreEqual(false,result.IsCanceled);
            Assert.AreEqual(false,result.IsFaulted);
            Assert.AreEqual(1, result.Result);
        }

        private void UpdateCategoryAsync()
        {
            var name = _category.Name + DateTime.Now;
            _category.Name = name;
            var result = _dbContext.UpdateItemAsync(_category);
            result.Wait();
            Assert.AreEqual(true, result.IsCompleted);
            Assert.AreEqual(false, result.IsCanceled);
            Assert.AreEqual(false, result.IsFaulted);
            Assert.AreEqual(1, result.Result);

            var readResult = _dbContext.GetItemAsync(_category.Id);
            readResult.Wait();
            Assert.AreEqual(true, readResult.IsCompleted);
            Assert.AreEqual(false, readResult.IsCanceled);
            Assert.AreEqual(false, readResult.IsFaulted);
        
            Assert.IsNotNull(readResult.Result);
            Assert.AreEqual(readResult.Result.Name, name);
            
        }

        private void GetCategoryAsync()
        {
            var id = 1;
            var result = _dbContext.GetItemAsync(id);

            result.Wait();
            Assert.AreEqual(true, result.IsCompleted);
            Assert.AreEqual(false, result.IsCanceled);
            Assert.AreEqual(false, result.IsFaulted);
            Assert.IsNotNull(result.Result);

            Assert.AreEqual(1, result.Result.Id);
            Assert.AreEqual("cat 1", result.Result.Name);
            Assert.AreEqual("desc 1", result.Result.Description);
        }

        private void DeleteCategoryAsync()
        {
            var result=_dbContext.DeleteItemAsync(_category.Id);
            result.Wait();

            Assert.AreEqual(true, result.IsCompleted);
            Assert.AreEqual(false, result.IsCanceled);
            Assert.AreEqual(false, result.IsFaulted);
            Assert.AreEqual(1, result.Result);

        }

        #endregion#region SyncTest

        #region SyncTest
        [TestMethod]
        public void DoMsSqlDataAccessProviderTest()
        {
            GetCategory();
            InsertCategory();
            UpdateCategory();
            DeleteCategory();
        }

        private void InsertCategory()
        {
            _dbContext.CreateItem(_category);

            Assert.IsNotNull(_category);
            Assert.AreNotEqual(0, _category.Id);
        }

        private void UpdateCategory()
        {
            var name = _category.Name + DateTime.Now;
            _category.Name = name;
            _dbContext.UpdateItem(_category);

            var item = _dbContext.GetItem(_category.Id);

            Assert.IsNotNull(item);
            Assert.AreEqual(item.Name, name);
            Assert.AreEqual(item.Name, _category.Name);
        }

        private void GetCategory()
        {
            var id = 1;
            var item = _dbContext.GetItem(id);
            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.Id);
            Assert.AreEqual("cat 1", item.Name);
            Assert.AreEqual("desc 1", item.Description);
        }

        private void DeleteCategory()
        {
            _dbContext.DeleteItem(_category.Id);
            var item = _dbContext.GetItem(_category.Id);
            Assert.IsNull(item);
        }

        #endregion
    }
}