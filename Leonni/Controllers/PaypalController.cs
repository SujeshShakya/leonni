using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Leonni.Core;
using Leonni.Core.Controllers;
using Leonni.Models.ViewModels;

namespace Leonni.Controllers
{
    public class PaypalController : LeonniApplicationController
    {
        private readonly string paymentTokenPassword = "a$f%*^$^&($^&ym@8o99k30n";

        public ActionResult PostProcessOrder() 
        {
            string txnID = Request["txn_id"];
            //string receipt_id = Request["receipt_id"];
            //string custom = Request["custom"];
            string invoice_id = Request["invoice"];

            long invoiceID = 0;
            //Invoice invoice = new Invoice();

            if (!string.IsNullOrEmpty(invoice_id))
            {

                if (long.TryParse(invoice_id, out invoiceID))
                {
                    //invoice = _repoInvoice.GetSingle(x => x.ID == invoiceID);

                    if (!string.IsNullOrEmpty(txnID))
                    {
                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + '/';
                        string detailLink = baseUrl + "billing/invoicedetails?id=" + invoiceID;
                        //EmailSender.PaymentSuccessUser(invoice.CustomerEmail, Request["payment_gross"], txnID);
                        //EmailSender.PaymentSuccessAdmin("naresh@designsandapplications.com", txnID, Request["payment_gross"], detailLink);
                        //invoice.TransactionID = txnID;
                        //invoice.IsPaid = true;

                        //_repoInvoice.Edit(invoice);
                        TempData["message"] = "You have paid the invoice successfully.";
                    }
                }

            }

            //@binesh : changed by binesh not appropriate code naresh, just removed the error 
            //return RedirectToAction("Message", "Home");
            return RedirectToAction("Advertisement", "ControlPanel");
        }

        public ActionResult Pay(string id)
        {
            //if (!string.IsNullOrEmpty(id))
            //{
            //    id = HttpUtility.UrlDecode(id).Replace(' ', '+');
            //    string invoiceID = Encryptor.DecryptString(id, paymentTokenPassword);

            //    if (!string.IsNullOrEmpty(invoiceID))
            //    {
            //        /// in the following view 
            //        /// we populate the paypal form and post it
            //        var invoice = _repoInvoice.GetSingle(x => x.ID == long.Parse(invoiceID));
            //        return View(invoice);
            //    }
            //}
            return View();
        }

        [HttpPost]
        public ActionResult PaymentForm(int clickCount)
        {
            double perClick = 0.5;
            ViewBag.Clicks = clickCount;

            return View(new PaypalModel
            {
                Amount = (clickCount * perClick).ToString(),
                Quantity = "1".ToString()
            });
        }
       
    }
}
