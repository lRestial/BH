using BH.Models;
using BH.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class productController : Controller
    {
        ECARDBEntities objBHEntities = new ECARDBEntities();
        // GET: product
        public ActionResult item0000202101023917895()
        {
            IEnumerable<shoppingviewmodel> products = (from objItem in objBHEntities.items
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
              ).Where(m => m.itemID == "item0000202101023917895").ToList();


            return View(products);
        }
    }
}