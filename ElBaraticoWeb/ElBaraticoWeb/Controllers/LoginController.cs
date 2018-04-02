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

        [HttpPost]
        public ActionResult CrearCuenta(String regNombre, String regApellidos, String regCorreo, String regUsuario, String regContraseña)
        {
            ManejoDatosBDModels MD = new ManejoDatosBDModels();
            if (string.IsNullOrEmpty(regNombre) || string.IsNullOrEmpty(regApellidos))
            {
                ModelState.AddModelError("errorRegistro", "Debe ingresar su nombre y apellidos");
                return View("Index");
            }
            else if (string.IsNullOrEmpty(regCorreo))
            {
                ModelState.AddModelError("errorRegistro", "Debe ingresar su correo electrónico");
                return View("Index");
            }
            else if (!IsValidEmail(regCorreo))
            {
                ModelState.AddModelError("errorRegistro", "El correo ingresado no es válido");
                return View("Index");
            }
            else if (string.IsNullOrEmpty(regUsuario))
            {
                ModelState.AddModelError("errorRegistro", "Debe ingresar un usuario/nickname con el que va a ingresar a su cuenta");
                return View("Index");
            }
            else if (!string.IsNullOrEmpty(MD.BuscarUsuario(regUsuario)))
            {
                ModelState.AddModelError("errorRegistro", "El usuario ya existe");
                return View("Index");
            }
            else if (string.IsNullOrEmpty(regContraseña))
            {
                ModelState.AddModelError("errorRegistro", "Se debe ingresar una contraseña");
                return View("Index");
            }
            else if (string.IsNullOrEmpty(MD.RegistrarUsuario(regNombre, regApellidos, regCorreo, regUsuario, regContraseña)))
            {
                ModelState.AddModelError("cuentaCreada", "La cuenta ha sido creada exitosamente");
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("errorRegistro", "Ocurrio un error al crear el usuario");
                return View("Index");
            }

            ModelState.AddModelError("errorRegistro", "Se produjo un error no esperado");
            return View("Index");
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}