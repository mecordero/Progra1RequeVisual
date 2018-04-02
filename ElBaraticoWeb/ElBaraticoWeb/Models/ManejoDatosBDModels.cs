using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ElBaraticoWeb.Models
{
    public class ManejoDatosBDModels
    {
        static SqlConnection con = new SqlConnection();

        public LogInModel ObtenerDatosUsuario(String logusuario)
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            var logIn = new LogInModel();

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Verificacion_LogIn '" + logusuario + "'", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    logIn.usuario = rd.GetSqlValue(0).ToString().Trim();
                    logIn.clave = rd.GetSqlValue(1).ToString().Trim();
                    logIn.tipo = rd.GetInt32(2);
                    logIn.idDuenno = rd.GetInt32(3);

                }

            }
            return logIn;
        }

        public ClienteModel ObtenerDatosCliente(int id)
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            var cliente = new ClienteModel();

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Obtener_Cliente '" + id + "'", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    cliente.nombre = rd.GetSqlValue(0).ToString().Trim();
                    cliente.apellidos = rd.GetSqlValue(1).ToString().Trim();
                    cliente.correoElectronico = rd.GetString(2).ToString();
                    cliente.direccion = rd.GetString(3).ToString();

                }

            }
            return cliente;
        }

        public AdministradorModel ObtenerDatosAdmin(int id)
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            var admin = new AdministradorModel();

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Obtener_Admin '" + id + "'", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    admin.nombre = rd.GetSqlValue(0).ToString().Trim();
                    admin.apellidos = rd.GetSqlValue(1).ToString().Trim();
                }

            }
            return admin;
        }

        public String BuscarUsuario(String logusuario)
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            String usuario = null;

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Verificacion_LogIn '" + logusuario + "'", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    usuario = rd.GetSqlValue(0).ToString().Trim();
                }

            }
            return usuario;
        }

        public String RegistrarUsuario(String regNombre, String regApellidos, String regCorreo, String regUsuario, String regContraseña)
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            String resultado = null;

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Registrar_Cliente '" + regNombre + "', '" + regApellidos + "', '" + regCorreo + "', '" + regUsuario + "', '" + regContraseña + "'", con);
                try
                {
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        resultado = rd.GetSqlValue(0).ToString().Trim();
                    }
                }
                catch
                {
                    return "Error al insertar el cliente en la BD";
                }
            }
            return resultado;
        }

        public List<Producto> ObtenerProductos()
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            
            List<Producto> productos = new List<Producto>();
            List<Categoria> categorias = ObtenerCategorias();

            con.ConnectionString = mySetting.ConnectionString;

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Obtener_Productos", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Producto producto = new Producto();
                    producto.id = rd.GetInt32(0);
                    producto.nombre = rd.GetSqlValue(1).ToString();
                    producto.categoria = ObtenerCategoriasProducto(categorias, rd.GetInt32(2));
                    producto.CantidadVendida = rd.GetInt32(3);
                    producto.Precio = rd.GetInt32(4);
                    producto.CalificacionPromedio = rd.GetInt32(5);

                    productos.Add(producto);
                }

            }
            return productos;
        }

        public Categoria ObtenerCategoriasProducto(List<Categoria> categorias, int id)
        {
            foreach (Categoria cat in categorias)
            {
                if (cat.id == id)
                    return cat;
            }
            return null;
        }

        public List<Categoria> ObtenerCategorias()
        {
            //Verifica que connectionString exista en Web.config
            ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings["WebBaraticoDBContext"];
            if (mySetting == null || string.IsNullOrEmpty(mySetting.ConnectionString))
                throw new Exception("Error Fatal: el string de conexión no existe en el archivo web.config");
            con.ConnectionString = mySetting.ConnectionString;

            List<Categoria> categorias = new List<Categoria>();

            con.Open();
            using (con)
            {

                SqlCommand cmd = new SqlCommand("Exec Obtener_Categorias", con);

                SqlDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    Categoria cat = new Categoria();
                    cat.id = rd.GetInt32(0);
                    cat.nombre = rd.GetSqlValue(1).ToString();

                    categorias.Add(cat);
                }
                return categorias;
            }
        }
    }
}