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
    public partial class Form9 : Form
    {
        public delegate void cerrarSesion();
        public cerrarSesion volverAlLogin;

        public delegate void initializeForm();
        public initializeForm volverAtras;

        private RedSocial redSocial;
        private Post post;
        public Form9(RedSocial redsocial, Post post)
        {
            this.redSocial = redsocial;
            this.post = post;
            InitializeComponent();
            this.richTextBox1.Text = post.Contenido;
            this.label2.Text = this.post.Usuario.Nombre + " " + this.post.Usuario.Apellido;
            this.InitializeDataGridComments();
            this.updateCommentBtn.Visible = false;

            if (this.redSocial.logedUser.Id == post.Usuario.Id)
            {
                this.updateCommentBtn.Visible = true;
                this.richTextBox1.ReadOnly = false;
            }
        }

        private void updateCommentBtn_Click(object sender, EventArgs e)
        {
            if (this.redSocial.ModificarPost(this.post.Id, this.richTextBox1.Text))
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

        private void InitializeDataGridComments()
        {

            List<Comentario> comentarios = this.post.Comentarios;
            for (int x = 0; x < comentarios.Count; x++)
            {
                int n = this.dataGridComments.Rows.Add();
                this.dataGridComments.Rows[n].Cells[0].Value = comentarios[x].Usuario.Nombre + " " + comentarios[x].Usuario.Apellido;
                this.dataGridComments.Rows[n].Cells[1].Value = comentarios[x].Contenido;
                this.dataGridComments.Rows[n].Cells[2].Value = comentarios[x].FechaComentario.ToLocalTime();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.newComentarioRichTextBox.Text != null)
            {
                    Comentario newComentario = new Comentario();
                    newComentario.Post = this.post;
                    newComentario.FechaComentario = DateTime.Now;
                    newComentario.Usuario = this.redSocial.logedUser;
                    newComentario.Contenido = this.newComentarioRichTextBox.Text;
                    if (this.redSocial.Comentar(newComentario))
                    {
                        MessageBox.Show("Se creó el comentario correctamente");
                        this.newComentarioRichTextBox.Text = null;
                        this.dataGridComments.Rows.Clear();
                        this.InitializeDataGridComments();
                }
                    else
                    {
                        MessageBox.Show("No se pudo crear el comentario el post correctamente");
                    }
            }
            else
            {
                MessageBox.Show("Debe completar el campo de comentario");
            }
        }

        
    }
}
