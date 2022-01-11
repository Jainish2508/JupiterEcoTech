using JupiterEcoTech.Classes;
using JupiterEcoTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JupiterEcoTech.Controllers
{
    [HandleError]
    public class ProductsController : Controller
    {
        private static GeneralModels generalModels = new GeneralModels();
        private static GeneralClass generalClass = new GeneralClass();
        // GET: Products
        public ActionResult Index(int? page)
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                var Products = generalClass.All_products_from_db(page);
                if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && Products != null)
                {
                    return View(Products);
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (Products != null && !string.IsNullOrEmpty(generalModels.country_code))
                        return View(Products);
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

        public ActionResult Laptop(int? page)
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                var Products = generalClass.All_products_from_db(page,"laptop");
                if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && Products != null)
                {
                    return View(Products);
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (Products != null && !string.IsNullOrEmpty(generalModels.country_code))
                        return View(Products);
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

        public ActionResult Desktop(int? page)
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                var Products = generalClass.All_products_from_db(page, "desktop");
                if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && Products != null)
                {
                    return View(Products);
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (Products != null && !string.IsNullOrEmpty(generalModels.country_code))
                        return View(Products);
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

        public ActionResult AllInOne(int? page)
        {
            try
            {
                generalModels.ip = Request.GetIPAddress();
                generalModels.country_code = Request.GetCountryName();
                var Products = generalClass.All_products_from_db(page, "allinone");
                if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && Products != null)
                {
                    return View(Products);
                }
                else
                {
                    generalModels.country_code = generalClass.IP_From_DB(generalModels);
                    if (Products != null && !string.IsNullOrEmpty(generalModels.country_code))
                        return View(Products);
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

        public ActionResult Details(string id)
        {
            try
            {
                if (Request.UrlReferrer == null || Request.UrlReferrer.ToString().ToLower().EndsWith("/details") || Request.UrlReferrer.ToString().ToLower().EndsWith("/details/"))
                    return RedirectToActionPermanent("index", "products");
                if(!string.IsNullOrEmpty(id) && Request.UrlReferrer == null)
                {
                    generalModels.ip = Request.GetIPAddress();
                    generalModels.country_code = Request.GetCountryName();
                    List<ProductDetails> product_details = generalClass.Product_Details(id).ToList();
                    if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && product_details != null)
                    {
                        return View(product_details);
                    }
                    else
                    {
                        generalModels.country_code = generalClass.IP_From_DB(generalModels);
                        if (product_details != null && !string.IsNullOrEmpty(generalModels.country_code))
                            return View(product_details);
                    }
                }
                else
                {
                    List<ProductDetails> product_details = generalClass.Product_Details(id).ToList();
                    if ((!string.IsNullOrEmpty(generalModels.ip) && !string.IsNullOrEmpty(generalModels.country_code)) && product_details != null)
                    {
                        return View(product_details);
                    }
                    else
                    {
                        generalModels.country_code = generalClass.IP_From_DB(generalModels);
                        if (product_details != null && !string.IsNullOrEmpty(generalModels.country_code))
                            return View(product_details);
                    }
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