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
    public partial class Form6 : Form
    {
        public delegate void opcElegida(int opcionElegida, int indexItemSeleccionado);
        public opcElegida seleccionarTabla;
        public delegate void cerrarSesion();
        public cerrarSesion volverAlLogin;

        private const int UPDATE_USUARIO = 1;
        private const int UPDATE_POSTS = 2;

        private RedSocial redSocial;
        public Form6(RedSocial redSocial)
        {
            this.redSocial = redSocial;
            InitializeComponent();
            this.initializeDataGrids();
        }

        private void initializeDataGrids()
        {
            this.initializeDataGridUsuarios();
            this.initializeDataGridPost();
        }

        public void initializeDataGridUsuarios()
        {

            List<Usuario> usuarios = this.redSocial.Usuarios;
            for (int x = 0; x < this.redSocial.Usuarios.Count; x++)
            {
                int n = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[n].Cells[0].Value = usuarios[x].Id;
                this.dataGridView1.Rows[n].Cells[1].Value = usuarios[x].Nombre;
                this.dataGridView1.Rows[n].Cells[2].Value = usuarios[x].Apellido;
                this.dataGridView1.Rows[n].Cells[3].Value = usuarios[x].Dni;
                this.dataGridView1.Rows[n].Cells[4].Value = usuarios[x].Email;
                this.dataGridView1.Rows[n].Cells[5].Value = usuarios[x].Password;
                this.dataGridView1.Rows[n].Cells[6].Value = usuarios[x].EsADM;
                this.dataGridView1.Rows[n].Cells[7].Value = usuarios[x].IntentosFallidos;
                this.dataGridView1.Rows[n].Cells[8].Value = usuarios[x].Bloqueado;
                this.dataGridView1.Rows[n].Cells[9].Value = "Eliminar";
                this.dataGridView1.Rows[n].Cells[10].Value = "Editar";

            }
        }

        public void initializeDataGridPost()
        {
            
            List<Post> post = this.redSocial.Post;
            for (int x = 0; x < this.redSocial.Post.Count; x++)
            {
                int n = this.dataGridView2.Rows.Add();
                this.dataGridView2.Rows[n].Cells[0].Value = post[x].Id;
                this.dataGridView2.Rows[n].Cells[1].Value = post[x].Usuario.Nombre + " " + post[x].Usuario.Apellido;
                this.dataGridView2.Rows[n].Cells[2].Value = post[x].Contenido;
                this.dataGridView2.Rows[n].Cells[3].Value = post[x].Fecha;
                this.dataGridView2.Rows[n].Cells[4].Value = "Editar";
                this.dataGridView2.Rows[n].Cells[5].Value = "Eliminar";

            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Usuario> usuarios = this.redSocial.Usuarios;
            if (e.ColumnIndex == 9) // solo si selecciona la columna de eliminar
            {
                if (this.redSocial.EliminarUsuario(usuarios[e.RowIndex].Id))
                {
                    MessageBox.Show("Usuario eliminado");
                    this.redSocial.inicializarAtributos();
                    dataGridView1.Rows.Clear();
                    dataGridView2.Rows.Clear();
                    initializeDataGrids();
                }
                else
                {
                    MessageBox.Show("El usuario no se pudo eliminar");
                }
            }
            if (e.ColumnIndex == 10) 
            {
                this.seleccionarTabla(UPDATE_USUARIO, e.RowIndex);
            }
        }

        private void dataGridView2_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            List<Post> post = this.redSocial.Post;
            if (e.ColumnIndex == 5) // solo si selecciona la columna de eliminar
            {
                if (this.redSocial.eliminarPost(post[e.RowIndex].Id))
                {
                    MessageBox.Show("Post eliminado");
                    this.redSocial.inicializarAtributos();
                    dataGridView2.Rows.Clear();
                    initializeDataGrids();
                }
                else
                {
                    MessageBox.Show("El post no se pudo eliminar");
                }
            }
            if (e.ColumnIndex == 4) 
            {
                this.seleccionarTabla(UPDATE_POSTS, e.RowIndex);
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            this.seleccionarTabla(UPDATE_USUARIO, e.RowIndex);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            this.seleccionarTabla(UPDATE_POSTS, e.RowIndex);
        }

        private void cerrarSesionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            this.volverAlLogin();
        }
    }

}