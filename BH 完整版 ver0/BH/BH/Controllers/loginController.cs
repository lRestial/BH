using BH.Models;
using BH.viewmodel;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class loginController : Controller
    {
        ECARDBEntities logindata = new ECARDBEntities();
        // GET: login
        public ActionResult Index(loginviewmodel acc)
        {

            acc.username = Session["username"].ToString();
            var accountdetail = logindata.login.Where(m => m.username == acc.username).FirstOrDefault();
            return View(accountdetail);
        }
        public ActionResult editoflogin(string username)
        {
            var loginedit = logindata.login.Where(m => m.username == username).FirstOrDefault();
            return View(loginedit);
        }
        [HttpPost]
        public ActionResult editoflogin(login login)
        {
            logindata.Entry(login).State = EntityState.Modified;
            login.username = login.username;
            login.password = login.password;
            logindata.SaveChanges();
            return RedirectToAction("Index","home");
        }
        public ActionResult Loginfirst()
        {
            return View();
        }
        public ActionResult Logincreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Loginfirst(loginviewmodel login)
        {         
            ScryptEncoder encoder = new ScryptEncoder();
            var userdetails = logindata.login.Where(m => m.username == login.username).FirstOrDefault();
            login.loginerrormessage = "帳號或密碼錯誤";
            if (userdetails == null)
            {               
                return View("Loginfirst", login);

            }
            bool encoderpassword = encoder.Compare(login.password, userdetails.password);
            if (encoderpassword == true)
            {
                Session["username"] = userdetails.username;
                Session["password"] = userdetails.password;
                return RedirectToAction("Index", "home");
            }
            else
            {
                return View("Loginfirst", login);
            }         
        }
        [HttpPost]
        public ActionResult Logincreate(accountcreateviewmodel account)
        {
            ScryptEncoder encoder = new ScryptEncoder();

            login objlogin = new login();
            objlogin.username = account.username;
            objlogin.password = encoder.Encode(account.password);
            objlogin.phone = account.phone;
            objlogin.email = account.email;
            objlogin.address = account.address;
            logindata.login.Add(objlogin);
            logindata.SaveChanges();
            if (logindata != null)
            {
                Session["username"] = account.username;
                Session["password"] = account.password;
                return RedirectToAction("Index", "home");
            }
            else
            {
                return View();
            }
            //   return Json(new { success = true, Message = "成功建立" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsUserExists(accountcreateviewmodel account)
        {

            return Json(!logindata.login.Any(m => m.username == account.username || m.phone == account.phone || m.email == account.email), JsonRequestBehavior.AllowGet);
        }
        public ActionResult loginout()
        {
            string username = (string)Session["username"];

            Session.Abandon();
            return RedirectToAction("Loginfirst", "login");
        }
        public ActionResult Loginmanage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Loginmanage(manageloginviewmodel manage)
        {
            var userdetails = logindata.managelogin.Where(m => m.managename == manage.managename && m.managepassword == manage.managepassword).FirstOrDefault();
            if (userdetails == null)
            {
                manage.loginerrormessage = "帳號或密碼錯誤";

                return View("Loginmanage", manage);

            }
            else
            {
                Session["username"] = userdetails.managename;
                Session["password"] = userdetails.managepassword;
                Session["manage"] = userdetails.managename;
                return RedirectToAction("Index", "home");
            }
        }
    }
}