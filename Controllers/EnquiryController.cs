using JupiterEcoTech.Classes;
using JupiterEcoTech.Models;
using Microsoft.Security.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JupiterEcoTech.Controllers
{
    [HandleError]
    public class EnquiryController : Controller
    {
        private static GeneralModels generalModels = new GeneralModels();
        private static GeneralClass generalClass = new GeneralClass();
        readonly EnquiryViewModels enquiryViewModels = new EnquiryViewModels();
        // GET: Enquiry
        public ActionResult Index()
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                enquiryViewModels.Product_Names = generalClass.Product_Name().ToList();
                if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && enquiryViewModels.Product_Names != null)
                {
                    return View(enquiryViewModels);
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (enquiryViewModels.Product_Names != null && !string.IsNullOrEmpty(generalModels.country_code))
                        return View(enquiryViewModels);
                }
            }
            catch (Exception e)
            {
                generalModels.ex = e.Message;
                generalClass.Error_method(generalModels, Request.Url.AbsoluteUri);
            }
            ViewBag.StatusCode = "500";
            ViewBag.Message = "Sorry! Unable to Process your Request.";
            return View("error");
        }

        [Obsolete]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> send_message(EnquiryViewModels enquiryViewModels, string recaptcha)
        {
            try
            {
                GeneralModels.CaptchaResponse response = GeneralClass.ValidateCaptcha(recaptcha);
                if (ModelState.IsValid && response.Success)
                {
                    if (await Task.Run(() => generalClass.Send_mail(generalModels, enquiryViewModels,null,Request.Url.AbsoluteUri)))
                        return Json(true,JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                generalModels.ex = e.Message;
                generalClass.Error_method(generalModels, Request.Url.AbsoluteUri);
            }
            ViewBag.StatusCode = "500";
            ViewBag.Message = "Sorry! Unable to Process your Request.";
            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }
}