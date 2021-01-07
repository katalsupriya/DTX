using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismDXP.Core.DataModels.Portal;
using TourismDXP.Core.Models;
using TourismDXP.Data.Context;

namespace TourismDXP.Services.Classes
{
    public class PortalManager
    {
        /// <summary>
        /// Used to add Update /Add LookUpDomain
        /// </summary>
        /// <param name="personItem"></param>
        /// <returns></returns>
        public static bool Update(PortalModel portalModel)
        {
            var dc = new TourismDXPContext();
            var portal = dc.Portals.FirstOrDefault(x => x.Id == portalModel.Id && !x.IsDeleted);
            try
            {
                // for add new record
                if (portal == null)
                {
                    portal = new Portal
                    {
                        Name = portalModel.Name,
                        Url = portalModel.Url,
                        Icon = portalModel.Icon,
                        IsDeleted = false
                    };
                    dc.Portals.Add(portal);

                }
                // for update 
                else
                {
                    portal.Name = portalModel.Name;
                    portal.Url = portalModel.Url;
                    portal.Icon = portalModel.Icon;
                    portal.IsDeleted = portalModel.IsDeleted;
                    portal.ModifiedOn = DateTime.Now;
                }
                dc.SaveChanges();
                portalModel.Id = portal.Id;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("UpdatePortal", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Get all Portals Data 
        /// </summary>
        /// <returns></returns>
        public static List<PortalModel> GetAll()
        {
            var dc = new TourismDXPContext();
            return (from p in dc.Portals
                    where p.IsDeleted == false
                    select new PortalModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Url = p.Url,
                        Icon = p.Icon
                    }).ToList();
        }

        /// <summary>
        /// Get by Id
        /// </summary>
        /// <returns></returns>
        public static PortalModel GetById(int id)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.Portals
                    where p.IsDeleted == false && p.Id == id

                    select new PortalModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Url = p.Url,
                        Icon = p.Icon
                    }).FirstOrDefault();
        }


        #region Portal user 


        /// <summary>
        /// Used to add Update /Add  PortalUser
        /// </summary>
        /// <param name="personItem"></param>
        /// <returns></returns>
        public static bool UpdatePortalUser(PortalUserModel portalUserModel)
        {
            var dc = new TourismDXPContext();
            var portalUser = dc.PortalUsers.FirstOrDefault(x => x.Id == portalUserModel.Id && !x.IsDeleted);
            try
            {
                // for add new record
                if (portalUser == null)
                {
                    portalUser = new PortalUser
                    {
                        PortalId = portalUserModel.PortalId,
                        PrimaryUserId = portalUserModel.PrimaryUserId,
                        SecondaryUserId = portalUserModel.SecondaryUserId,
                        IsDeleted = false
                    };
                    dc.PortalUsers.Add(portalUser);

                }
                // for update 
                else
                {
                    portalUser.PortalId = portalUserModel.PortalId;
                    portalUser.PrimaryUserId = portalUserModel.PrimaryUserId;
                    portalUser.SecondaryUserId = portalUserModel.SecondaryUserId;
                    portalUser.IsDeleted = portalUserModel.IsDeleted;
                    portalUser.ModifiedOn = DateTime.Now;
                }
                dc.SaveChanges();
                portalUserModel.Id = portalUser.Id;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("UpdateportalUser", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Get Current Poral user  Secondary Portal user Info and other 
        /// </summary>
        /// <returns></returns>
        public static PortalUserModel GetPortalUserByPrimaryUserId(int primaryUserId)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.PortalUsers
                    where p.IsDeleted == false && p.PrimaryUserId == primaryUserId

                    select new PortalUserModel
                    {
                        Id = p.Id,
                        PortalId = p.PortalId,
                        PrimaryUserId = p.PrimaryUserId,
                        SecondaryUserId = p.SecondaryUserId
                    }).FirstOrDefault();
        }
        #endregion


    }
}
