using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Linq;
using PriceAggregator.Core.Entities;
using PriceAggregator.Core.ExceptionHandling;
using PriceAggregator.Core.Libraries.Logging;

namespace PriceAggregator.Core.Repository.DbContext.Base
{
    public abstract class GenericDbContext : BaseDbContext, IGenericDbContext
    {
        protected GenericDbContext(ILoggingService logger)
            : base(logger)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Retailer> Retailers { get; set; }

        #region Paging

        protected static int GetDefaultPageIndex(int? pageIndex)
        {
            if (!pageIndex.HasValue || pageIndex.Value < 1)
                return 1;
            return pageIndex.Value;
        }

        protected static int GetDefaultPageSize(int? pageSize)
        {
            if (!pageSize.HasValue || pageSize.Value < 1)
                return 20;
            return pageSize.Value;
        }

        #endregion

        #region IBaseDbContext

        public DbSet GetDbSet<T>() where T : BaseEntity
        {
            if (typeof (T) == typeof (Brand))
                return Brands;

            if (typeof (T) == typeof (Category))
                return Categories;

            if (typeof (T) == typeof (Retailer))
                return Retailers;

            throw new Exception();
        }


        public T GetItem<T>(int id) where T : BaseEntity
        {
            try
            {
                object item = GetDbSet<T>().Find(id);

                if (item == null)
                {
                    throw new ObjectDoesNotExistException();
                }

                return (T) item;
            }
            catch (ObjectDoesNotExistException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        public List<T> GetList<T>(int id, int? pageIndex, int? pageSize, string sortName) where T : BaseEntity
        {
            return GetList(GetDbSet<T>() as IQueryable<T>, id, pageIndex, pageSize, sortName);
        }

        public List<T> GetList<T>(IQueryable<T> items, int id, int? pageIndex, int? pageSize, string sortName)
            where T : BaseEntity
        {
            if (!pageIndex.HasValue)
                return items.AsQueryable().OrderByField(sortName, true).ToList();

            int page = GetDefaultPageIndex(pageIndex);
            int size = GetDefaultPageSize(pageSize);

            return
                items.AsQueryable()
                    .OrderByField(sortName, true)
                    .Skip((page - 1)*size)
                    .Take(size).ToList();
        }


        public void CreateItem<T>(int id, T item) where T : BaseEntity
        {
            try
            {
                DbSet dbSet = GetDbSet<T>();
                var found = (T) dbSet.Find(id);
                if (found != null)
                {
                    throw new ObjectAlreadyExistsException();
                }

                dbSet.Add(item);
                Entry(item).State = EntityState.Added;
                SaveChanges();
            }
            catch (ObjectAlreadyExistsException)
            {
                throw;
            }
            catch (UpdateException e)
            {
                Logger.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        public void UpdateItem<T>(int id, T item) where T : BaseEntity
        {
            try
            {
                DbSet dbSet = GetDbSet<T>();
                var found = (T) dbSet.Find(id);
                if (found == null)
                {
                    throw new ObjectDoesNotExistException();
                }

                dbSet.Add(item);
                Entry(item).State = EntityState.Added;
                SaveChanges();
            }
            catch (ObjectDoesNotExistException)
            {
                throw;
            }
            catch (UpdateException e)
            {
                Logger.Error(e);
                throw;
            }

            catch (DbUpdateException e)
            {
                Logger.Error(e.InnerException);
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }


        public void DeleteItem<T>(int id) where T : BaseEntity
        {
            try
            {
                DbSet dbSet = GetDbSet<T>();
                object entity = dbSet.Find(id);
                if (entity == null)
                {
                    throw new ObjectDoesNotExistException();
                }
                dbSet.Remove(entity);
                SaveChanges();
            }
            catch (ObjectDoesNotExistException)
            {
                throw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;
            }
        }

        #endregion
    }
}