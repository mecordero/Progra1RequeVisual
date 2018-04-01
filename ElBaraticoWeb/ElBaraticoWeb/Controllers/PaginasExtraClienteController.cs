using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Progra1Reque.Controllers
{
    public class PaginasExtraClienteController : Controller
    {
        // GET: PaginasExtraCliente
        public ActionResult Carrito()
        {
            return View();
        }
        public ActionResult Facturas()
        {
            return View();
        }
    }
}