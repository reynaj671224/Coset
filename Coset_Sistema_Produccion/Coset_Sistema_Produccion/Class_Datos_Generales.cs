using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Coset_Sistema_Produccion
{
    class  Class_Datos_Generales
    {

        public Datos_generales Obtener_informacion_datos_generales_base_datos()
        {
            Datos_generales datos_generales = new Datos_generales();
            MySqlConnection connection = new MySqlConnection(Configura_Cadena_Conexion_MySQL_sistema_datos_generales());
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(Commando_leer_Mysql_datos_generales(), connection);
                connection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                mySqlDataReader.Read();
                datos_generales.Nombre_empresa = mySqlDataReader["nombre"].ToString();
                datos_generales.Domicilio_empresa = mySqlDataReader["domicilio"].ToString();
                datos_generales.Telefono = mySqlDataReader["telefono"].ToString();
                datos_generales.Rfc = mySqlDataReader["rfc"].ToString();
                datos_generales.Iva= mySqlDataReader["iva"].ToString();
                datos_generales.Tc = mySqlDataReader["tc"].ToString();
                datos_generales.Clave_empresa = mySqlDataReader["clave_empresa"].ToString();
            }
            catch (Exception ex)
            {
                datos_generales.error = ex.Message;
            }
            connection.Close();
            return datos_generales;
        }

        private string Commando_leer_Mysql_datos_generales()
        {
            return "SELECT * FROM sistema.datos_generales";
        }

        private string Configura_Cadena_Conexion_MySQL_sistema_datos_generales()
        {
            return "Server=" + Coset_Sistema_Produccion.ip_addres_server + ";Database=sistema;Uid=root;Pwd=" + Coset_Sistema_Produccion.password_server + ";";
        }
        
    }
    public class Datos_generales
    {
        public string Nombre_empresa = "";
        public string Domicilio_empresa = "";
        public string Telefono = "";
        public string Rfc = "";
        public string Iva = "";
        public string Tc = "";
        public string Clave_empresa = "";
        public string error = "";
    }
}
