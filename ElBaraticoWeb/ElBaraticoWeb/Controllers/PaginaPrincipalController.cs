using ElBaraticoWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Progra1Reque.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        List<Producto> productos = new List<Producto>();
        // GET: PaginaPrincipalCliente
        public ActionResult PrincipalCliente()
        {
            if (Session["cliente"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ManejoDatosBDModels MD = new ManejoDatosBDModels();

            productos = MD.ObtenerProductos();

            return MostrarTodosProductos();            
        }

        public ActionResult MostrarTodosProductos()
        {
            var viewModel = new PaginaPrincipalClienteModel
            {
                listaProductos = productos
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BuscarProducto(String busquedaProducto)
        {
            if (string.IsNullOrEmpty(busquedaProducto))
            {
                return MostrarTodosProductos();
            }
            else
            {
                List<Producto> mostrarProdu = new List<Producto>();
                foreach (Producto produ in productos)
                {
                    if (produ.nombre.Like(busquedaProducto + "%"))
                    {
                        mostrarProdu.Add(produ);
                    }
                }

                var viewModel = new PaginaPrincipalClienteModel
                {
                    listaProductos = mostrarProdu,
                };

                return View("PrincipalCliente", viewModel);
            }
        }

        // GET: PaginaPrincipalAdministrador
        public ActionResult PrincipalAdministrador()
        {
            if (Session["admin"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }        
    }

    public static class MyStringExtensions
    {
        public static bool Like(this string toSearch, string toFind)
        {
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }
    }
}