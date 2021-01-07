using EntityFramework.DynamicFilters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismDXP.Core;
using TourismDXP.Core.DataModels;
using TourismDXP.Core.DataModels.Portal;

namespace TourismDXP.Data.Context
{


    public class TourismDXPContext : DbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TourismDXPContext>(null);
            base.OnModelCreating(modelBuilder);

            // for soft delete filter 
            modelBuilder.Filter("IsDeleted",
             (ISoftDelete d) => d.IsDeleted, false);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }
        public TourismDXPContext()
            : base("TourismDXPConnection")
        {
        }


        #region ADO DbSets
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public DbSet<Portal> Portals { get; set; }
        public DbSet<PortalUser> PortalUsers { get; set; }
        //Look up 
        public DbSet<LookUpDomain> LookUpDomains { get; set; }
        public DbSet<LookUpDomainValue> LookUpDomainValues { get; set; }

        public virtual DbSet<UserInRole> UserInRole { get; set; }
        public virtual DbSet<UserMap> UserMap { get; set; }

        #endregion


        // Automatically add the times the entity got created/modified
        public override int SaveChanges()
        {
            string tempInfo = String.Empty;

            var entries = ChangeTracker.Entries().ToList();
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].State == EntityState.Unchanged || entries[i].State == EntityState.Detached || entries[i].State == EntityState.Deleted) continue;

                var hasInterfaceInheritDb = entries[i].Entity as BaseEntity;
                if (hasInterfaceInheritDb == null) continue;

                if (entries[i].State == EntityState.Added)
                {
                    var created = entries[i].Property("CreatedOn");
                    if (created != null)
                    {
                        created.CurrentValue = DateTime.Now;
                    }
                }
                if (entries[i].State == EntityState.Modified)
                {
                    var modified = entries[i].Property("ModifiedOn");
                    if (modified != null)
                    {
                        modified.CurrentValue = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }

        #region Methods/Functions
        protected virtual TEntity AttachEntityToContext<TEntity>(TEntity entity) where TEntity : BaseEntity, new()
        {
            //little hack here until Entity Framework really supports stored procedures
            //otherwise, navigation properties of loaded entities are not loaded until an entity is attached to the context
            var alreadyAttached = Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (alreadyAttached == null)
            {
                //attach new entity
                Set<TEntity>().Attach(entity);
                return entity;
            }
            else
            {
                //entity is already loaded.
                return alreadyAttached;
            }
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Core.BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : Core.BaseEntity, new()
        {
            //add parameters to command
            if (parameters != null && parameters.Length > 0)
            {
                for (int i = 0; i <= parameters.Length - 1; i++)
                {
                    var p = parameters[i] as DbParameter;
                    if (p == null)
                        throw new Exception("Not support parameter type");

                    commandText += i == 0 ? " " : ", ";

                    commandText += "@" + p.ParameterName;
                    if (p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Output)
                    {
                        //output parameter
                        commandText += " output";
                    }
                }
            }

            var result = this.Database.SqlQuery<TEntity>(commandText, parameters).ToList();
            bool acd = this.Configuration.AutoDetectChangesEnabled;
            try
            {
                this.Configuration.AutoDetectChangesEnabled = false;

                for (int i = 0; i < result.Count; i++)
                    result[i] = AttachEntityToContext(result[i]);
            }
            finally
            {
                this.Configuration.AutoDetectChangesEnabled = acd;
            }

            return result;
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return this.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        #endregion


    }
}
