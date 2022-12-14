using IceCreamParlour.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using System.Web.Mvc;

namespace IceCreamParlour.Controllers
{
    public class RegisterUserController : Controller
    {
        DbIcecreamParlourEntities db = new DbIcecreamParlourEntities();

        // GET: RegisterUser

       
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "User_Id,Name,Contact,Email,Address,Password,UserType,Card_No,JoinDate,IsActive,IsDelete")] User _user)
        {
            try { 
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                var checkContact = db.Users.FirstOrDefault(s => s.Contact == _user.Contact);
                if (check == null || checkContact == null)
                {
                    
                    _user.Password = GetMD5(_user.Password);
                    _user.JoinDate = DateTime.Now;
                    _user.IsActive = 0;
                    _user.IsDelete = 0;
                        _user.lockEnable = true;
                        _user.LockEndDateUtc = DateTime.Now.AddMinutes(1);
                       
                        db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(_user);
                    var sub = db.Subscriptions.Where(x => x.Subscription_Id == _user.UserType).FirstOrDefault();
                    if (sub != null)
                    {
                        var subPayment = new Subscription_Payment()
                        {
                            User_Id = _user.User_Id,
                            Date = DateTime.Now,
                            Subscription_Id = sub.Subscription_Id,
                            Subscription = sub,
                            User = _user
                        };
                        db.Subscription_Payment.Add(subPayment);
                    }
                    var Result = db.SaveChanges();
                  
                    
                }
                else
                {
                    ViewBag.checkEmail = "1";
                    ViewBag.checkContact = "1";
                    return View(_user);
                }
            }
           
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return Redirect("/RegisterUser/Login");

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
                var data = db.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password) ).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().Name;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["UserId"] = data.FirstOrDefault().User_Id;
                    Session["Name"] = data.FirstOrDefault().Name;
                    Session["Address"] = data.FirstOrDefault().Address;
                    Session["Contact"] = data.FirstOrDefault().Contact;
                    Session["Card_No"] = data.FirstOrDefault().Card_No;

                    return RedirectToAction("Index","Home");
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