﻿using System;
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

        //Variables auxiliares
        RedSocial redSocial = new RedSocial();
        
        public Form1()
        {
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
                    /*menuLogin.IniciarSesion += ingresarClickeado;*/
                    menuLogin.logInicio += logHandler;
                    menuLogin.volverAtras += initializeForm2;
                    menuLogin.Show();
                    break;

                //* menuRegistro;
                case 2:
                    menuRegistro = new Form4();
                    menuRegistro.MdiParent = this;
                    /*menuRegistro.registroClickeado += registroClickeado;*/
                    menuRegistro.volverAtras += initializeForm2;
                    menuRegistro.Show();
                    break;
            }
        }

        private void logHandler()
        {

            MessageBox.Show("Bienvenido al red social ");

            /*if (redSocial.IdUsuarioLogueado(this.loggedInUserId))
            {
                InicioUserLogueado = new Form5();
                InicioUserLogueado.MdiParent = this;
                //InicioUserLogueado.opcionElegidaMenuAdministrador += opcionElegidaMenuAdministrador;
                InicioUserLogueado.Show();
            }*/
        }

        /*private bool ingresarClickeado(int DNI, string password)
        {
            int loginResult = redSocial.IniciarSesion(DNI, password);
            if (loginResult != -1)
            {
                // buscar en la lista de usuarios el usuario con el DNI que nos devuelve
                // la función
                loggedInUserId = loginResult;
                Console.Out.WriteLine("loginResult: " + loginResult);
                Console.Out.WriteLine("user: " + this.redSocial.usuarios[0].Nombre);
                return true;
            }
            return false;
        }*/

        /*
        //-  la función IniciarSesión nos devuelve el id del usuario encontrado
        //-  en la clase Red Social buscamos en toda la lista de usuarios el usuario que tenga el id
        //   que nos devolvió la función IniciarSesion
         */
    }
}
