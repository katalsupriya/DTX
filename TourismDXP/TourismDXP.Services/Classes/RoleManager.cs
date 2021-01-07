using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TourismDXP.Core.DataModels;
using TourismDXP.Core.Enums;
using TourismDXP.Core.Helper;
using TourismDXP.Core.Models;
using TourismDXP.Data.Context;

namespace TourismDXP.Services.Classes
{
    public class RoleManager
    {
 

        /// <summary>
        /// get user role detail by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static RoleModel GetUserRoleByUserId(int userId)
        {
            var dc = new TourismDXPContext();
            return (from user in dc.Users
                    join userRole in dc.UserInRole
                    on user.Id equals userRole.UserId
                    join role in dc.Roles
                    on userRole.RoleId equals role.Id //dc  on p.Id equals userRole.RoleId
                    where userRole.IsDeleted == false
                    && user.Id == userId
                    select new RoleModel
                    {
                        RoleName = role.RoleName,
                        Id = role.Id,
                        ParentId = user.RoleId
                    }).FirstOrDefault();

        }
      
    }
}
