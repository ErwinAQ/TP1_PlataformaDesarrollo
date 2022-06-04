using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP1_PlataformaDesarrollo
{
    public partial class Form1 : Form
    {

        Form2 menuInicial;
        Form3 menuLogin;
        Form4 menuRegistro;
        Form5 InicioUserLogueado;
        Form6 InicioAdminLogueado;
        Form7 UpdateUsuario;
        Form8 UpdatePost;
        Form9 ComentarPost;

        //Variables auxiliares
        RedSocial redSocial;

        public Form1()
        {
            this.redSocial = new RedSocial();
            InitializeComponent();
            initializeForm2();
        }
        private void initializeForm2()
        {
            menuInicial = new Form2();
            menuInicial.MdiParent = this;
            menuInicial.opcionClickeada += opcionClickeada;
            menuInicial.Show();
        }

        private void opcionClickeada(int opcionElegida)
        {
            switch (opcionElegida)
            {
                //* menuLogin
                case 1:
                    menuLogin = new Form3();
                    menuLogin.MdiParent = this;
                    menuLogin.IniciarSesion += ingresarClickeado;
                    menuLogin.logInicio += logHandler;
                    menuLogin.volverAtras += initializeForm2;
                    menuLogin.Show();
                    break;

                //* menuRegistro;
                case 2:
                    menuRegistro = new Form4(this.redSocial);
                    menuRegistro.MdiParent = this;
                    menuRegistro.volverAtras += initializeForm2;
                    menuRegistro.Show();
                    break;
            }
        }

        private void logHandler()
        {
            if (this.redSocial.logedUser.EsADM)
            {
                InicioAdminLogueado = new Form6(this.redSocial);
                InicioAdminLogueado.MdiParent = this;
                InicioAdminLogueado.seleccionarTabla += seleccionarTabla;
                InicioAdminLogueado.volverAlLogin += cerrarSesion;
                InicioAdminLogueado.Show();
            }
            else
            {
                InicioUserLogueado = new Form5(this.redSocial);
                InicioUserLogueado.MdiParent = this;
                InicioUserLogueado.comentarPost += comentarPost;
                InicioUserLogueado.volverAlLogin += cerrarSesion;
                InicioUserLogueado.Show();
            }
        }

        private bool ingresarClickeado(string DNI, string Password)
        {
            if (redSocial.IniciarSesion(DNI, Password))
            {
                return true;
            }
            return false;
        }

        /*
        //-  la función IniciarSesión nos devuelve el id del usuario encontrado
        //-  en la clase Red Social buscamos en toda la lista de usuarios el usuario que tenga el id
        //   que nos devolvió la función IniciarSesion
         */

        private void seleccionarTabla(int opcionElegida, int indexItemSeleccionado)
        {
            switch (opcionElegida)
            {
                case 1:
                    // = modificarUsuarioSeleccionado
                    initializeForm7(this.redSocial.Usuarios[indexItemSeleccionado]);
                    break;
                case 2:
                    initializeForm8(this.redSocial.Post[indexItemSeleccionado]);
                    break;
                case 3:
                    break;
            }
        }

        private void comentarPost(int opcionElegida, Post post) 
        {
            switch (opcionElegida) 
            {
                case 1:
                    initializeForm9(post);
                    break;
            }
        }

        private void initializeForm7(Usuario usuarioSelected)
        {
            UpdateUsuario = new Form7(usuarioSelected);
            UpdateUsuario.MdiParent = this;
            UpdateUsuario.ModificaUsuario += modificarUsuario;
            UpdateUsuario.volverAtras += logHandler;
            UpdateUsuario.volverAlLogin += cerrarSesion;
            UpdateUsuario.Show();
        }

        private void initializeForm8(Post postSelected) 
        {
            UpdatePost = new Form8(postSelected);
            UpdatePost.MdiParent = this;
            UpdatePost.ModificarPost += modificarPost;
            UpdatePost.volverAtras += logHandler;
            UpdatePost.volverAlLogin += cerrarSesion;
            UpdatePost.Show();
        }

        private void initializeForm9(Post postSelected) 
        {
            postSelected.Comentarios = this.redSocial.obtenerComentariosByPost(postSelected.Id);
            ComentarPost = new Form9(this.redSocial, postSelected);
            ComentarPost.MdiParent = this;
            ComentarPost.volverAtras += logHandler;
            ComentarPost.volverAlLogin += cerrarSesion;
            ComentarPost.Show();
        }

        private bool modificarUsuario(int Id, string Nombre, string Apellido, string Dni, string Email, bool EsADM, int IntentosFallidos, bool Bloqueado)
        {
            if (this.redSocial.modificarUsuario(Id, Nombre, Apellido, Dni, Email, EsADM, IntentosFallidos, Bloqueado))
            {
                return true;
            }
            return false;
        }

        private bool modificarPost(int Id, String Contenido) 
        {
            if (this.redSocial.ModificarPost(Id, Contenido)) 
            {
                return true;
            }
            return false;
        }

        private void cerrarSesion() 
        {
            this.redSocial.CerrarSesion();
            this.initializeForm2();
        }

    }
}