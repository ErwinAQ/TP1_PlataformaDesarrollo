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
        }

        public void initializeDataGridUsuarios()
        {

            Console.Out.WriteLine("cantidad" + this.redSocial.Usuarios[4].Id);
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
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Usuario> usuarios = this.redSocial.Usuarios;
            if (e.ColumnIndex == 9) // solo si selecciona la columna de agregar
            {
                if (this.redSocial.EliminarUsuario(usuarios[e.RowIndex].Id))
                {
                    MessageBox.Show("Usuario eliminado");
                }
                else
                {
                    MessageBox.Show("El usuario no se pudo eliminar");
                }
            }

        }
    }
}
