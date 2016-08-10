using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PriceAggergator.Core.Logging.Inteface;
using PriceAggregator.Core.DataEntity;
using PriceAggregator.Core.DataEntity.Base;

namespace PriceAggregator.Core.DataAccess
{
    public class MsSqlDataAccessProvider<T> : GenericDataAccessProvider<T> where T : BaseEntity, new()
    {
        public MsSqlDataAccessProvider(Lazy<ILoggingService> logger) : base(logger)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            try
            {
                base.OnModelCreating(modelBuilder);
             /*   modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

                modelBuilder.Entity<Brand>().HasKey(t => t.Id);
                modelBuilder.Entity<Brand>().Property(t => t.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Brand>().ToTable("brand", "dbo");

                modelBuilder.Entity<Category>().HasKey(t => t.Id);
                modelBuilder.Entity<Category>().Property(t => t.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Category>().ToTable("category", "dbo");

                modelBuilder.Entity<Product>().HasKey(t => t.Id);
                modelBuilder.Entity<Product>().Property(t => t.Id)
                    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                modelBuilder.Entity<Product>().ToTable("product", "dbo");
                */
            }
            catch (Exception e)
            {
                Logger.Value.Error(e);
                throw;
            }
        }
    }
}