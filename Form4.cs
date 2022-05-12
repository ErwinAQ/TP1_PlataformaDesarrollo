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
    public partial class Form4 : Form
    {
        public delegate bool registroClick(string Nombre, string Apellido, string Mail, string Password, int DNI);
        public registroClick registroClickeado;

        public delegate void initializeForm();
        public initializeForm volverAtras;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string Nombre = textBox1.Text;
            string Apellido = textBox2.Text;
            string Mail = textBox3.Text;
            string Contraseña = textBox4.Text;
            int DNI;
            int.TryParse(textBox5.Text, out DNI);

            if (DNI == 0)
            {
                MessageBox.Show("El DNI debe ser un número");
            }
            else if (Mail == "")
            {
                MessageBox.Show("El mail no puede estar vacio");
            }
            else if (Contraseña.Length < 3 || Contraseña.Length > 12)
            {
                MessageBox.Show("La contraseña debe tener entre 3 y 12 caracteres");
            }
            else if (Nombre == "")
            {
                MessageBox.Show("El nombre no puede estar vacio");
            }
            else if (Apellido == "")
            {
                MessageBox.Show("El apellido no puede estar vacio");
            }
            else
            {
                if (this.registroClickeado(Nombre, Apellido, Mail, Contraseña, DNI))
                {
                    MessageBox.Show("El registro fue exitoso");
                    this.Close();
                    this.volverAtras();
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAtras();
        }
    }
}
