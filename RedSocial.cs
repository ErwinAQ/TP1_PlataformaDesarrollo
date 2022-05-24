using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace TP1_PlataformaDesarrollo
{
    public class RedSocial
    {
        public List<Usuario> usuarios { get; set; }
        public Usuario logedUser;
        private DAL DB;

        public RedSocial()
        {
            usuarios = new List<Usuario>();
            DB = new DAL();
            inicializarAtributos();
        }

        private void inicializarAtributos()
        {
            usuarios = DB.inicializarUsuarios();
        }

        public bool agregarUsuario(string Nombre, string Apellido, string Dni, string Email, string Password, bool EsADM, int IntentosFallidos, bool Bloqueado)
        {
            //comprobación para que no me agreguen usuarios con DNI duplicado
            bool esValido = true;
            foreach (Usuario u in usuarios)
            {
                if (u.Dni == Dni)
                    esValido = false;
            }
            if (esValido)
            {
                int idNuevoUsuario;
                idNuevoUsuario = DB.agregarUsuario(Nombre, Apellido, Dni, Email, Password, EsADM, IntentosFallidos, Bloqueado);
                if (idNuevoUsuario != -1)
                {
                    //Ahora sí lo agrego en la lista
                    Usuario nuevo = new Usuario(idNuevoUsuario, Nombre, Apellido, Dni, Email, Password, EsADM, IntentosFallidos, Bloqueado);
                    usuarios.Add(nuevo);
                    return true;
                }
                else
                {
                    //algo salió mal con la query porque no generó un id válido
                    return false;
                }
            }
            else
                return false;
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            this.usuarios.Add(usuario);
            return true;
        }

        public int IniciarSesion(int DNI, string password)
        {

            StreamReader lectura;
            String cadena;
            bool encontrado = false;
            String[] campos = new string[4];
            int codRetorno = -1;
            char[] separador = { ';' };

            /*
             -1 -> usuario no encontrado
            DNI (int) -> id usuario encontrado
             */

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
            if (_codigoRetorno != 1) { return true; }
            else { return false; }
        }

        public void EliminarUsuario()
        {
            //Eliminar el usuario de la lista de amigos
        }
        public void CerrarSesion()
        {
           //Cerrar la sesion actual.
        }
        public void QuitarAmigo(in Usuario ExAmigo)
        {
            //Quita el usuario UsuarioActual de la lista de amigos de
            //ExAmigo y a la vez quita ExAmigo de la lista de amigos del usuario UsuarioActual.
        }
        public void Postear(in Post post, in Tag tag)
        {
            /*Agrega el Post p a la lista de posts, agrega el post a la lista del
            usuario UsuarioActual. Revisa los tags, si no están en la lista de tags los agrega, luego para cada
            tag agrega el post p a su lista de posts y agrega los tags en t a la lista de tags del post p.*/
        }
        public void ModificarPost(in Post post)
        {
            //Idem modificar usuario pero con datos de post.
        }
        public void EliminarPost(in Post post)
        {
            //Elimina comentarios y reacciones asociadas a p, luego elimina el post.
        }
        public void Comentar(in Post post, in Comentario comentario)
        {
            //Alta de comentario c del UsuarioActual en el post p.
        }
        public void ModificarComentario(in Comentario comentario)
        {
            //Idem modificar usuario pero con datos de comentario.
        }
        public void QuitarComentario(in Post post, in Comentario comentario)
        {
            //Elimina el comentario c en el post p.
        }
        public void Reaccionar(in Post post, in Reaccion reaccion)
        {
            //Alta de reacción r del UsuarioActual en el post p.
        }
        public void ModificarReaccion(in Reaccion reaccion)
        {
            //Idem modificar usuario pero con datos de reacción.
        }
        public void QuitarReaccion(in Post post, in Reaccion reaccion)
        {
            //Elimina la reacción r del post p.
        }
        public void MostrarDatos()
        {
            //Muestra los datos del usuario UsuarioActual. Devuelve un usuario.
        }
        public void MostrarPost()
        {
            //Muestra los posts del usuario UsuarioActual. Devuelve una lista de posts.
        }
        public void MostrarPostAmigos()
        {
            //Muestra los posts de los amigos del usuario UsuarioActual. Devuelve
            //una lista de posts.
        }
        public void BuscarPost(in string Contenido, in DateTime FechaDesde, in DateTime FechaHasta, in Tag tags)
        {
            /*El
            usuario puede buscar posts por contenido, fecha (desde/hasta) o por los tags. El sistema revisa
            cada uno de estos inputs, si es nulo lo saltea. El filtrado se realiza sobre la lista completa de
            posts. Devuelve una lista de posts (filtrada).*/
        }
    }
}
