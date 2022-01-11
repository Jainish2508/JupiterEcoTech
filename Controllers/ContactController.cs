using JupiterEcoTech.Classes;
using JupiterEcoTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace JupiterEcoTech.Controllers
{
    public class ContactController : Controller
    {
        private static GeneralModels generalModels = new GeneralModels();
        private static GeneralClass generalClass = new GeneralClass();
        // GET: Contact
        public ActionResult Index()
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                if (!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code))
                {
                    return View();
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (!string.IsNullOrEmpty(generalModels.country_code))
                        return View();
                }
            }
            catch(Exception e)
            {
                generalModels.ex = e.Message;
                generalClass.Error_method(generalModels, Request.Url.AbsoluteUri);
            }
            ViewBag.StatusCode = "500";
            ViewBag.Message = "Sorry! Unable to Process your Request.";
            return View("error");
        }

        [Obsolete]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> send_message(ContactViewModels contactViewModels, string recaptcha)
        {
            try
            {
                GeneralModels.CaptchaResponse response = GeneralClass.ValidateCaptcha(recaptcha);
                if(ModelState.IsValid && response.Success)
                {
                    if (await Task.Run(() => generalClass.Send_mail(generalModels, null, contactViewModels, Request.Url.AbsoluteUri)))
                        return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                generalModels.ex = e.Message;
                generalClass.Error_method(generalModels, Request.Url.AbsoluteUri);
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }
    }

}