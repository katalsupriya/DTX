using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourismDXP.Core.DataModels;
using TourismDXP.Core.Helper;
using TourismDXP.Core.Models;
using TourismDXP.Data.Context;

namespace TourismDXP.Services.Classes
{
   public class AccountManager
    {
        /// <summary>
        /// Get User By Email And Code
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static User GetUserByEmailAndCode(string Email, string Code)
        {
            var dc = new TourismDXPContext();
            var user = dc.Users.FirstOrDefault(x => x.Email == Email && x.Code == Code);
            try
            {
                return user;
            }
            catch (Exception ex)
            {
                return user;
            }
        }

        /// <summary>
        /// Reset Admin Password
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Email"></param>
        public static Tuple<bool, string> ResetUserPassword(string Password, string Email)
        {
            var dc = new TourismDXPContext();
            var user = dc.Users.FirstOrDefault(x => x.Email == Email && x.IsCodeUsed == false);
            bool status = false;
            string message = string.Empty;
            try
            {
                if(user != null)
                {
                    var hashPassword = CipherHelper.EncryptPassword(Password + "_" + Email.ToLower());
                    user.Password = hashPassword;
                    user.IsCodeUsed = true;
                    status = dc.SaveChanges() > 0;
                    message = "Password changed successfully";
                }
                else
                {
                    status = false;
                    message = "Token is expired";
                }
            }
            catch(Exception ex)
            {
                status = false;
                message = "Something went wrong";
            }

            return Tuple.Create(status, message);
        }
    }
}
