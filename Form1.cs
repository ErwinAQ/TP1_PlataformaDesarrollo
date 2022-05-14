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

        //Variables auxiliares
        RedSocial redSocial = new RedSocial();
        string carpetaElegida = "C:/Users/" + Environment.UserName + "/AppData/Local/RedSocial";
        int loggedInUserId;
        public Form1()
        {
            InitializeComponent();
            initializeForm2();
            Console.Out.WriteLine("Cantidad de usuarios: " + this.redSocial.usuarios.Count);
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
                    menuRegistro = new Form4();
                    menuRegistro.MdiParent = this;
                    menuRegistro.registroClickeado += registroClickeado;
                    menuRegistro.volverAtras += initializeForm2;
                    menuRegistro.Show();
                    break;
            }
        }

        private void logHandler()
        {

            MessageBox.Show("Bienvenido al red social ");

            if (redSocial.IdUsuarioLogueado(this.loggedInUserId))
            {
                InicioUserLogueado = new Form5();
                InicioUserLogueado.MdiParent = this;
                //InicioUserLogueado.opcionElegidaMenuAdministrador += opcionElegidaMenuAdministrador;
                InicioUserLogueado.Show();
            }
        }

        private bool ingresarClickeado(int DNI, string password)
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
        }

        /*
        //-  la función IniciarSesión nos devuelve el id del usuario encontrado
        //-  en la clase Red Social buscamos en toda la lista de usuarios el usuario que tenga el id
        //   que nos devolvió la función IniciarSesion
         */

        private bool registroClickeado(string Nombre, string Apellido, string Mail, string Password, int DNI)
        {
            Usuario Aux = new Usuario(Nombre, Apellido, Mail, Password, DNI);

            bool result = redSocial.AgregarUsuario(Aux);

            //bool result = redSocial.AgregarUsuario(Nombre, Apellido, Mail, Password, DNI);

            if (result)
            {

                checkIfRedSocialExists();

                //Me guarda los datos del usuario creado en la ruta indicada
                StreamWriter file = new StreamWriter(carpetaElegida + "/RedSocial/usuarios.txt", true);

                file.WriteLine(Nombre + ";" + Apellido + ";" + Mail + ";" + Password + ";" + DNI);
                file.Close();
            }

            return result;
        }

        private void checkIfRedSocialExists()
        {
            //Chequea si existe la carpeta, si no, la crea
            if (!Directory.Exists(carpetaElegida + "/RedSocial"))
            {
                System.IO.Directory.CreateDirectory(carpetaElegida + "/RedSocial");
            }
        }
    }
}
