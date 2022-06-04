﻿using System;
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
    public partial class Form5 : Form
    {
        public delegate void cerrarSesion();
        public cerrarSesion volverAlLogin;

        private RedSocial redSocial;
        public Form5(RedSocial redSocial)
        {
            this.redSocial = redSocial;
            InitializeComponent();
            this.welcomeUserLabel.Text = "Bienvenido " + this.redSocial.logedUser.Nombre + "!";
            this.initializeDataGrids();

        }

        private void initializeDataGrids()
        {
            this.initializeDataGridAmigos();
            this.initializeDataGridNoAmigos();
            this.initializeDataGridFriendsPosts();
        }

        private void initializeDataGridAmigos()
        {
            List<Usuario> amigos = this.redSocial.logedUser.Amigos;
            for (int x = 0; x < amigos.Count; x++)
            {
                int n = this.dataGridAmigosActuales.Rows.Add();
                this.dataGridAmigosActuales.Rows[n].Cells[0].Value = amigos[x].Nombre + " " + amigos[x].Apellido;
                this.dataGridAmigosActuales.Rows[n].Cells[1].Value = "Eliminar";
            }
        }
        private void initializeDataGridNoAmigos()
        {
            List<Usuario> noAmigos = this.redSocial.usuarioNoAmigos;
            for (int x = 0; x < noAmigos.Count; x++)
            {
                int n = this.dataGridNoAmigos.Rows.Add();
                this.dataGridNoAmigos.Rows[n].Cells[0].Value = noAmigos[x].Nombre + " " + noAmigos[x].Apellido;
                this.dataGridNoAmigos.Rows[n].Cells[1].Value = "Agregar";
            }
        }

        private void initializeDataGridFriendsPosts()
        {
            List<Post> posts = this.redSocial.Post;
            for (int x = 0; x < posts.Count; x++)
            {
                int n = this.dataGridPostsAmigos.Rows.Add();
                this.dataGridPostsAmigos.Rows[n].Cells[0].Value = posts[x].Usuario.Nombre + " " + posts[x].Usuario.Apellido;
                this.dataGridPostsAmigos.Rows[n].Cells[1].Value = posts[x].Contenido;
                this.dataGridPostsAmigos.Rows[n].Cells[2].Value = posts[x].Fecha;
            }
        }

        private void dataGridNoAmigos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Usuario> noAmigos = this.redSocial.usuarioNoAmigos;
            if (e.ColumnIndex == 1) // solo si selecciona la columna de agregar
            {
                if (this.redSocial.AgregarAmigo(noAmigos[e.RowIndex].Id))
                {
                    MessageBox.Show("El usuario ya es amigo suyo");
                    this.dataGridAmigosActuales.Rows.Clear();
                    this.dataGridNoAmigos.Rows.Clear();
                    this.dataGridPostsAmigos.Rows.Clear();
                    this.initializeDataGrids();
                }
                else
                {
                    MessageBox.Show("No se pudo agregar al amigo");
                }
                    
            }
        }

        private void dataGridAmigosActuales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Usuario> amigos = this.redSocial.logedUser.Amigos;
            if (e.ColumnIndex == 1) // solo si selecciona la columna de agregar
            {
                if (this.redSocial.QuitarAmigo(amigos[e.RowIndex].Id))
                {
                    MessageBox.Show("El usuario ya no es amigo suyo");
                    this.dataGridAmigosActuales.Rows.Clear();
                    this.dataGridNoAmigos.Rows.Clear();
                    this.dataGridPostsAmigos.Rows.Clear();
                    this.initializeDataGrids();
                }
                else
                {
                    MessageBox.Show("El usuario no se pudo eliminar");
                }
            }
        }
        private void cerrarSesionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAlLogin();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Post post = new Post();
            post.Contenido = this.richTextBox1.Text;
            post.Usuario = this.redSocial.logedUser;
            post.Fecha = DateTime.Now;
            
            if (this.redSocial.Postear(post))
            {
                MessageBox.Show("Se creó el post correctamente");
                this.richTextBox1.Text = null;
                //this.dataGridAmigosActuales.Rows.Clear();
                //this.dataGridNoAmigos.Rows.Clear();
                //this.initializeDataGrids();
            }
            else
            {
                MessageBox.Show("No se pudo crear el post");
            }
        }
    }
}
