using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using PriceAggregator.Core.DbContext;
using PriceAggregator.Core.ExceptionHandling;
using PriceAggregator.Models;

namespace PriceAggregator.Core.DataProvider
{
    public class BrandDataProvider : BaseDataProvider
    {
        public BrandDataProvider() : base()
        {
        }
        
        public BrandDataProvider(IObjectContextAdapter dbContext) : base(dbContext)
        {
        }

        public BrandCollection GetBrands()
        {
            using (var db = new BrandDbContext())
            {
                IOrderedQueryable<Brand> brands = db.DbSet.OrderBy(item => item.Name);
                return new BrandCollection {Items = brands.ToList()};
            }
        }

        public Brand GetBrand(string id)
        {
            using (var db = new BrandDbContext())
            {
                Brand brand = db.DbSet.Find(id);

                if (brand == null)
                {
                    throw new ObjectDoesNotExistException();
                }

                return brand;
            }
        }

        public void CreateBrand(Brand brand)
        {
            using (var db = new BrandDbContext())
            {
                Brand found = db.DbSet.Find(brand.Id);
                if (found != null)
                {
                    throw new ObjectAlreadyExistsException();
                }
                db.DbSet.AddOrUpdate(brand);
                db.SaveChanges();
            }
        }


        public void UpdateBrand(string id, Brand brand)
        {
            using (var db = new BrandDbContext())
            {
                Brand found = db.DbSet.Find(id);
                if (found == null)
                {
                    throw new ObjectDoesNotExistException();
                }
                db.DbSet.AddOrUpdate(brand);
                db.SaveChanges();
            }
        }


        public void DeleteBrand(string id)
        {
            using (var db = new BrandDbContext())
            {
                Brand brand = db.DbSet.Find(id);
                if (brand == null)
                {
                    throw new ObjectDoesNotExistException();
                }
                db.DbSet.Remove(brand);
                db.SaveChanges();
            }
        }
    }
}