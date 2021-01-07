using System.Data.Entity.Migrations;
using System.Linq;
using TourismDXP.Core.DataModels;
using TourismDXP.Core.Enums;
using TourismDXP.Data.Context;

namespace TourismDXP.Data.Configuration
{
    public class Configuration : DbMigrationsConfiguration<TourismDXPContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TourismDXPContext context)
        {
            #region Roles
            var adminRole = context.Roles.Where(x => x.RoleName == UserRole.Admin.ToString() && x.IsSeeded == true).FirstOrDefault();
            if (adminRole == null)
            {
                Role role = new Role()
                {
                    RoleName = UserRole.Admin.ToString(),
                    IsDeleted = false,
                    IsSeeded = true
                };
                context.Roles.Add(role);
                adminRole = role;
            }

            var superAdminRole = context.Roles.Where(x => x.RoleName == UserRole.SuperAdmin.ToString() && x.IsSeeded == true).FirstOrDefault();
            if (superAdminRole == null)
            {
                Role role = new Role()
                {
                    RoleName = UserRole.SuperAdmin.ToString(),
                    IsDeleted = false,
                    IsSeeded = true
                };
                context.Roles.Add(role);
                superAdminRole = role;
            }


            var affiliateRole = context.Roles.Where(x => x.RoleName == UserRole.Affiliate.ToString() && x.IsSeeded == true).FirstOrDefault();
            if (affiliateRole == null)
            {
                Role role = new Role()
                {
                    RoleName = UserRole.Affiliate.ToString(),
                    IsDeleted = false,
                    IsSeeded = true,
                    CategoryId = (int)UserType.Reseller
                };
                context.Roles.Add(role);
                affiliateRole = role;
            }

            var agencyRole = context.Roles.Where(x => x.RoleName == UserRole.Agency.ToString() && x.IsSeeded == true).FirstOrDefault();
            if (agencyRole == null)
            {
                Role role = new Role()
                {
                    RoleName = UserRole.Agency.ToString(),
                    IsDeleted = false,
                    IsSeeded = true,
                    CategoryId = (int)UserType.Reseller
                };
                context.Roles.Add(role);
                agencyRole = role;
            }

            var resellerRole = context.Roles.Where(x => x.RoleName == "Value Add Reseller" && x.IsSeeded == true).FirstOrDefault();
            if (resellerRole == null)
            {
                Role role = new Role()
                {
                    RoleName = "Value Add Reseller",
                    IsDeleted = false,
                    IsSeeded = true,
                    CategoryId = (int)UserType.Reseller
                };
                context.Roles.Add(role);
                resellerRole = role;
            }

            var clientRole = context.Roles.Where(x => x.RoleName == UserRole.Client.ToString() && x.IsSeeded == true).FirstOrDefault();
            if (clientRole == null)
            {
                Role role = new Role()
                {
                    RoleName = UserRole.Client.ToString(),
                    IsDeleted = false,
                    IsSeeded = true,
                    CategoryId = (int)UserType.Client
                };
                context.Roles.Add(role);
                clientRole = role;
            }

            context.SaveChanges();
            #endregion

        }
    }
}
