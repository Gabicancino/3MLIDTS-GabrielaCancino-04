using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;//libreria para lectura de archivos de TXT
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3MLIDTS_GabrielaCancino_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            string apellidos = tbApellido.Text;
            string Edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string telefono = tbTelefono.Text;
            string genero = "";
            if (rbMasculino.Checked) {
                genero = "Masculino";
            }
            else if (rbFemenino.Checked) {
                genero = "Femenino";
                
            }
            string datos = $"Nombres:{nombre} \r\n" +
               $"Apellidos: {apellidos}\r\n" +
               $"Edad:{Edad}\r\n Estatura: {estatura}\r\n" +
               $"Telefono:{telefono}\r\n" +
               $"Genero:{genero}\r\n";
            string rutaArchivo = "C:\\Users\\User\\Documents\\3MLIDTS-GabrielaCancino-04.txt";
            bool archivoExiste = File.Exists(rutaArchivo);
            using (StreamWriter escritor = new StreamWriter(rutaArchivo, true)) { 
            if (archivoExiste){
                    escritor.WriteLine();
                }
            escritor .WriteLine(datos);
            }
            MessageBox.Show("Datos Guardados Correctamente: \r\n" + datos,"informacion - Actividad 04" , MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tbNombre.Clear();
            tbApellido.Clear();
            tbEstatura.Clear();
            tbTelefono.Clear();
            tbEdad.Clear();
            rbFemenino.Checked = false;
            rbMasculino.Checked = false;
        }
    }
}
