using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class homeController : Controller
    {
        // GET: home
        public ActionResult Index() //首頁 功能
        {
            return View();
        }
        public ActionResult About() //關於 功能
        {
            return View();
        }
        public ActionResult Event() //抽卡 功能
        {
            return View();
        }

        public ActionResult Service() //服務 功能
        {
            return View();
        }
    }
}