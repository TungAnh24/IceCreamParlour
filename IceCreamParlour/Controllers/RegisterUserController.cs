using IceCreamParlour.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class RegisterUserController : Controller
    {
        DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        public ActionResult Index()
        {
            var lstCategory = db.Books.ToList();
           
            Book book = new Book();


        
            return View(book);
        }
        // GET: RegisterUser

        [HttpGet]
        public ActionResult Register()
        {
            //List<User> li = db.Users.ToList();
            // ViewBag.list = new SelectList(li, "User_Id ");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.JoinDate = DateTime.Now;
                    //_user.IsActive = 1;
                    //_user.IsDelete = 0;
                    //_user.
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
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
                var data = db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
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




    }
}