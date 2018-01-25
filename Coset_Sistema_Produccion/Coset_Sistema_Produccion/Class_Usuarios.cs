using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Coset_Sistema_Produccion
{
    public class Class_Usuarios
    {
        public List<Usuario> Adquiere_usuarios_disponibles_en_base_datos()
        {
            List<Usuario> usuarios_disponibles = new List<Usuario>();
            MySqlConnection connection = new MySqlConnection(Configura_Cadena_Conexion_MySQL_sistema_empleados());
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand(Commando_leer_Mysql(), connection);
                connection.Open();
                MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                while (mySqlDataReader.Read())
                {
                    usuarios_disponibles.Add(new Usuario()
                    {
                        nombre_usuario = mySqlDataReader["nombre_usuario_empleado"].ToString(),
                        clave_usuario = mySqlDataReader["clave_usuario_empleado"].ToString(),
                        fecha_ingreso_empleado = mySqlDataReader["fecha_ingreso_empleado"].ToString(),
                        puesto_empleado = mySqlDataReader["puesto"].ToString(),
                        costo_semana_empleado = mySqlDataReader["costo_semana_empleado"].ToString(),
                        costo_hora_empleado = mySqlDataReader["costo_hora_empleado"].ToString(),
                        tipo_empleado = mySqlDataReader["tipo_empleado"].ToString(),
                        codigo_empleado = mySqlDataReader["codigo_empleado"].ToString(),
                        nombre_empleado = mySqlDataReader["nombre_empleado"].ToString()

                    });
                }
            }
            catch (Exception ex)
            {
                usuarios_disponibles.Add(new Usuario()
                { error = ex.Message.ToString() });
            }
            connection.Close();
            return usuarios_disponibles;
        }

        private string Commando_leer_Mysql()
        {
            return "SELECT * FROM sistema.empleados";
        }

        private string Configura_Cadena_Conexion_MySQL_sistema_empleados()
        {
            return "Server="+ Coset_Sistema_Produccion.ip_addres_server+";Database=sistema;Uid=root;Pwd="+ Coset_Sistema_Produccion.password_server + ";";
        }
    }

        

    public class Usuario
    {
        public string nombre_usuario = "";
        public string clave_usuario = "";
        public string fecha_ingreso_empleado = "";
        public string puesto_empleado = "";
        public string costo_semana_empleado = "";
        public string costo_hora_empleado = "";
        public string tipo_empleado = "";
        public string codigo_empleado = "";
        public string nombre_empleado = "";
        public string error = "";
    }
}
