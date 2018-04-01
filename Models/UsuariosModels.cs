using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ElBaraticoWeb.Models
{    public class LogInModel
    {
        public String usuario { get; set; }
        public String clave { get; set; }
        public int tipo { get; set; }
        public int idDuenno { get; set; }
    }

    public class AdministradorModel
    {
        List<AdministradorModel> listaAdministradores = new List<AdministradorModel>();

        public int Id { get; set; }
        public String nombre { get; set; }
        public String apellidos { get; set; }
    }

    public class ClienteModel
    {
        List<ClienteModel> listaCliente = new List<ClienteModel>();
        
        public int Id { get; set; }
        public String nombre { get; set; }
        public String apellidos { get; set; }
        public String correoElectronico { get; set; }
        public String direccion { get; set; }

    }
}