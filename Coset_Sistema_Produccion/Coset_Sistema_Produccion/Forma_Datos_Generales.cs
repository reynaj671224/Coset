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
    public partial class Forma_Datos_Generales : Form
    {
        Datos_generales datos_temporales = new Datos_generales();
        Control_folio folios_temporales = new Control_folio();
        Class_Datos_Generales Class_Datos_Generales = new Class_Datos_Generales();
        Class_Control_Folios Class_Control_Folios = new Class_Control_Folios();
        public Forma_Datos_Generales()
        {
            InitializeComponent();
        }

        private void Forma_Datos_Generales_Load(object sender, EventArgs e)
        {
            datos_temporales = Class_Datos_Generales.Obtener_informacion_datos_generales_base_datos();
            Asigna_valores_cajas_variables_datos_generales();
            folios_temporales = Class_Control_Folios.Obtener_informacion_control_folio_base_datos();
            Asigna_valores_cajas_variables_control_folios();
        }

        private void Asigna_valores_cajas_variables_control_folios()
        {
            if (folios_temporales.error == "")
            {
                textBoxFolioClientes.Text = folios_temporales.Folio_clientes;
                textBoxFolioProveedores.Text = folios_temporales.Folio_proveedores;
                textBoxFolioOt.Text = folios_temporales.Folio_ot;
                textBoxFolioCotizaciones.Text = folios_temporales.Folio_cotizaciones;
                textBoxFolioOc.Text = folios_temporales.Folio_oc;
                textBoxFolioProyectos.Text = folios_temporales.Folio_proyectos;
                textBoxFolioMateriales.Text = folios_temporales.Folio_materiales;
            }
            else
                MessageBox.Show(folios_temporales.error);
        }

        private void Asigna_valores_cajas_variables_datos_generales()
        {
            if (datos_temporales.error == "")
            {
                textBoxEmpresa.Text = datos_temporales.Nombre_empresa;
                textBoxDomicilio.Text = datos_temporales.Domicilio_empresa;
                textBoxTelefono.Text = datos_temporales.Telefono;
                textBoxRfc.Text = datos_temporales.Rfc;
                textBoxIva.Text = datos_temporales.Iva;
                textBoxTc.Text = datos_temporales.Tc;
            }
            else
                MessageBox.Show(datos_temporales.error);
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonModificar_Click(object sender, EventArgs e)
        {
            if (Verifica_pestana_seleccionada_en_tab() == "Datos_generales")
                Modifica_datos_generales();
            else
                Modifica_control_folios();

        }

        private void Modifica_control_folios()
        {
            Desactiva_boton_modificar();
            Aparece_boton_cancelar_operacio();
            Asigna_valores_cajas_variable_temporal_folios();
            Activar_timer_modificaciones_Control_folios();
            Activa_cajas_campos_informacion_control_folio();
        }

        private void Activa_cajas_campos_informacion_control_folio()
        {
            textBoxFolioClientes.Enabled = true;
            textBoxFolioProveedores.Enabled = true;
            textBoxFolioOt.Enabled = true;
            textBoxFolioCotizaciones.Enabled = true;
            textBoxFolioOc.Enabled = true;
            textBoxFolioProyectos.Enabled = true;
            textBoxFolioMateriales.Enabled = true;
        }

        private void Activar_timer_modificaciones_Control_folios()
        {
            timerModificarControlFolios.Enabled = true;
        }

        private void Asigna_valores_cajas_variable_temporal_folios()
        {
            folios_temporales.Folio_clientes = textBoxFolioClientes.Text;
            folios_temporales.Folio_proveedores = textBoxFolioProveedores.Text;
            folios_temporales.Folio_ot = textBoxFolioOt.Text;
            folios_temporales.Folio_cotizaciones = textBoxFolioCotizaciones.Text;
            folios_temporales.Folio_oc = textBoxFolioOc.Text;
            folios_temporales.Folio_proyectos = textBoxFolioProyectos.Text;
            folios_temporales.Folio_materiales = textBoxFolioMateriales.Text;
        }

        private void Modifica_datos_generales()
        {
            Desactiva_boton_modificar();
            Aparece_boton_cancelar_operacio();
            Asigna_valores_cajas_variable_temporal_datos_generales();
            Activa_timer_modificar_datos_generales();
            Activa_cajas_campos_informacion();
        }


        private void Activa_cajas_campos_informacion()
        {
            textBoxEmpresa.Enabled = true;
            textBoxDomicilio.Enabled = true;
            textBoxTelefono.Enabled = true;
            textBoxRfc.Enabled = true;
            textBoxIva.Enabled = true;
            textBoxTc.Enabled = true;
        }

        private void Asigna_valores_cajas_variable_temporal_datos_generales()
        {
           
            datos_temporales.Nombre_empresa = textBoxEmpresa.Text;
            datos_temporales.Domicilio_empresa  =textBoxDomicilio.Text;
            datos_temporales.Telefono = textBoxTelefono.Text;
            datos_temporales.Rfc   = textBoxRfc.Text;
            datos_temporales.Iva = textBoxIva.Text;
            datos_temporales.Tc  = textBoxTc.Text;
        }

        private void Activa_timer_modificar_datos_generales()
        {
            timerModificarDatosGenerales.Enabled = true;
        }

        private void Aparece_boton_cancelar_operacio()
        {
            buttonCancelar.Visible = true;
        }

        private void Desactiva_boton_modificar()
        {
            buttonModificar.Enabled = false;
        }

        private string Verifica_pestana_seleccionada_en_tab()
        {
            if (tabControlDatosGenerales.SelectedIndex == 0)
                return "Datos_generales";
            else 
                return "Control_folios";
        }

        private void timerModificarDatosGenerales_Tick(object sender, EventArgs e)
        {
            if (datos_temporales.Nombre_empresa != textBoxEmpresa.Text ||
            datos_temporales.Domicilio_empresa != textBoxDomicilio.Text ||
            datos_temporales.Telefono != textBoxTelefono.Text ||
            datos_temporales.Rfc != textBoxRfc.Text ||
            datos_temporales.Iva != textBoxIva.Text ||
            datos_temporales.Tc != textBoxTc.Text)
            {
                timerModificarDatosGenerales.Enabled = false;
                Aparece_botom_guardar_base_datos();
            }
        }

        private void Aparece_botom_guardar_base_datos()
        {
            buttonGuardarBasedeDatos.Visible = true;
        }

        private void buttonGuardarBasedeDatos_Click(object sender, EventArgs e)
        {
            if (Verifica_pestana_seleccionada_en_tab() == "Datos_generales")
                Secuecia_guarda_datos_generales_base_datos();
            else
                Secuencia_guarda_control_folios_base_datos();

        }

        private void Secuencia_guarda_control_folios_base_datos()
        {
            Guarda_Control_folios_base_datos();
            Desaparece_boton_guarda_base_datos();
            Desaparece_boton_cancelar();
            Desactiva_cajas_control_folios();
            folios_temporales= Class_Control_Folios.Obtener_informacion_control_folio_base_datos();
            Asigna_valores_cajas_variables_control_folios();
            Aparece_boton_modificar();
        }

        private bool Tc_textbox_es_numero()
        {
            try
            {
                Convert.ToSingle(textBoxTc.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("TC no es valor esperado", "TC Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxTc.Text = "";
                buttonGuardarBasedeDatos.Visible = false;
                Activa_timer_modificar_datos_generales();
                return false;
            }
        }

        private bool Iva_textbox_es_numero()
        {
            try
            {
                Convert.ToSingle(textBoxIva.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Iva no es valor esperado", "Iva Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxIva.Text = "";
                buttonGuardarBasedeDatos.Visible = false;
                Activa_timer_modificar_datos_generales();
                return false;
            }
        }

        private void Desactiva_cajas_control_folios()
        {
            textBoxFolioClientes.Enabled = false;
            textBoxFolioProveedores.Enabled = false;
            textBoxFolioOt.Enabled = false;
            textBoxFolioCotizaciones.Enabled = false;
            textBoxFolioOc.Enabled = false;
            textBoxFolioProyectos.Enabled = false;
            textBoxFolioMateriales.Enabled = false;
        }

        private void Guarda_Control_folios_base_datos()
        {
            MySqlConnection connection = new MySqlConnection(Configura_Cadena_Conexion_MySQL_sistema_datos_generales());
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(Configura_cadena_comando_modificar_en_base_de_datos_control_folios(), connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();
        }

        private string Configura_Cadena_Conexion_MySQL_sistema_datos_generales()
        {
            return "Server=" + Coset_Sistema_Produccion.ip_addres_server + ";Database=sistema;Uid=root;Pwd=" + Coset_Sistema_Produccion.password_server + ";";
        }

        private string Configura_cadena_comando_modificar_en_base_de_datos_control_folios()
        {
            return "UPDATE sistema.control_folio set  foilio_clientes='" + textBoxFolioClientes.Text +
               "',folio_proveedores='" + textBoxFolioProveedores.Text +
               "',folio_ot='" + textBoxFolioOt.Text +
               "',folio_cotizaciones='" + textBoxFolioCotizaciones.Text +
               "',foilio_oc='" + textBoxFolioOc.Text +
               "',folio_proyectos='" + textBoxFolioProyectos.Text +
               "',folio_materiales='" + textBoxFolioMateriales.Text +
               "' where control_folio='" + folios_temporales.Folio_control + "';";
        }

        private void Secuecia_guarda_datos_generales_base_datos()
        {
            if (Iva_textbox_es_numero() && Tc_textbox_es_numero())
            {
                Guarda_datos_generales_base_datos();
                Desaparece_boton_guarda_base_datos();
                Desaparece_boton_cancelar();
                Desactiva_cajas_datos_generales();
                datos_temporales=Class_Datos_Generales.Obtener_informacion_datos_generales_base_datos();
                Asigna_valores_cajas_variables_datos_generales();
                Aparece_boton_modificar();
            }

        }

        private void Aparece_boton_modificar()
        {
            buttonModificar.Enabled = true;
        }

        private void Desactiva_cajas_datos_generales()
        {
            textBoxEmpresa.Enabled = false;
            textBoxDomicilio.Enabled = false;
            textBoxTelefono.Enabled = false;
            textBoxRfc.Enabled = false;
            textBoxIva.Enabled = false;
            textBoxTc.Enabled = false;
        }

        private void Desaparece_boton_cancelar()
        {
            buttonCancelar.Visible=false;
        }

        private void Desaparece_boton_guarda_base_datos()
        {
            buttonGuardarBasedeDatos.Visible = false;
        }

        private void Guarda_control_folios_base_datos()
        {
            throw new NotImplementedException();
        }

        private void Guarda_datos_generales_base_datos()
        {

            MySqlConnection connection = new MySqlConnection(Configura_Cadena_Conexion_MySQL_sistema_datos_generales());
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
            return "UPDATE sistema.datos_generales set  nombre='" + textBoxEmpresa.Text +
                "',domicilio='" + textBoxDomicilio.Text +
                "',telefono='" + textBoxTelefono.Text +
                "',rfc='" + textBoxRfc.Text +
                "',iva='" + textBoxIva.Text +
                "',tc='" + textBoxTc.Text +
                "' where clave_empresa='" + datos_temporales.Clave_empresa + "';";
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            Aparece_boton_modificar();
            Desactiva_cajas_datos_generales();
            datos_temporales= Class_Datos_Generales.Obtener_informacion_datos_generales_base_datos();
            Asigna_valores_cajas_variables_datos_generales();
            Desactiva_cajas_control_folios();
            folios_temporales=Class_Control_Folios.Obtener_informacion_control_folio_base_datos();
            Asigna_valores_cajas_variables_control_folios();
            Desaparece_boton_cancelar();
            Desaparece_boton_guarda_base_datos();
        }

        private void timerModificarControlFolios_Tick(object sender, EventArgs e)
        {
            if (folios_temporales.Folio_clientes != textBoxFolioClientes.Text ||
            folios_temporales.Folio_proveedores != textBoxFolioProveedores.Text ||
            folios_temporales.Folio_ot != textBoxFolioOt.Text ||
            folios_temporales.Folio_cotizaciones != textBoxFolioCotizaciones.Text ||
            folios_temporales.Folio_oc != textBoxFolioOc.Text ||
            folios_temporales.Folio_proyectos != textBoxFolioProyectos.Text ||
            folios_temporales.Folio_materiales != textBoxFolioMateriales.Text)
            {
                timerModificarControlFolios.Enabled = false;
                Aparece_botom_guardar_base_datos();
            }
        }
    }
    
   
}
