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
    public partial class Form5 : Form
    {
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
        }

        private void initializeDataGridAmigos()
        {
            int n = this.dataGridAmigosActuales.Rows.Add();
            List<Usuario> amigos = this.redSocial.logedUser.Amigos;
            for (int x = 0; x < this.redSocial.logedUser.Amigos.Count; x++)
            {
                this.dataGridAmigosActuales.Rows[n].Cells[0].Value = amigos[x].Nombre + " " + amigos[x].Apellido;
                this.dataGridAmigosActuales.Rows[n].Cells[1].Value = "Eliminar";
            }
        }
        private void initializeDataGridNoAmigos()
        {
            int n = this.dataGridNoAmigos.Rows.Add();
            List<Usuario> noAmigos = this.redSocial.usuarioNoAmigos;
            for (int x = 0; x < this.redSocial.logedUser.Amigos.Count; x++)
            {
                this.dataGridNoAmigos.Rows[n].Cells[0].Value = noAmigos[x].Nombre + " " + noAmigos[x].Apellido;
                this.dataGridNoAmigos.Rows[n].Cells[1].Value = "Agregar";
            }
        }
    }
}
