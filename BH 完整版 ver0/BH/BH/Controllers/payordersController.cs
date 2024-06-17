using BH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class payordersController : Controller
    {
        ECARDBEntities objBHEntities = new ECARDBEntities();
        // GET: payorders
        public ActionResult Index()
        {        
            var payusername = Session["username"].ToString();
            var paylists = objBHEntities.cartpaypal.Where(m => m.username == payusername && m.paypaldate != null && m.paypaltruefalse == "yes").OrderByDescending(m => m.paypaldate).ToList();
            return View(paylists);           
        }
    }
}