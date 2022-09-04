using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IceCreamParlour.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity;
using System.Web.Helpers;

namespace IceCreamParlour.Areas.Local.Controllers
{

    public class LoginAdminController : Controller
    {
        private DB_Entities _db = new DB_Entities();
        // GET: Home
        public ActionResult Index()
        {
            if (Session["Admin_Id"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //GET: Register

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Admin _user)
        {
            if (ModelState.IsValid)
            {
                var check = _db.Admins.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.IsActive = 0;
                    _user.IsDelete = 0;
                    _db.Configuration.ValidateOnSaveEnabled = false;
                    _db.Admins.Add(_user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {

                var f_password = GetMD5(password);
                var data = _db.Admins.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) && s.IsActive ==0).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["Name"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["Admin_Id"] = data.SingleOrDefault().Admin_Id;
                    Session["Roles"] = data.FirstOrDefault().Roles;
                    return RedirectToAction("Index","Books");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    TempData["msg"] = "<script>alert('Your account does not exist or has been locked !!!');</script>";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public ActionResult ChangePass()
        {
            //if (Session["Name"] == null)
            //{
            //    return RedirectToAction("Login");
            //}
            //else 
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(string Password,string newPassword,string Confirmpwd)
        {
            Admin objadmin = new Admin();
            string ad = Session["Name"].ToString();
            int id = int.Parse(Session["Admin_Id"].ToString());
            var login = _db.Admins.Where(u => u.Name.Equals(ad) && u.Admin_Id.Equals(id)).FirstOrDefault();
            var f_pass = GetMD5(Password);
            if(login.Password == f_pass)
            {
                if (Confirmpwd == newPassword)
                {
                    login.ConfirmPassword = GetMD5(Confirmpwd);
                    login.Password = GetMD5(newPassword);
                    var str = GetMD5(newPassword);                                     
                    //_db.Entry(login).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["msg"] = "<script>alert('Password has been changed successfully !!!');</script>";
                }
                else
                {
                    TempData["msg"] = "<script>alert('New password match !!! Please check');</script>";
                }
            }
            else
            {
                TempData["msg"] = "<script>alert('Old password not match !!! Please check entered old password');</script>";
            }
            return View();
        }


        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}