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

        public delegate void initializeForm();
        public initializeForm volverAtras;

        private RedSocial redSocial;

        public Form4(RedSocial redSocial)
        {
            InitializeComponent();
            this.redSocial = redSocial;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Nombre = textBox1.Text;
            string Apellido = textBox2.Text;
            string Dni = textBox3.Text;
            string Email = textBox4.Text;
            string Password = textBox5.Text;
            Boolean EsADM = false;
            int IntentosFallidos = 0;
            Boolean Bloqueado = false;

            if (this.redSocial.agregarUsuario(Nombre, Apellido, Dni, Email, Password, EsADM, IntentosFallidos, Bloqueado))
            {
                MessageBox.Show("El registro fue exitoso");
                this.Close();
                this.volverAtras();
            }
            else
            
                MessageBox.Show("No se pudo agregar el usuario");
                this.volverAtras();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAtras();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
