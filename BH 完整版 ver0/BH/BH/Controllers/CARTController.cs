using BH.Models;
using BH.paypalviewmodel;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BH.Controllers
{
    public class CARTController : Controller
    {
        ECARDBEntities paydata = new ECARDBEntities();
        // GET: CART
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult shoppingCartThing()
        {
            if (Session["username"] == null || string.IsNullOrWhiteSpace(Session["username"].ToString()))
            {
                Response.Redirect("~/login/Loginfirst");
            }
            else if (Session["username"] == Session["username"])
            {
                var user = Session["username"].ToString();
                Session["CartCounter"] = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).Count();
                var cartdata = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).ToList();
                return View(cartdata);
            }
            return View();           
        }
        public ActionResult delete(string OrderNumber)
        {
            var user = Session["username"].ToString();
            var cartdelete = paydata.cartpaypal.Where(m => m.OrderNumber == OrderNumber && m.paypaltruefalse == null && m.paypaldate == null && m.username == user).FirstOrDefault();
            paydata.cartpaypal.Remove(cartdelete);
            paydata.SaveChanges();
            Session["CartCounter"] = paydata.cartpaypal.Where(n => n.paypaltruefalse == null || n.paypaldate == null || n.username == user).Count();            
            return RedirectToAction("shoppingCartThing");
        }
        public ActionResult checkout()
        {
            if (Session["username"] == null || string.IsNullOrWhiteSpace(Session["username"].ToString()))
            {
                Response.Redirect("~/login/Loginfirst");
            }
            else if (Session["username"] == Session["username"])
            {
                var user = Session["username"].ToString();
                var cartdata = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).ToList();
                return View(cartdata);
            }
            return View();
        }
        private Payment payment;
        private Payment CreatePayment(APIContext apicontext, string redirectUrl)
        {

            var listItems = new ItemList() { items = new List<Item>() };
            var user = Session["username"].ToString();
            var cartdata = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).ToList();
            foreach (var cart in cartdata)
            {
                listItems.items.Add(new Item()
                {
                    name = cart.itemName.ToString(),
                    currency = "TWD",
                    price = cart.UnitPrice.ToString(),
                    quantity = cart.Quantity.ToString(),
                    sku = "sku"
                });
            }
            var payer = new Payer() { payment_method = "paypal" };           
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };          
            var details = new Details()
            {

                tax = "0",
                shipping = "0",
                subtotal = Session["Total"].ToString(),
            };
            var amount = new Amount()
            {
                currency = "TWD",
                total = Session["Total"].ToString(),    
                details = details
            };
            
            var transactionlist = new List<Transaction>();
            transactionlist.Add(new Transaction()
            {
                description = "Transaction  Description",
                invoice_number = Convert.ToString(new Random().Next(10000)),
                amount = amount,
                item_list = listItems
            });
            payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionlist,
                redirect_urls = redirUrls
            };            
            return payment.Create(apicontext);
        }
        private Payment ExecutePayment(APIContext apicontext, string payerId, string paymentId)
        {
            var paymentExecuttion = new PaymentExecution() { payer_id = payerId };
            payment = new Payment() { id = paymentId };
            return payment.Execute(apicontext, paymentExecuttion);
        }
        public ActionResult PaymentWithPaypal()
        {         
            APIContext apicontext = PaypalConfiguration.GetAPIContext();
            try
            {
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {                   
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/CART/PaymentWithPaypal?";
                    var Guid = Convert.ToString((new Random()).Next(10000000));
                    var createPayment = CreatePayment(apicontext, baseURI + "guid=" + Guid);                    
                    var links = createPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links link = links.Current;
                        if (link.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectUrl = link.href;
                            Session.Add(Guid, createPayment.id);
                            return Redirect(paypalRedirectUrl);
                        }
                    }
                }
                else
                {                  
                    var guid = Request.Params["guid"];
                    var excutedPayment = ExecutePayment(apicontext, payerId, Session[guid] as string);                   
                    if (excutedPayment.state.ToString().ToLower() != "approved")
                    {               
                        Session["CartItem"] = null;
                        Session["CartCounter"] = null;
                        Session["OrderId"] = null;
                        Session["Total"] = null;
                        return View("ggg");
                    }
                }
            }
            catch (Exception)
            {               
                Session["CartItem"] = null;
                Session["OrderId"] = null;
                Session["Total"] = null;
                return View("errorpaypal");

            }            
            var user = Session["username"].ToString();
            var cartpaypalcount = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).Count();
            for (int i = 1; i <= cartpaypalcount; i++)
            {
                cartpaypal objcartpaypal = paydata.cartpaypal.Where(n => n.paypaltruefalse == null && n.paypaldate == null && n.username == user).FirstOrDefault();
                if (objcartpaypal != null)
                {
                    paydata.Entry(objcartpaypal).State = EntityState.Modified;
                    objcartpaypal.paypaldate = DateTime.Now;
                    objcartpaypal.paypaltruefalse = "yes";
                    paydata.SaveChanges();
                }
                else
                    break;
            }          
            Session["CartCounter"] = null;           
            Session["Total"] = null;
            return View("successpaypal");
        }
    }
}