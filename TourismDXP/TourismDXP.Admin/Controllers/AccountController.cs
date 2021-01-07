using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TourismDXP.Admin.App_Start;
using TourismDXP.Core.Enums;
using TourismDXP.Core.Helper;
using TourismDXP.Core.Models;
using TourismDXP.Services.Classes;

namespace TourismDXP.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// login page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LoginUser()
        {
            return View(new LoginModel());
        }
        /// <summary>
        /// login page method
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> LoginUser(LoginModel login)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("~/Views/Account/LoginUser.cshtml");

                Serilog.Log.Information("User Login Process Start!");

                if (ModelState.IsValid)
                {
                    var user = await UserManager.Login(login);
                    Serilog.Log.Information("User Info:" + user);

                    if (user != null && user.Id > 0)
                    {
                        SessionHelper.UserId = user.Id.ToString();
                        //
                        HttpContext.Session["Username"] = user.Email;
                        HttpContext.Session["Userid"] = user.Id.ToString();
                        TempData["Email"] = user.Email;
                        string[] roles = user.Role.Split(',');
                        if (roles.Count() > 0)
                        {
                            HttpContext.Session["UserRole"] = user.Role;
                        }
                        //TODO: need to remove this after all done 
                        foreach (var role in roles)
                        {
                            if (Convert.ToInt32(role) == (int)UserRole.Admin)
                            {
                                HttpContext.Session["Role"] = "Admin";

                                TempData["Role"] = "Admin";
                                break;
                            }
                            if (Convert.ToInt32(role) == 6)
                            {
                                HttpContext.Session["Role"] = "Admin";

                                TempData["Role"] = "Client";
                                break;
                            }
                        }
                        return RedirectToAction("Index", "Home");

                    }
                }
            }
            catch (Exception ex)
            {
            }

            TempData["UserError"] = "The Email or Password is incorrect.";
            return RedirectToAction("LoginUser");
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="Detail"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPassword(string Detail)
        {
            ForgotPasswordModel model = new ForgotPasswordModel
            {
                Parameter = Detail
            };
            return View(model);
        }

        /// <summary>
        /// Forgot User Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotUserPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var paths = (AppDomain.CurrentDomain.BaseDirectory + @"Views\Shared\EmailTemplate\ForgetPassword.cshtml");
                var EmailBody = System.IO.File.ReadAllText(paths);

                var user = await UserManager.IsEmailExist(model.Email);

                if (user != null)
                {
                    string code = CipherHelper.GenerateCode();

                    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, email = model.Email, token = code }, protocol: Request.Url.Scheme);
                    string body = EmailBody.Replace("#name#", user.Username).Replace("#url#", callbackUrl);
                    string subject = "Forget Password";
                    EmailModel emailModel = new EmailModel
                    {
                        FromEmail = "crm@ethnicitymatters.com",
                        From = "Ethnicity_Matters",
                        ToEmail = user.Email,
                        Subject = "Forget Password",
                        Body = body,

                    };
                    await EmailSender.SendEmailAsync(model.Email, subject, body);

                    await UserManager.UpdateUserCode(model.Email, code, false);
                    return RedirectToAction("ForgotPasswordConfirmation", "Account");


                }
                ModelState.AddModelError("", "User not found");
                return View("~/Views/Account/ForgotPassword.cshtml", model);
            }
            return View("~/Views/Account/ForgotPassword.cshtml", model);
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <param name="userType"></param>
        /// <param name="email"></param>
        /// <returns></returns>

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string token, string userType, string email)
        {
            var obj = new ResetPasswordModel
            {
                Code = token,
                UserType = userType,
                Email = email
            };
            return token == null ? View("Error") : View(obj);
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetUserPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.UserType = "Admin";
            var message = string.Empty;
            if (model.UserType == "Admin")
            {
                var user = AccountManager.GetUserByEmailAndCode(model.Email, model.Code);
                if (user == null)
                {
                    TempData["Error"] = "Token is expired please resend mail.";
                    return RedirectToAction("Logout", "Account");
                }
                var passUpdate = AccountManager.ResetUserPassword(model.Password, model.Email);
                message = passUpdate.Item2;
            }
         
            return RedirectToAction("ResetPasswordConfirmation", "Account", new { userType = model.UserType, message = message });

        }

        /// <summary>
        /// Reset Password Confirmation
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation(string userType, string message)
        {
            var obj = new ResetPasswordModel();
            obj.UserType = userType;
            ViewBag.Message = message;
            return View(obj);
        }


        /// <summary>
        /// Logout 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {

            SessionHelper.KillCurrentSession();
            //TODO remove all this 
            if (HttpContext.Session["Role"] == "Admin")
            {
                HttpContext.Session["Role"] = null;
                HttpContext.Session["Username"] = "";
                HttpContext.Session["AdminId"] = null;
                HttpContext.Session["Token"] = null;
                return RedirectToAction("Index", "Home");
            }
            else if (HttpContext.Session["Role"] == "Client")
            {
                HttpContext.Session["Role"] = null;
                HttpContext.Session["Username"] = "";
                HttpContext.Session["AdminId"] = null;
                HttpContext.Session["Token"] = null;
                //return RedirectToAction("Login", "Client");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                HttpContext.Session["Token"] = null;
                HttpContext.Session["Role"] = null;
                HttpContext.Session["Username"] = "";
                HttpContext.Session["AdminId"] = null;
                return RedirectToAction("LoginUser", "Account");
            }
        }
    }
}