﻿using System;
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
            return View();
        }
        // GET: PaginaPrincipalAdministrador
        public ActionResult PrincipalAdministrador()
        {
            return View();
        }
    }
}