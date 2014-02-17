using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Migrations;
using DataAccess.Migrations.easygenerator.DataAccess.Migrations;
using DomainModel.Entities;

namespace DataAccess
{
    public class DataContext : DbContext, IDataContext, IUnitOfWork
    {
        static DataContext()
        {
            try
            {
                var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
                Database.SetInitializer<DataContext>(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataContext()
            : this("DefaultConnection")
        {
        }

        public DataContext(string connectionString)
            : base(connectionString)
        {
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, args) => DateTimeObjectMaterializer.Materialize(args.Entity);
        }

        public DbSet<User> Users { get; set; }

        public IDbSet<T> GetSet<T>() where T : Entity
        {
            return Set<T>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<Guid>().Where(p => p.Name == "Id").Configure(p => p.IsKey());

            modelBuilder.Entity<User>().Property(e => e.Email).IsRequired().HasMaxLength(254);
            modelBuilder.Entity<User>().Property(e => e.PasswordHash).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.ParentId).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Skype).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Sex).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.City).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Birthday).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.AboutMySelf).IsOptional();
            modelBuilder.Entity<User>().Property(e => e.Language).IsRequired().HasMaxLength(10);

            base.OnModelCreating(modelBuilder);
        }

        public void Save()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                foreach (var validationErrors in exception.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                throw;
            }

        }
    }
}
