using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TP1_PlataformaDesarrollo
{
    public class RedSocial
    {
        public List<Usuario> usuarios { get; set; }

        private int IdUsuarios;
        public Usuario logedUser;

        public RedSocial()
        {
            usuarios = new List<Usuario>();
            //  llenar lista de usuarios
            // initializeListUsers()
            IdUsuarios = 0;
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            Usuario otro = new Usuario(usuario.Id, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.Password, usuario.Dni);
            this.usuarios.Add(otro);
            IdUsuarios++;
            otro.Id = IdUsuarios;
            return true;

        }

        public int IniciarSesion(int DNI, string password)
        {

            StreamReader lectura;
            String cadena;
            bool encontrado = false;
            String[] campos = new string[4];
            int codRetorno = -1;

            /*
             -1 -> usuario no encontrado
            DNI (int) -> id usuario encontrado
             */

            char[] separador = { ';' };
            try
            {
                lectura = File.OpenText("C:/Users/" + Environment.UserName + "/AppData/Local/RedSocial/RedSocial/usuarios.txt");
                cadena = lectura.ReadLine(); // para que lea la linea (primer linea)
                while (cadena != null && encontrado == false) // si se termina la cadena o si ecuentro el dato magico
                {
                    campos = cadena.Split(separador);
                    if (Int32.Parse(campos[4].Trim()).Equals(DNI))
                    {
                        if (campos[3].Trim().Equals(password))
                        {
                            codRetorno = Int32.Parse(campos[4]); // encontro a usuario
                            encontrado = true;
                        }
                    }
                    else
                    {
                        cadena = lectura.ReadLine(); // para que lea la proxima linea
                    }

                }
                lectura.Close();
            }
            catch (FileNotFoundException) { Console.WriteLine("Error"); }
            catch (Exception eex) { Console.WriteLine("Error" + eex.Message); }


            return codRetorno;
        }

       /* public Usuario getUserByDNI(int dni)
        {
            int indexUser = this.usuarios.
            return;
        }*/

        public bool IdUsuarioLogueado(int _codigoRetorno)
        {
            if (_codigoRetorno == 1) { return true; }
            else { return false; }
        }
    }
}
