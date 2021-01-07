using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TourismDXP.Core.Helper;
using TourismDXP.Core.Models;
using TourismDXP.Data.Context;

namespace TourismDXP.Services.Classes
{
    public class UserManager
    {
        /// <summary>
        /// Login user 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static async Task<UserModel> Login(LoginModel login)
        {
            UserModel loginUser = new UserModel();
            try
            {
                //hash password 
                var hashPassword = CipherHelper.EncryptPassword(login.Password + "_" + login.Email.ToLower());
                var dc = new TourismDXPContext();

                var user = dc.Users.FirstOrDefault(x => x.Email == login.Email && x.Password == hashPassword);
                if (user != null)
                {
                    //var roles = await GetUserRoles(user.Id);
                    //string role = string.Join(",", roles);
                    loginUser.Id = user.Id;
                    loginUser.Email = user.Email;
                    loginUser.Role = user.RoleId.ToString();
                    loginUser.RoleName = user.Role;
                }
            }
            catch (Exception ex)
            {
               Serilog.Log.Error(ex, "UserManager=>>Login");
            }
            return loginUser;
        }

        /// <summary>
        /// Update user code 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<bool> UpdateUserCode(string email, string code, bool isUsed)
        {
            bool status = false;
            try
            {
                var dc = new TourismDXPContext();
                var user = dc.Users.FirstOrDefault(x => x.Email == email);
                if (user != null)
                {
                    user.Code = code;
                    user.IsCodeUsed = isUsed;
                    status = await dc.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                status = false;
                Serilog.Log.Error(ex, "UserManager=>>UpdateUserCode");
            }
            return status;
        }

        /// <summary>
        /// Login user 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public static async Task<UserModel> IsEmailExist(string email)
        {
            UserModel userModel = new UserModel();
            var userEmail = email;
            try
            {
                var dc = new TourismDXPContext();
                var user = await dc.Users.FirstOrDefaultAsync(x => x.Email == userEmail);
                if (user != null && user.Id > 0)
                {
                    userModel.Id = user.Id;
                    userModel.FirstName = user.FirstName;
                    userModel.LastName = user.LastName;
                    userModel.Email = user.Email;
                    userModel.Password = user.Password;
                    userModel.Phone = user.Phone;
                    userModel.RoleId = user.RoleId;
                    userModel.IsDeleted = (bool)user.IsDeleted;
                    userModel.Role = user.Role;
                    userModel.Username = user.Username;
                    userModel.Code = user.Code;
                    userModel.IsCodeUsed = user.IsCodeUsed;
                }
            }
            catch (Exception ex)
            {
               Serilog.Log.Error(ex, "UserManager=>>IsEmailExist");
            }
            return userModel;
        }
      
      
    }
}
