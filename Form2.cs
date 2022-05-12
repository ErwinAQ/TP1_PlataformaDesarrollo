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
    public partial class Form2 : Form
    {
        public delegate void opcion(int opcion);
        public opcion opcionClickeada;

        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.opcionClickeada(1);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.opcionClickeada(2);
            this.Close();
        }
    }
}
