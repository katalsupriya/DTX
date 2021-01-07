using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismDXP.Core.DataModels;
using TourismDXP.Core.Models;
using TourismDXP.Data.Context;

namespace TourismDXP.Services.Classes
{
    public class LookUpManager
    {

        /// <summary>
        /// Used to add Update /Add LookUpDomain
        /// </summary>
        /// <param name="personItem"></param>
        /// <returns></returns>
        public static bool UpdateLookUpDomain(LookUpDomainModel lookUpDomainModel)
        {
            var dc = new TourismDXPContext();
            var lookUpDomain = dc.LookUpDomains.FirstOrDefault(x => x.Code == lookUpDomainModel.Code && !x.IsDeleted);
            try
            {
                // for add new record
                if (lookUpDomain == null)
                {
                    lookUpDomain = new LookUpDomain
                    {
                        // Id = Guid.NewGuid(),
                        Code = lookUpDomainModel.Code,
                        Description = lookUpDomainModel.Description
                    };
                    dc.LookUpDomains.Add(lookUpDomain);
                    dc.SaveChanges();
                }
                // for update 
                else
                {
                    lookUpDomain.Code = lookUpDomainModel.Code;
                    lookUpDomain.Description = lookUpDomainModel.Description;
                    lookUpDomain.IsDeleted = false;
                    lookUpDomain.ModifiedOn = DateTime.Now;
                    dc.SaveChanges();
                }
                lookUpDomainModel.Id = lookUpDomain.Id;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// get customer/Person by selected id 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static LookUpDomainModel GetLookUpDomainByCode(string Code)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.LookUpDomains
                    where p.IsDeleted == false && p.Code == Code
                    select new LookUpDomainModel
                    {
                        Id = p.Id,
                        Code = p.Code,
                        Description = p.Description
                    }).FirstOrDefault();
        }
        /// <summary>
        /// get customer/Person by selected id 
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<LookUpDomainValueModel> GetLookUpDomainValueByDomainId(long DomainId)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.LookUpDomainValues
                    where p.IsDeleted == false && p.DomainId == DomainId
                    select new LookUpDomainValueModel
                    {
                        Id = p.Id,
                        DomainId = p.DomainId,
                        DomainText = p.DomainText,
                        IsActive = p.IsActive,
                        DomainValue = p.DomainValue,
                        DomainValueParentId = p.DomainValueParentId
                    }).ToList();
        }
        /// <summary>
        /// get lookUpDomainValue based on domainValue 
        /// </summary>
        /// <param name="domainValue"></param>
        /// <returns></returns>
        public static LookUpDomainValueModel GetLookUpDomainValueByDomainValue(string domainValue)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.LookUpDomainValues
                    where p.IsDeleted == false && p.DomainValue == domainValue
                    select new LookUpDomainValueModel
                    {
                        Id = p.Id,
                        DomainId = p.DomainId,
                        DomainText = p.DomainText,
                        IsActive = p.IsActive,
                        DomainValue = p.DomainValue,
                        DomainValueParentId = p.DomainValueParentId
                    }).FirstOrDefault();
        }


        /// <summary>
        /// get lookUpDomainValue based on id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static LookUpDomainValueModel GetLookUpDomainValueById(int id)
        {
            var dc = new TourismDXPContext();
            return (from p in dc.LookUpDomainValues
                    where p.IsDeleted == false && p.Id == id
                    select new LookUpDomainValueModel
                    {
                        Id = p.Id,
                        DomainId = p.DomainId,
                        DomainText = p.DomainText,
                        IsActive = p.IsActive,
                        DomainValue = p.DomainValue,
                        DomainValueParentId = p.DomainValueParentId
                    }).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookUpDomainValueModel"></param>
        /// <returns></returns>
        public static bool UpdateLookUpDomainValue(LookUpDomainValueModel lookUpDomainValueModel)
        {
            var dc = new TourismDXPContext();
            var lookUpDomainValue = dc.LookUpDomainValues.FirstOrDefault(x => x.Id == lookUpDomainValueModel.Id &&
            !x.IsDeleted && x.IsActive);
            try
            {
                // for add new record
                if (lookUpDomainValue == null)
                {
                    lookUpDomainValue = new LookUpDomainValue
                    {
                        // Id = Guid.NewGuid(),
                        DomainId = lookUpDomainValueModel.DomainId,
                        DomainValueParentId = lookUpDomainValueModel.DomainValueParentId,
                        DomainValue = lookUpDomainValueModel.DomainValue,
                        DomainText = lookUpDomainValueModel.DomainText,
                        IsActive = lookUpDomainValueModel.IsActive,
                        CreatedBy = lookUpDomainValueModel.CreatedBy,
                        CreatedOn = DateTime.Now
                    };
                    dc.LookUpDomainValues.Add(lookUpDomainValue);
                    dc.SaveChanges();
                }
                // for update 
                else
                {
                    lookUpDomainValue.DomainValueParentId = lookUpDomainValueModel.DomainValueParentId;
                    lookUpDomainValue.DomainValue = lookUpDomainValueModel.DomainValue;
                    lookUpDomainValue.DomainText = lookUpDomainValueModel.DomainText;
                    lookUpDomainValue.IsActive = lookUpDomainValueModel.IsActive;
                    lookUpDomainValue.IsDeleted = false;
                    lookUpDomainValue.ModifiedOn = DateTime.Now;
                    lookUpDomainValue.ModifiedBy = lookUpDomainValueModel.ModifiedBy;
                    dc.SaveChanges();
                }
                lookUpDomainValueModel.Id = lookUpDomainValue.Id;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// check whether template category(domain value) is duplicate or not
        /// </summary>
        /// <param name="lookUpDomainValueModel"></param>
        /// <returns></returns>
        public static bool IsDomainValueDuplicate(LookUpDomainValueModel lookUpDomainValueModel)
        {
            var dc = new TourismDXPContext();
            var detail = dc.LookUpDomainValues.FirstOrDefault(x => x.DomainText == lookUpDomainValueModel.DomainText && !x.IsDeleted);
            if (detail == null)
                return false;
            if (detail.Id != lookUpDomainValueModel.Id)
                return true;
            return false;
        }
    }
}
