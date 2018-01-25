using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Coset_Sistema_Produccion
{
    public partial class Forma_Usuarios : Form
    {
        public static List<Usuario> usuarios_disponibles_empleados = new List<Usuario>();
        public Class_Usuarios clase_usuarios = new Class_Usuarios();
        public Usuario Usuario_Modificaciones = new Usuario();
        public string Operacio_usuarios = "";
        public Forma_Usuarios()
        {
            InitializeComponent();
        }

        private void ButtonHome_Click(object sender, EventArgs e)
        {
            Regresar_forma_principal();
        }

        private void Regresar_forma_principal()
        {
            this.Close();
        }

        private void buttonAgregarUsuario_Click(object sender, EventArgs e)
        {
            Agrega_usuario();
        }

        private void Agrega_usuario()
        {
            Desactiva_botones_operacion();
            Aparece_caja_codigo_empleado();
            Activa_cajas_informacion();
            Inicia_timer_para_asegurar_informacion_en_todos_los_campos();
            Activa_boton_cancelar_operacio();
            Operacio_usuarios = "Agregar";
        }

        private void Aparece_caja_codigo_empleado()
        {
            textBoxCodigoempleado.Visible = true;
        }

        private void Inicia_timer_para_asegurar_informacion_en_todos_los_campos()
        {
            timerAgregarUsuario.Enabled = true;
        }

        private void Activa_boton_guardar_base_de_datos()
        {
            buttonGuardarBasedeDatos.Visible = true;
        }

        private void Activa_caja_tipo_empleado()
        {
            comboBoxTipoempleado.Enabled = true;
        }

        private void Activa_caja_costo_hora_empleado()
        {
            textBoxCostohora.Enabled = true;
        }

        private void Activa_caja_costo_semana_empleado()
        {
            textBoxCostosemana.Enabled = true;
        }

        private void Activa_caja_puesto_empleado()
        {
            textBoxPuestoempleado.Enabled = true;
        }

        private void Activa_caja_fecha_ingreso_empleado()
        {
            dateTimePickerEmpleado.Enabled = true;
        }

        private void Activa_caja_nombre_empleado()
        {
            textBoxNOmbreempleado.Enabled = true;
        }

        private void Activa_caja_codigo_empleado()
        {
            textBoxCodigoempleado.Enabled = true;
        }

        private void Desactiva_botones_operacion()
        {
            Desactiva_boton_agregar_usuarios();
            Desactiva_boton_modificar_usuarios();
            Desactiva_boton_eliminar_usuarios();
            Desactiva_boton_visualizar_usuario();
        }

        private void Desactiva_boton_visualizar_usuario()
        {
            buttonBuscarempleado.Enabled = false;
        }

        private void Activa_cajas_informacion()
        {
            Activa_caja_codigo_empleado();
            Activa_caja_nombre_empleado();
            Activa_caja_fecha_ingreso_empleado();
            Activa_caja_puesto_empleado();
            Activa_caja_costo_semana_empleado();
            Activa_caja_costo_hora_empleado();
            Activa_caja_tipo_empleado();
            Activa_caja_nombre_usuario();
            ASctiva_caja_clave_usuario();
        }

        private void ASctiva_caja_clave_usuario()
        {
            textBoxClaveusuario.Enabled = true;
        }

        private void Activa_caja_nombre_usuario()
        {
            textBoxNombreususario.Enabled = true;
        }

        private void Desactiva_boton_eliminar_usuarios()
        {
            buttonEliminarUsuario.Enabled = false;
        }

        private void Desactiva_boton_modificar_usuarios()
        {
            buttonModificarUsuario.Enabled = false;
        }

        private void Desactiva_boton_agregar_usuarios()
        {
            buttonAgregarUsuario.Enabled = false;
        }


        private void TimerAgregarUsuario_Tick(object sender, EventArgs e)
        {
            if(textBoxCodigoempleado.Text!="" && textBoxNOmbreempleado.Text !="" &&
                dateTimePickerEmpleado.Text !="" && textBoxPuestoempleado.Text !="" &&
                textBoxCostosemana.Text!="" && textBoxCostohora.Text !="" &&
                comboBoxTipoempleado.Text!="" && textBoxNombreususario.Text!="" &&
                textBoxClaveusuario.Text!="")
            {
                timerAgregarUsuario.Enabled = false;
                Activa_boton_guardar_base_de_datos();
            }
        }

        private void Activa_boton_cancelar_operacio()
        {
            buttonCancelar.Visible = true;
        }

        private void buttonGuardarBasedeDatos_Click(object sender, EventArgs e)
        {
            if (Costo_por_semana_textbox_es_numero() && Costo_por_hora_textbox_es_numero())
            {
                guarda_informacion_en_base_de_datos();
                Limpia_cajas_captura_despues_de_agregar_empleado();
                Limpia_combo_codigo_empleadlo();
                Desactiva_cajas_captura_despues_de_agregar_empleado();
                Desactiva_boton_guardar_base_de_datos();
                Desactiva_boton_cancelar();
                Desaparece_combo_codigo_empleado();
                Activa_botones_operacion();
                Activa_Combo_codigo_empleado();
                Elimina_informacion_usuarios_disponibles();
            }
        }

        private bool Costo_por_hora_textbox_es_numero()
        {
            try
            {
                Convert.ToSingle(textBoxCostohora.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Costo Por Semana no es valor esperado", "Costo Por Hora Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCostohora.Text = "";
                buttonGuardarBasedeDatos.Visible = false;
                Inicia_timer_para_asegurar_informacion_en_todos_los_campos();
                return false;
            }
        }

        private bool Costo_por_semana_textbox_es_numero()
        {
            try
            {
                Convert.ToSingle(textBoxCostosemana.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Costo Por Semana no es valor esperado", "Costo Por Hora Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxCostosemana.Text = "";
                buttonGuardarBasedeDatos.Visible = false;
                Inicia_timer_para_asegurar_informacion_en_todos_los_campos();
                return false;
            }
        }

        private void Elimina_informacion_usuarios_disponibles()
        {
            usuarios_disponibles_empleados = null;
        }

        private void Limpia_combo_codigo_empleadlo()
        {
            comboBoxCodigoempleado.Items.Clear();
            comboBoxCodigoempleado.Text = "";
        }

        private void Activa_Combo_codigo_empleado()
        {
            comboBoxCodigoempleado.Enabled = true;
        }

        private void Desaparece_combo_codigo_empleado()
        {
            comboBoxCodigoempleado.Visible = false;
        }

        private void Desactiva_boton_cancelar()
        {
            buttonCancelar.Visible = false;
        }

        private void Activa_botones_operacion()
        {
            Activa_boton_agregar_usuarios();
            Activa_boton_modificar_usuarios();
            Activa_boton_eliminar_usuarios();
            Activa_boton_visualizar_usuarios();
        }

        private void Activa_boton_visualizar_usuarios()
        {
            buttonBuscarempleado.Enabled = true;
        }

        private void Activa_boton_eliminar_usuarios()
        {
            buttonEliminarUsuario.Enabled = true;
        }

        private void Activa_boton_modificar_usuarios()
        {
            buttonModificarUsuario.Enabled = true;
        }

        private void Activa_boton_agregar_usuarios()
        {
            buttonAgregarUsuario.Enabled = true;
        }

        private void Desactiva_boton_guardar_base_de_datos()
        {
            buttonGuardarBasedeDatos.Visible = false;
        }

        private void Desactiva_cajas_captura_despues_de_agregar_empleado()
        {
            Desactiva_caja_codigo_empleado();
            Desactiva_caja_nombre_empleado();
            Desactiva_caja_fecha_ingreso_empleado();
            Desactiva_caja_puesto_empleado();
            Desactiva_caja_costo_semana_empleado();
            Desactiva_caja_costo_hora_empleado();
            Desactiva_caja_tipo_empleado();
            Desactiva_caja_nombre_usuario();
            Desactiva_caja_clave_usuario();
        }

        private void Desactiva_caja_clave_usuario()
        {
            textBoxClaveusuario.Enabled = false;
        }

        private void Desactiva_caja_nombre_usuario()
        {
            textBoxNombreususario.Enabled = false;
        }

        private void Desactiva_caja_tipo_empleado()
        {
            comboBoxTipoempleado.Enabled = false;
        }

        private void Desactiva_caja_costo_hora_empleado()
        {
            textBoxCostohora.Enabled = false;
        }

        private void Desactiva_caja_costo_semana_empleado()
        {
            textBoxCostosemana.Enabled = false;
        }

        private void Desactiva_caja_puesto_empleado()
        {
            textBoxPuestoempleado.Enabled = false;
        }

        private void Desactiva_caja_fecha_ingreso_empleado()
        {
            dateTimePickerEmpleado.Enabled = false;
        }

        private void Desactiva_caja_nombre_empleado()
        {
            textBoxNOmbreempleado.Enabled = false;
        }

        private void Desactiva_caja_codigo_empleado()
        {
            textBoxCodigoempleado.Enabled = false;
        }

        private void Limpia_cajas_captura_despues_de_agregar_empleado()
        {
            Limpia_caja_codigo_empleado();
            Limpia_caja_nombre_empleado();
            Limpia_caja_fecha_ingreso_empleado();
            Limpia_caja_puesto_empleado();
            Limpia_caja_costo_semana_empleado();
            Limpia_caja_costo_hora_empleado();
            Limpia_caja_tipo_empleado();
            Limpia_caja_nombre_usuario();
            Limpia_caja_clave_usuario();
        }

        private void Limpia_caja_tipo_empleado()
        {
            comboBoxTipoempleado.Text = "";
        }

        private void Limpia_caja_clave_usuario()
        {
            textBoxClaveusuario.Text = "";
        }

        private void Limpia_caja_nombre_usuario()
        {
            textBoxNombreususario.Text = "";
        }

        private void Limpia_caja_costo_hora_empleado()
        {
            textBoxCostohora.Text = "";
        }

        private void Limpia_caja_costo_semana_empleado()
        {
            textBoxCostosemana.Text = "";
        }

        private void Limpia_caja_puesto_empleado()
        {
            textBoxPuestoempleado.Text = "";
        }

        private void Limpia_caja_fecha_ingreso_empleado()
        {
            dateTimePickerEmpleado.Text = DateTime.Today.ToString();
        }

        private void Limpia_caja_nombre_empleado()
        {
            textBoxNOmbreempleado.Text = "";
        }

        private void Limpia_caja_codigo_empleado()
        {
            textBoxCodigoempleado.Text = "";
        }

        private void guarda_informacion_en_base_de_datos()
        {
            if (Operacio_usuarios == "Modificar")
                Guarda_datos_modificar_usuario();
            else if (Operacio_usuarios == "Agregar")
                Guarda_datos_agregar_usuario();
        }



        private void Guarda_datos_modificar_usuario()
        {
            MySqlConnection connection = new MySqlConnection(Configura_cadena_conexion_MySQL_sistema_empleados());
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(Configura_cadena_comando_modificar_en_base_de_datos(), connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        private string Configura_cadena_comando_modificar_en_base_de_datos()
        {
            return "UPDATE sistema.empleados set  nombre_empleado='" + textBoxNOmbreempleado.Text +
                "',fecha_ingreso_empleado='" + dateTimePickerEmpleado.Text +
                "',puesto='" + textBoxPuestoempleado.Text +
                "',costo_semana_empleado='" + textBoxCostosemana.Text +
                "',costo_hora_empleado='" + textBoxCostohora.Text +
                "',tipo_empleado='" + comboBoxTipoempleado.Text +
                "',nombre_usuario_empleado='" + textBoxNombreususario.Text +
                "',clave_usuario_empleado='" + textBoxClaveusuario.Text +
                "' where codigo_empleado='" + comboBoxCodigoempleado.Text + "';";
        }

        private void Guarda_datos_agregar_usuario()
        {
            MySqlConnection connection = new MySqlConnection(Configura_cadena_conexion_MySQL_sistema_empleados());
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(Configura_cadena_comando_insertar_en_base_de_datos(), connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        private string Configura_cadena_comando_insertar_en_base_de_datos()
        {
            return "INSERT INTO empleados(codigo_empleado, nombre_empleado,fecha_ingreso_empleado," +
                "puesto,costo_semana_empleado,costo_hora_empleado,tipo_empleado," +
                "nombre_usuario_empleado,clave_usuario_empleado) " +
                "VALUES('" + textBoxCodigoempleado.Text + "','" + textBoxNOmbreempleado.Text + "'," +
                "'" + dateTimePickerEmpleado.Text + "','" + textBoxPuestoempleado.Text + "'" +
                ",'" + textBoxCostosemana.Text + "','" + textBoxCostohora.Text + "'," +
                "'" + comboBoxTipoempleado.Text + "','" + textBoxNombreususario.Text + "'," +
                "'" + textBoxClaveusuario.Text + "');";
        }

        private void Elimina_datos_usuario()
        {
            MySqlConnection connection = new MySqlConnection(Configura_cadena_conexion_MySQL_sistema_empleados());
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(Configura_cadena_comando_eliminar_en_base_de_datos(), connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }
        private string Configura_cadena_comando_eliminar_en_base_de_datos()
        {
            return "DELETE from sistema.empleados where codigo_empleado='" +
               comboBoxCodigoempleado.Text + "';";
        }

        
        private string Configura_cadena_conexion_MySQL_sistema_empleados()
        {
            return "Server=" + Coset_Sistema_Produccion.ip_addres_server + ";Database=sistema;Uid=root;Pwd=" + Coset_Sistema_Produccion.password_server + ";";
        }

        private void buttonModificarUsuario_Click(object sender, EventArgs e)
        {
            Modifica_usuario();
        }

        private void Modifica_usuario()
        {
            Desactiva_botones_operacion();
            Desaparece_caja_captura_codigo_empleado();
            Aparece_combo_codigo_empleado();
            Obtener_datos_usuarios_disponibles_base_datos();
            Rellena_combo_codigo_empleado();
            Activa_boton_cancelar_operacio();
            Operacio_usuarios = "Modificar";
        }

        private void Obtener_datos_usuarios_disponibles_base_datos()
        {
            usuarios_disponibles_empleados = clase_usuarios.Adquiere_usuarios_disponibles_en_base_datos();
        }

        private void Configura_cadena_comando_actualizar_en_base_de_datos()
        {
            //throw new NotImplementedException();
        }

        private void Rellena_combo_codigo_empleado()
        {
            foreach (Usuario usuario in usuarios_disponibles_empleados)
            {
                if (usuario.error == "")
                    comboBoxCodigoempleado.Items.Add(usuario.codigo_empleado);
                else
                {
                    MessageBox.Show(usuario.error);
                    break;
                }
            }
        }

        private void Aparece_combo_codigo_empleado()
        {
            comboBoxCodigoempleado.Visible = true;
        }

        private void Desaparece_caja_captura_codigo_empleado()
        {
            textBoxCodigoempleado.Visible = false;
        }

        private void Forma_Usuarios_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxCodigoempleado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Operacio_usuarios == "Modificar")
                configura_forma_modificar();
            else if (Operacio_usuarios == "Eliminar")
                configura_forma_eliminar();
            else if (Operacio_usuarios == "Visualizar")
                configura_forma_visualizar();


        }

        private void configura_forma_visualizar()
        {
            Rellena_cajas_informacion_de_empleado();
        }

        private void configura_forma_eliminar()
        {
            Activa_cajas_informacion();
            Rellena_cajas_informacion_de_empleado();
            Desactiva_Combo_codigo_empleado();
        }

        private void configura_forma_modificar()
        {
            Activa_cajas_informacion();
            Rellena_cajas_informacion_de_empleado();
            Desactiva_Combo_codigo_empleado();
            Inicia_timer_modificar_empleado();
        }

        private void Inicia_timer_modificar_empleado()
        {
            timerActualizrempleado.Enabled = true;
        }

        private void Desactiva_Combo_codigo_empleado()
        {
            comboBoxCodigoempleado.Enabled = false;
        }

        private void Rellena_cajas_informacion_de_empleado()
        {
            Usuario_Modificaciones = usuarios_disponibles_empleados.Find(usuario => usuario.codigo_empleado.Contains(comboBoxCodigoempleado.SelectedItem.ToString()));

            textBoxNOmbreempleado.Text = Usuario_Modificaciones.nombre_empleado;
            dateTimePickerEmpleado.Text = Usuario_Modificaciones.fecha_ingreso_empleado;
            textBoxPuestoempleado.Text = Usuario_Modificaciones.puesto_empleado;
            textBoxCostosemana.Text = Usuario_Modificaciones.costo_semana_empleado;
            textBoxCostohora.Text = Usuario_Modificaciones.costo_hora_empleado;
            comboBoxTipoempleado.Text = Usuario_Modificaciones.tipo_empleado;
            textBoxNombreususario.Text = Usuario_Modificaciones.nombre_usuario;
            textBoxClaveusuario.Text = Usuario_Modificaciones.clave_usuario;

        }

        private void timerActualizrempleado_Tick(object sender, EventArgs e)
        {
            if(textBoxNOmbreempleado.Text!= Usuario_Modificaciones.nombre_empleado ||
               dateTimePickerEmpleado.Text!= Usuario_Modificaciones.fecha_ingreso_empleado||
               textBoxPuestoempleado.Text!= Usuario_Modificaciones.puesto_empleado ||
               textBoxCostosemana.Text!=Usuario_Modificaciones.costo_semana_empleado ||
               textBoxCostohora.Text!= Usuario_Modificaciones.costo_hora_empleado ||
               comboBoxTipoempleado.Text!= Usuario_Modificaciones.tipo_empleado ||
               textBoxNombreususario.Text!= Usuario_Modificaciones.nombre_usuario ||
               textBoxClaveusuario.Text!=Usuario_Modificaciones.clave_usuario)
            {
                timerActualizrempleado.Enabled = false;
                buttonGuardarBasedeDatos.Visible = true;
            }
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Limpia_cajas_captura_despues_de_agregar_empleado();
            Limpia_combo_codigo_empleadlo();
            Desactiva_cajas_captura_despues_de_agregar_empleado();
            Desactiva_boton_guardar_base_de_datos();
            Desaparece_combo_codigo_empleado();
            Desactiva_boton_cancelar();
            Desactiva_boton_eliminar_base_de_datos();
            Activa_botones_operacion();
            Activa_Combo_codigo_empleado();
            Aparece_caja_codigo_empleado();
        }

        private void buttonEliminarUsuario_Click(object sender, EventArgs e)
        {
            Eliminar_usuarios();
            
        }

        private void buttonBorrarBasedeDatos_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Esta Seguro de Eiliminar Este Usuario?", "Eliminar Usuario", MessageBoxButtons.YesNo, MessageBoxIcon.Question)== DialogResult.Yes)
                Elimina_informacion_en_base_de_datos();
            Limpia_cajas_captura_despues_de_agregar_empleado();
            Limpia_combo_codigo_empleadlo();
            Desactiva_cajas_captura_despues_de_agregar_empleado();
            Desactiva_boton_eliminar_base_de_datos();
            Desactiva_boton_cancelar();
            Desaparece_combo_codigo_empleado();
            Activa_botones_operacion();
            Activa_Combo_codigo_empleado();
            Elimina_informacion_usuarios_disponibles();
        }

        private void Desactiva_boton_eliminar_base_de_datos()
        {
            buttonBorrarBasedeDatos.Visible = false;
        }

        private void Elimina_informacion_en_base_de_datos()
        {
            Elimina_datos_usuario();
        }

        private void Eliminar_usuarios()
        {
            Desactiva_botones_operacion();
            Desaparece_caja_captura_codigo_empleado();
            Aparece_combo_codigo_empleado();
            Obtener_datos_usuarios_disponibles_base_datos();
            Rellena_combo_codigo_empleado();
            Activa_boton_cancelar_operacio();
            Inicia_timer_eliminar_usuario();
            Operacio_usuarios = "Eliminar";
        }

        private void Inicia_timer_eliminar_usuario()
        {
            timerEliminaempleado.Enabled = true;
        }

        private void Activa_boton_borrar_ususraio_base_datos()
        {
            buttonBorrarBasedeDatos.Visible = true;
        }

        private void timerEliminaempleado_Tick(object sender, EventArgs e)
        {
            if (comboBoxCodigoempleado.Text != "")
            {
                timerEliminaempleado.Enabled = false;
                Activa_boton_borrar_ususraio_base_datos();
            }
        }

        private void buttonBuscarempleado_Click(object sender, EventArgs e)
        {
            Visualiza_empleado();
        }

        private void Visualiza_empleado()
        {
            Desactiva_botones_operacion();
            Desaparece_caja_captura_codigo_empleado();
            Aparece_combo_codigo_empleado();
            Obtener_datos_usuarios_disponibles_base_datos();
            Rellena_combo_codigo_empleado();
            Activa_boton_cancelar_operacio();
            Operacio_usuarios = "Visualizar";
        }
    }
}
 