using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TP1_PlataformaDesarrollo
{
    public partial class Form3 : Form
    {
        public delegate bool ingresarClickeado(int DNI, string Password);
        public ingresarClickeado IniciarSesion;

        public delegate void logueadoI();
        public logueadoI logInicio;

        public delegate void initializeForm();
        public initializeForm volverAtras;

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAtras();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int DNI;
            int.TryParse(textBox1.Text, out DNI);
            string Contraseña = textBox2.Text;

            if (DNI == 0)
            {
                MessageBox.Show("El DNI debe ser un número");
            }
            else if (Contraseña.Length < 3 || Contraseña.Length > 12)
            {
                MessageBox.Show("La contraseña debe tener entre 3 y 12 caracteres");
            }
            else
            {
                bool logueado = this.IniciarSesion(DNI, Contraseña);

                if (logueado)
                {
                    this.Close();
                    this.logInicio();
                }
                else
                {
                    MessageBox.Show("DNI o contraseña incorrecto");
                }
            }
        }
    }
}
