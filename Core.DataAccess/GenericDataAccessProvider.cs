using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataAccess.Helpers;
using PriceAggregator.Core.DataAccess.Interfaces;
using PriceAggregator.Core.DataEntity.Base;
using PriceAggregator.Core.Exceptions;

namespace PriceAggregator.Core.DataAccess
{
    public abstract class GenericDataAccessProvider<T> : DbContext, IGenericDataAccessProvider<T>
        where T : BaseEntity, new()
    {
        protected Lazy<ILoggingService> Logger;

        protected GenericDataAccessProvider(Lazy<ILoggingService> logger) : base("PriceAggregator")

        {
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            Logger = logger;
        }

        #region Paging

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        #endregion

        #region IBaseDbContext

        public DbSet<T> Items { get; set; }

        public IEnumerable<T> SqlQuery(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<T>(sqlQuery, parameters);
        }

        public async Task<IEnumerable<T>> SqlQueryAsync(string sqlQuery, params object[] parameters)
        {
            return await Database.SqlQuery<T>(sqlQuery, parameters).ToListAsync();
        }

        public T GetItem(int id)
        {
            try
            {
                return Items.Find(id);
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public async Task<T> GetItemAsync(int id)
        {
            try
            {
                return await Items.FindAsync(id);
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }


        public int GetCount()
        {
            try
            {
                return Items.Count();
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public async Task<int> GetCountAsync()
        {
            try
            {
                return await Items.CountAsync();
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }


        private IQueryable<T> GetListInternal(int? pageIndex, int? pageSize, string sortName, bool acsending)
        {

            if (!pageIndex.HasValue)
                return Items.AsQueryable().OrderByField<T>(sortName, acsending);

            if (!pageSize.HasValue)
                pageSize = PageSize;

            return
                Items.AsQueryable()
                    .OrderByField<T>(sortName, acsending)
                    .Skip((pageIndex.Value - 1)*pageSize.Value)
                    .Take(pageSize.Value);
        }

        public IEnumerable<T> GetList(int? pageIndex, int? pageSize, string sortName,bool acsending)
        {
            return GetListInternal(pageIndex, pageSize, sortName, acsending).ToList();
        }

        public async Task<IEnumerable<T>> GetListAsync(int? pageIndex, int? pageSize, string sortName,bool acsending)
        {
            return await GetListInternal(pageIndex, pageSize, sortName, acsending).ToListAsync().ConfigureAwait(false);
        }

        public void CreateItem(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                Items.Add(item);
                SaveChanges();
            }
            catch (UpdateException e)
            {
                Logger.Value.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Value.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public void CreateItems(IEnumerable<T> items)
        {
            try
            {
                if (items == null)
                    throw new ArgumentNullException(nameof(items));
                Configuration.AutoDetectChangesEnabled = false;
                Items.AddRange(items);
                Configuration.AutoDetectChangesEnabled = true;
                SaveChanges();
            }
            catch (UpdateException e)
            {
                Logger.Value.Error(e);
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Value.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public async Task<int> CreateItemAsync(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                Items.Add(item);
                return await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (UpdateException e)
            {
                Logger.Value.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Value.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public void UpdateItem(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (Entry(item).State == EntityState.Detached)
                    Items.Attach(item);
                Entry(item).State = EntityState.Modified;
                SaveChanges();
            }
            catch (UpdateException e)
            {
                Logger.Value.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Value.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public async Task<int> UpdateItemAsync(T item)
        {
            try
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                if (Entry(item).State == EntityState.Detached)
                    Items.Attach(item);
                Entry(item).State = EntityState.Modified;
                return await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (UpdateException e)
            {
                Logger.Value.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Value.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }


        public async Task<int> DeleteItemAsync(int id)
        {
            try
            {
                var item = await Items.FindAsync(id);

                if (item == null) return await new Task<int>(() => 0);

                Items.Remove(item);

                return await SaveChangesAsync().ConfigureAwait(false);
            }
            catch (ObjectDoesNotExistException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        public void DeleteItem(int id)
        {
            try
            {
                var item = Items.Find(id);

                if (item == null) return;

                Items.Remove(item);
                SaveChanges();
            }
            catch (ObjectDoesNotExistException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }

        #endregion
    }
}