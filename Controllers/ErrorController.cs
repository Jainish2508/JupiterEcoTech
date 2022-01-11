using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JupiterEcoTech.Controllers
{
    [HandleError]
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            ViewBag.StatusCode = "500";
            ViewBag.Message = "Sorry! Unable to Process your Request.";
            return View("error");
        }
        public ActionResult E404()
        {
            ViewBag.StatusCode = "404";
            ViewBag.Message = "Uh oh! Page you are looking for not found.";
            return View("error");
        }
    }
}