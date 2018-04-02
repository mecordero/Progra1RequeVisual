using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElBaraticoWeb.Models
{
    public class Producto
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public Categoria categoria { get; set; }
        public int CantidadVendida { get; set; }
        public int Precio { get; set; }
        public int CalificacionPromedio { get; set; }
    }
}