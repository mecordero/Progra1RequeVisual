using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Progra1Reque.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        // GET: PaginaPrincipalCliente
        public ActionResult PrincipalCliente()
        {
            if (Session["cliente"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }
        // GET: PaginaPrincipalAdministrador
        public ActionResult PrincipalAdministrador()
        {
            if (Session["cliente"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}