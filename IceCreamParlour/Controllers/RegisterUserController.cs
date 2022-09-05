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
            try { 
            if (ModelState.IsValid)
            {
                var check = db.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    _user.JoinDate = DateTime.Now;
                    _user.IsActive = 0;
                    _user.IsDelete = 0;
                    
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
                    if(Result > 0)
                    {
                        ViewBag.SuccessMsg  = "You have just registered your account successfully";

                        //return RedirectToAction("Login");
                    }
                   
                    //return RedirectToAction("Index");
                    
                }
                else
                {
                    ViewBag.error = "Email already exists";
                   
                    return RedirectToAction("Register");
                }
            }
            else
            {
                ViewBag.error = "Oh noo";
                return RedirectToAction("Register");
            }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

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
                    Session["UserId"] = data.FirstOrDefault().User_Id;

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