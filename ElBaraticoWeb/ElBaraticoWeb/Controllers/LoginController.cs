using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElBaraticoWeb.Models;

namespace Progra1Reque.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IngresarSistema(String logusuario, String logcontraseña)
        {
            Session.Clear();
            LogInModel datosUsuario = new LogInModel();
            ManejoDatosBDModels MD = new ManejoDatosBDModels();

            datosUsuario = MD.ObtenerDatosUsuario(logusuario);

            if (datosUsuario.usuario != logusuario.Trim())
            {
                ModelState.AddModelError("errorIngreso", "El usuario no existe!");
                return View("Index");
            }
            else
            {
                if (datosUsuario.clave == logcontraseña.Trim())
                {
                    if(datosUsuario.tipo==1){
                        AdministradorModel admin = new AdministradorModel();
                        Session["admin"]  = MD.ObtenerDatosAdmin(datosUsuario.idDuenno);
                        return RedirectToAction("PrincipalAdministrador","PaginaPrincipal");    
                    }else{
                        ClienteModel cliente = MD.ObtenerDatosCliente(datosUsuario.idDuenno);
                        Session["cliente"] = cliente;
                        return RedirectToAction("PrincipalCliente","PaginaPrincipal");       
                    }                                    
                }
                else
                {
                    ModelState.AddModelError("errorIngreso", "La clave es incorrecta!");
                    return View("Index");
                }
            }

        }

        public ActionResult SalirSistema()
        {
            Session.Clear();
            return RedirectToAction("Index","Login");
        }

    }
}