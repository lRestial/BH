
using BH.Models;
using BH.viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class itemController : Controller
    {
        ECARDBEntities objBHEntities = new ECARDBEntities();
        // GET: item
        public ActionResult Index()
        {
            var itemlist = objBHEntities.items.ToList();
            return View(itemlist);
        }
        public ActionResult createofitem()
        {
            itemviewmodel objitemmodel = new itemviewmodel();
            objitemmodel.categorySelectlistitem =
                (from objCat in objBHEntities.categories
                 select new SelectListItem()
                 {
                     Text = objCat.categoryName,
                     Value = objCat.categoryId.ToString(),
                     Selected = true
                 });
            return View(objitemmodel);
        }
        [HttpPost]
        public JsonResult createofitem(itemviewmodel objitemmodel)
        {

            if (objitemmodel.imagePath == null || objitemmodel.itemName == null || objitemmodel.itemPrice == 0)
            {
                return Json(new { success = true, Message = "不可空白" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string NewImage = Guid.NewGuid() + Path.GetExtension(objitemmodel.imagePath.FileName);
                objitemmodel.imagePath.SaveAs(Server.MapPath("~/images/" + NewImage));
                items objItem = new items();
                objItem.imagePath = "~/images/" + NewImage;
                objItem.categoryId = objitemmodel.categoryId;
                objItem.Description = objitemmodel.Description;
                objItem.itemID = "item0000" + string.Format("{0:yyyyddHHmmssfff}", DateTime.Now);
                objItem.itemName = objitemmodel.itemName;
                objItem.itemPrice = objitemmodel.itemPrice;
                objBHEntities.items.Add(objItem);
                objBHEntities.SaveChanges();
                return Json(new { success = true, Message = "商品成功加入" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult deleteofitem(string itemid)
        {
            var itemdelete = objBHEntities.items.Where(m => m.itemID == itemid).FirstOrDefault();
            objBHEntities.items.Remove(itemdelete);
            objBHEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult editofitem(string itemid)
        {
            var itemedit = objBHEntities.items.Where(m => m.itemID == itemid).FirstOrDefault();
            return View(itemedit);
        }
        [HttpPost]
        public ActionResult editofitem(items items,string itemimagePath) 
        {
           
            objBHEntities.Entry(items).State = EntityState.Modified;
            items.imagePath = itemimagePath;
            objBHEntities.SaveChanges();
            return RedirectToAction("Index");                                  
        }
       
    }
}