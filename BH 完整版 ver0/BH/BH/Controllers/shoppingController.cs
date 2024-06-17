using BH.Models;
using BH.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class shoppingController : Controller
    {
        ECARDBEntities objBHEntities = new ECARDBEntities();
        // GET: shopping
        public ActionResult Index(shoppingviewmodel shop)
        {
            string searching = "";
            searching = shop.searching;
            IEnumerable<shoppingviewmodel> listofshoppingviewmodels = (from objItem in objBHEntities.items
                                                                       join
                                                                        objCat in objBHEntities.categories
                                                                        on objItem.categoryId equals objCat.categoryId
                                                                       select new shoppingviewmodel()
                                                                       {
                                                                           itemID = objItem.itemID,
                                                                           imagePath = objItem.imagePath,
                                                                           itemName = objItem.itemName,
                                                                           Description = objItem.Description,
                                                                           itemPrice = objItem.itemPrice,
                                                                           category = objCat.categoryName,
                                                                       }
            ).ToList();
            if (searching == null)
            {
                return View(listofshoppingviewmodels);
            }
            else if (searching != null)
            {
                return View(listofshoppingviewmodels.Where(m => m.category.Contains(searching) || m.itemName.Contains(searching) || m.Description.Contains(searching)));
            }
            else
            {
                return View(listofshoppingviewmodels);
            }
        }
        [HttpPost]
        public JsonResult Index(string ItemId)
        {
            items objItem = objBHEntities.items.Single(model => model.itemID == ItemId);
            cartpaypal objcartpaypal = new cartpaypal();
            objcartpaypal.OrderNumber = "Order" + string.Format("{0:ddmmyyyyHHmmssfff}", DateTime.Now);
            objcartpaypal.OrderDate = DateTime.Now;
            objcartpaypal.itemID = ItemId;
            objcartpaypal.categoryId = objItem.categoryId;
            objcartpaypal.itemName = objItem.itemName;
            objcartpaypal.imagePath = objItem.imagePath;
            objcartpaypal.Quantity = 1;
            objcartpaypal.UnitPrice = objItem.itemPrice;
            objcartpaypal.Total = objItem.itemPrice;
            objcartpaypal.username = Session["username"].ToString();
            objBHEntities.cartpaypal.Add(objcartpaypal);
            objBHEntities.SaveChanges();           
            var user = Session["username"].ToString();
            return Json(new { Success = true, Counter = objBHEntities.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).Count() }, JsonRequestBehavior.AllowGet);
        }
    }
}