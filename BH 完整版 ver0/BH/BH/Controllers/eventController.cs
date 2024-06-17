using BH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
     public class eventController : Controller
     {
         ECARDBEntities ev = new ECARDBEntities();
         // GET: event
         public ActionResult Index()
         {
             return View();
         }       
         public ActionResult USAfood()
         {
             string a = "美式料理";
             try
             {
                 var eventfood = ev.cuisine.Where(m => m.EV.Contains(a)).OrderBy(i => Guid.NewGuid()).Take(1).ToList();
                 return View(eventfood);
             }
             catch 
             {
                 return View();
             }

         }
         public ActionResult JPfood()
         {
             string a = "日式料理";
             try
             {
                 var eventfood = ev.cuisine.Where(m => m.EV.Contains(a)).OrderBy(i => Guid.NewGuid()).Take(1).ToList();
                 return View(eventfood);
             }
             catch
             {
                 return View();
             }
         }
         public ActionResult SAfood()
         {
             string a = "東南亞料理";
             try
             {
                 var eventfood = ev.cuisine.Where(m => m.EV.Contains(a)).OrderBy(i => Guid.NewGuid()).Take(1).ToList();
                 return View(eventfood);
             }
             catch
             {
                 return View();
             }
         }
         public ActionResult Frenchfood()
         {
             string a = "法式料理";
             try
             {
                 var eventfood = ev.cuisine.Where(m => m.EV.Contains(a)).OrderBy(i => Guid.NewGuid()).Take(1).ToList();
                 return View(eventfood);
             }
             catch
             {
                 return View();
             }
         }
     }
   
}