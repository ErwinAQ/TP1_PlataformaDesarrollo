using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1_PlataformaDesarrollo
{
    public partial class Form7 : Form
    {
        public delegate void initializeForm();
        public initializeForm volverAtras;

        public delegate bool actualizarDatos(int Id, string Nombre,string Apellido, string Dni, string Email, bool EsADM, int IntentosFallidos, bool Bloqueado);
        public actualizarDatos ModificaUsuario;

        private Usuario usuario;
        public Form7(Usuario usuario)
        {
            this.usuario = usuario;
            InitializeComponent();
            this.textBox1.Text = this.usuario.Nombre;
            this.textBox2.Text = this.usuario.Apellido;
            this.textBox3.Text = this.usuario.Dni;
            this.textBox4.Text = this.usuario.Email;
            this.checkBox1.Checked = this.usuario.EsADM;
            this.textBox5.Text = this.usuario.IntentosFallidos.ToString();
            this.checkBox2.Checked = this.usuario.Bloqueado;
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAtras();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ModificaUsuario(this.usuario.Id, this.textBox1.Text, this.textBox2.Text, this.textBox3.Text, this.textBox4.Text, this.checkBox1.Checked, int.Parse(this.textBox5.Text), this.checkBox2.Checked)) 
            {
                MessageBox.Show("Modificado con éxito");
            }else
                MessageBox.Show("No se pudo modificar el usuario");
        }
    }
}
