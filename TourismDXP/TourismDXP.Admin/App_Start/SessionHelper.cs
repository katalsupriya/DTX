using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourismDXP.Core.Enums;
using TourismDXP.Services.Classes;

namespace TourismDXP.Admin.App_Start
{
    public static class SessionHelper
    {
        public static string UserId
        {
            get
            {
                var userId = Convert.ToString(HttpContext.Current.Session["UserId"]);

                if (string.IsNullOrEmpty(userId))
                    throw new ArgumentNullException("UserId is null");

                return userId;
            }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public static bool IsUserSuperAdmin
        {
            //get; set;
            get
            {
                var userId = Convert.ToString(HttpContext.Current.Session["UserId"]);
                if (!string.IsNullOrEmpty(userId))
                {
                    var roleDetail = RoleManager.GetUserRoleByUserId(Convert.ToInt32(userId));
                    if (roleDetail != null)
                    {
                        if (roleDetail.RoleName == UserRole.SuperAdmin.ToString())
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static void KillCurrentSession()
        {
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();
        }

        /// <summary>
        /// User Roles
        /// </summary>
        public static string UserRoles
        {
            get
            {
                var userRoles = Convert.ToString(HttpContext.Current.Session["UserRole"]);

                if (string.IsNullOrEmpty(userRoles))
                    return "";

                return userRoles;
            }
            set { HttpContext.Current.Session["UserRole"] = value; }
        }

    }
}