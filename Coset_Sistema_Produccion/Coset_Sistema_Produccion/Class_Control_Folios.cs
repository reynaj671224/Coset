using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Coset_Sistema_Produccion
{
    class Class_Control_Folios
    {
        public Control_folio Obtener_informacion_control_folio_base_datos()
        {
            Control_folio control_Folio = new Control_folio();
            MySqlConnection connection = new MySqlConnection(Configura_Cadena_Conexion_MySQL_sistema_datos_generales());
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(Commando_leer_Mysql_control_folios(), connection);
                connection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                mySqlDataReader.Read();
                control_Folio.Folio_clientes = mySqlDataReader["foilio_clientes"].ToString();
                control_Folio.Folio_proveedores = mySqlDataReader["folio_proveedores"].ToString();
                control_Folio.Folio_ot = mySqlDataReader["folio_ot"].ToString();
                control_Folio.Folio_cotizaciones = mySqlDataReader["folio_cotizaciones"].ToString();
                control_Folio.Folio_oc = mySqlDataReader["foilio_oc"].ToString();
                control_Folio.Folio_proyectos = mySqlDataReader["folio_proyectos"].ToString();
                control_Folio.Folio_materiales = mySqlDataReader["folio_materiales"].ToString();
                control_Folio.Folio_control = mySqlDataReader["control_folio"].ToString();
            }
            catch (Exception ex)
            {
                control_Folio.error=ex.Message;
            }
            connection.Close();
            return control_Folio;
        }

        private string Commando_leer_Mysql_control_folios()
        {
            return "SELECT * FROM sistema.control_folio";
        }
        private string Configura_Cadena_Conexion_MySQL_sistema_datos_generales()
        {
            return "Server=" + Coset_Sistema_Produccion.ip_addres_server + ";Database=sistema;Uid=root;Pwd=" + Coset_Sistema_Produccion.password_server + ";";
        }
    }
    public class Control_folio
    {
        public string Folio_clientes = "";
        public string Folio_proveedores = "";
        public string Folio_ot = "";
        public string Folio_cotizaciones = "";
        public string Folio_oc = "";
        public string Folio_proyectos = "";
        public string Folio_materiales = "";
        public string Folio_control = "";
        public string error = "";
    }
}
