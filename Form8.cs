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
        private RedSocial redsocial;
        public Form8(RedSocial redsocial, Post post)
        {
            this.redsocial = redsocial;
            this.post = post;
            InitializeComponent();
            this.richTextBox1.Text = this.post.Contenido;
            this.InitializeDataGridComments();
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

        private void InitializeDataGridComments()
        {

            List<Comentario> comentarios = this.post.Comentarios;
            for (int x = 0; x < comentarios.Count; x++)
            {
                int n = this.dataGridComments.Rows.Add();
                this.dataGridComments.Rows[n].Cells[0].Value = comentarios[x].Id;
                this.dataGridComments.Rows[n].Cells[1].Value = comentarios[x].Usuario.Nombre + " " + comentarios[x].Usuario.Apellido;
                this.dataGridComments.Rows[n].Cells[2].Value = comentarios[x].Contenido;
                this.dataGridComments.Rows[n].Cells[3].Value = comentarios[x].FechaComentario.ToLocalTime();
                this.dataGridComments.Rows[n].Cells[4].Value = "Eliminar";
            }
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

        private void dataGridComments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Comentario> comentarios = this.post.Comentarios;
            if (e.ColumnIndex == 4) // solo si selecciona la columna de eliminar
            {
                if (this.redsocial.QuitarComentario(this.post, comentarios[e.RowIndex].Id))
                {
                    MessageBox.Show("Comentario eliminado");
                    this.redsocial.inicializarAtributos();
                    dataGridComments.Rows.Clear();
                    InitializeDataGridComments();
                }
                else
                {
                    MessageBox.Show("El comentario no se pudo eliminar");
                }
            }
        }
    }
}
