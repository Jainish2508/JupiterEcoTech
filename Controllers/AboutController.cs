using JupiterEcoTech.Classes;
using JupiterEcoTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JupiterEcoTech.Controllers
{
    public class AboutController : Controller
    {
        private static GeneralModels generalModels = new GeneralModels();
        private static GeneralClass generalClass = new GeneralClass();
        // GET: About
        public ActionResult Index()
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                var randomproducts = generalClass.Get_Random_Products();
                if (!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code))
                {
                    return View();
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (generalModels.country_code != null)
                        return View();
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
    }
}