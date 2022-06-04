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
    public partial class Form8 : Form
    {
        public delegate void cerrarSesion();
        public cerrarSesion volverAlLogin;

        public delegate void initializeForm();
        public initializeForm volverAtras;

        public delegate bool actualizarPost(int Id, string Contenido);
        public actualizarPost ModificarPost;

        private Post post;
        public Form8(Post post)
        {
            this.post = post;
            InitializeComponent();
            this.richTextBox1.Text = this.post.Contenido;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ModificarPost(this.post.Id, this.richTextBox1.Text))
            {
                MessageBox.Show("Modificado con éxito");
                this.Close();
                this.volverAtras();
            }
            else
                MessageBox.Show("No se pudo modificar el post");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAtras();
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAlLogin();
        }
    }
}
