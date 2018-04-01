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

                SqlCommand cmd = new SqlCommand("Exec Obtener_Cliente '" + id + "'",con);

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
    }
}
    