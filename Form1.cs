using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;//libreria para lectura de archivos de TXT
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; //Libreria para evaluaciones de estructuras de texto
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3MLIDTS_GabrielaCancino_04
{
    public partial class Form1 : Form
    {
        //string SQLConection="Server=localhost; Port =3306; Database=programacion_avanzada; Uid=root; Pwd=;"0Gabs16.;
        string SQLConection = "Server =127.0.0.1; Port=3306; Database=programacion_avanzada; Uid=root; Pwd=0Gabs16.";
        public Form1()
        {
            InitializeComponent();
            //Declaracion de los manejadores de eventos
            tbEdad.TextChanged += ValidarEdad;
            tbApellido.TextChanged += ValidarApellidos;
            tbNombre.TextChanged += ValidarNombre;
            tbEstatura.TextChanged += ValidarEstatura;
            tbTelefono.Leave += ValidarTelefono;
        }

        private void insertarRegistros (string nombre, string apellidos, int edad, decimal estatura, string telefono, string genero) {

            using (MySqlConnection conectar = new MySqlConnection(SQLConection)) {
                conectar.Open();
                string insertQuery = "INSERT INTO registros (nombre, apellidos, telefono, estatura, edad, genero) " +
                                     "VALUES (@nombre, @apellidos, @telefono, @estatura, @edad, @genero)";

                using (MySqlCommand comando = new MySqlCommand(insertQuery, conectar)) {
                    comando. Parameters.AddWithValue("@nombre", nombre);
                    comando. Parameters.AddWithValue("@apellidos", apellidos);
                    comando. Parameters.AddWithValue("@telefono", telefono);
                    comando.Parameters.AddWithValue("@estatura", estatura);
                    comando.Parameters.AddWithValue("@edad", edad);
                    comando. Parameters.AddWithValue("@genero", genero);
                    comando.ExecuteNonQuery();
                }
                conectar.Close();
            }
        }

        private void ValidarNombre(object sender, EventArgs e)
        {
            TextBox cajaNombre = (TextBox)sender;
            if (!EsTextoValido(cajaNombre.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el Nombre",
                "Error Nombre", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                cajaNombre.Clear();
            }
        }
        private void ValidarApellidos(object sender, EventArgs e)
        {
            TextBox cajaApellidos = (TextBox)sender;
            if (!EsTextoValido(cajaApellidos.Text))
            {
                MessageBox.Show("Ingrese valores correctos para el apellido",
                "Error Apellido", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                cajaApellidos.Clear();
            }
        }

        private void ValidarEdad(object sender, EventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (!EsEnteroValido(textbox.Text)) {
                MessageBox.Show("Ingrese valores correctos para la edad ", "Error Edad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbEdad.Clear();
            }
        }

        private bool EsTextoValido(string valor)
        {
            return Regex.IsMatch(valor, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        }

        private bool EsEnteroValido(string valor)
        {
            int resultado;
            return int.TryParse(valor, out resultado);
        }
        private void ValidarEstatura(object sender, EventArgs e)
        {
            TextBox textBoxEstatura = (TextBox)sender;
            if (!EsFlotanteValido(textBoxEstatura.Text))
            {
                MessageBox.Show("Ingrese valores correctos para la estatura",
                "Error Estatura", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
                textBoxEstatura.Clear();
            }
        }

        private bool EsFlotanteValido(String valor)
        {
            float resultado;
            return float.TryParse(valor, out resultado );
        }

        private void ValidarTelefono(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string input = textBox.Text;
            if (input.Length > 10)
            {
                if (!EsEnteroValidoDe10Digitos(input))
                {
                    textBox.BackColor = Color.Red;
                }
            }
            else if (!EsEnteroValidoDe10Digitos(input))
            {
                textBox.BackColor = Color.Yellow;
            }
            else
            {
                textBox.BackColor = Color.SeaGreen;
            }
        }

        private bool EsEnteroValidoDe10Digitos(string valor)
        {
            long resultado;
            return long.TryParse(valor, out resultado  ) && valor.Length ==
            10;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = tbNombre.Text;
            string apellidos = tbApellido.Text;
            string Edad = tbEdad.Text;
            string estatura = tbEstatura.Text;
            string telefono = tbTelefono.Text;
            string genero = "";
            if (rbHombre.Checked) {
                genero = "Hombre";
            }
            else if (rbMujer.Checked) {
                genero = "Mujer";
                
            }

            // Validar que los campos tengan el formato correcto
            if (EsEnteroValido(Edad) && EsFlotanteValido(estatura) &&
            EsEnteroValidoDe10Digitos(telefono) &&
            EsTextoValido(nombre) && EsTextoValido(apellidos))
            {
                // Crear una cadena con los datos
                string datos = $"Nombres: {nombre}\r\nApellidos: {apellidos}\r\nTeléfono: {telefono}" +
               $"\r\nEstatura: {estatura} cm\r\nEdad: {Edad} años\r\nGénero: {genero}\r\n"; ;// Guardar los datos en un archivo de texto

                string rutaArchivo = @"C:\Users\User\Documents\3MLIDTS-GabrielaCancino-04.txt";
                bool archivoExiste = File.Exists(rutaArchivo);
                if (archivoExiste == false)
                {
                    File.WriteAllText(rutaArchivo, datos);
                }
                else
                {
                    // Verificar si el archivo ya existe
                    using (StreamWriter writer = new StreamWriter
                    (rutaArchivo, true))
                    {
                        if (archivoExiste)
                        {
                            // Si el archivo existe, añadir un separador antes del nuevo registro
                              writer.WriteLine();
                        }
                              writer.WriteLine(datos);
                        // string nombre, string apellidos, int edad,  decimal estatura, string telefono, string genero) {

                            insertarRegistros(nombre, apellidos, int.Parse
                            (Edad), decimal.Parse(estatura), telefono, genero);
                            MessageBox.Show("Datos insertados en la Base de  Datos:\n\n" + datos, "Información BD", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    // Mostrar un mensaje con los datos capturados
                    //MessageBox.Show("Datos guardados con éxito:\n\n" + datos,  "Información", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
               else
                {
                    MessageBox.Show("Por favor, ingrese datos válidos en los campos.", "Error", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                }

            }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            tbNombre.Clear();
            tbApellido.Clear();
            tbEstatura.Clear();
            tbTelefono.Clear();
            tbEdad.Clear();
            rbMujer.Checked = false;
            rbHombre.Checked = false;
        }
    }
}
