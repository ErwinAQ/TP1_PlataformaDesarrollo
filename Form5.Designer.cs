namespace TP1_PlataformaDesarrollo
{
    partial class Form5
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridAmigosActuales = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EliminarAmigoColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridNoAmigos = new System.Windows.Forms.DataGridView();
            this.NoFriendNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addFriendColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.welcomeUserLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAmigosActuales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNoAmigos)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesionToolStripMenuItem1});
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.cerrarSesionToolStripMenuItem.Text = "Archivo";
            // 
            // cerrarSesionToolStripMenuItem1
            // 
            this.cerrarSesionToolStripMenuItem1.Name = "cerrarSesionToolStripMenuItem1";
            this.cerrarSesionToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.cerrarSesionToolStripMenuItem1.Text = "Cerrar sesion";
            this.cerrarSesionToolStripMenuItem1.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Amigos actuales";
            // 
            // dataGridAmigosActuales
            // 
            this.dataGridAmigosActuales.AllowUserToAddRows = false;
            this.dataGridAmigosActuales.AllowUserToDeleteRows = false;
            this.dataGridAmigosActuales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridAmigosActuales.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.EliminarAmigoColumn});
            this.dataGridAmigosActuales.Location = new System.Drawing.Point(12, 109);
            this.dataGridAmigosActuales.Name = "dataGridAmigosActuales";
            this.dataGridAmigosActuales.Size = new System.Drawing.Size(247, 116);
            this.dataGridAmigosActuales.TabIndex = 2;
            this.dataGridAmigosActuales.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridAmigosActuales_CellClick);
            // 
            // nameColumn
            // 
            this.nameColumn.HeaderText = "Nombre";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // EliminarAmigoColumn
            // 
            this.EliminarAmigoColumn.HeaderText = "";
            this.EliminarAmigoColumn.Name = "EliminarAmigoColumn";
            this.EliminarAmigoColumn.Text = "";
            this.EliminarAmigoColumn.ToolTipText = "Eliminar";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Buscar amigos";
            // 
            // dataGridNoAmigos
            // 
            this.dataGridNoAmigos.AllowUserToAddRows = false;
            this.dataGridNoAmigos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridNoAmigos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NoFriendNameColumn,
            this.addFriendColumn});
            this.dataGridNoAmigos.Location = new System.Drawing.Point(12, 244);
            this.dataGridNoAmigos.Name = "dataGridNoAmigos";
            this.dataGridNoAmigos.Size = new System.Drawing.Size(247, 194);
            this.dataGridNoAmigos.TabIndex = 4;
            this.dataGridNoAmigos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridNoAmigos_CellClick);
            // 
            // NoFriendNameColumn
            // 
            this.NoFriendNameColumn.HeaderText = "Nombre";
            this.NoFriendNameColumn.Name = "NoFriendNameColumn";
            this.NoFriendNameColumn.ReadOnly = true;
            // 
            // addFriendColumn
            // 
            this.addFriendColumn.HeaderText = "";
            this.addFriendColumn.Name = "addFriendColumn";
            this.addFriendColumn.Text = "Agregar";
            // 
            // welcomeUserLabel
            // 
            this.welcomeUserLabel.AutoSize = true;
            this.welcomeUserLabel.Location = new System.Drawing.Point(12, 40);
            this.welcomeUserLabel.Name = "welcomeUserLabel";
            this.welcomeUserLabel.Size = new System.Drawing.Size(63, 13);
            this.welcomeUserLabel.TabIndex = 5;
            this.welcomeUserLabel.Text = "Bienvenido!";
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.welcomeUserLabel);
            this.Controls.Add(this.dataGridNoAmigos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridAmigosActuales);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form5";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridAmigosActuales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNoAmigos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridAmigosActuales;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridNoAmigos;
        private System.Windows.Forms.Label welcomeUserLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoFriendNameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn addFriendColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewButtonColumn EliminarAmigoColumn;
    }
}