using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldOfDiscs.Models;
using System.Net.Mail;
using System.Net;
using log4net;

namespace WorldOfDiscs.Controllers
{
    public class UserController : Controller
    {

        //cấu hình ghi log 
        private static log4net.ILog Log { get; set; }
        ILog log = log4net.LogManager.GetLogger(typeof(UserController));

        WorldOfDiscsEntities db = new WorldOfDiscsEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            try{
                User us = db.Users.SingleOrDefault(n => n.Email == user.Email);
                if (us == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    user = null;
                    ViewBag.Success = "Bạn đã đăng ký tài khoản thành công.";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Lỗi: Email này đã được sử dụng để đăng ký !";
                    return View();
                }
            }           
            catch(Exception ex)
            {
                #region Ghi log
                log.Error(ex);
                #endregion
                return null;
            }
                
                
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var v = db.Users.Where(a => a.Email.Equals(Email) && a.Password.Equals(Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.Id.ToString();
                        Session["LogedUserName"] = v.FullName.ToString();

                        return RedirectToAction("Index", "Home");
                    }
                }
                ViewBag.ErrorLogin = "Bạn đã nhập sai email hoặc mặt khẩu.";
                return View();
            }
            catch (Exception ex)
            {
                #region Ghi log
                log.Error(ex);
                #endregion
                return null;
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        
    }
}