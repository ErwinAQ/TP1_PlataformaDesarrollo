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
        public List<Usuario> Usuarios { get; set; }

        public List<Post> Post { get; set; }

        public List<Comentario> Comentario { get; set; }
        public List<Reaccion> Reaccion { get; set; }
        public List<Tag> Tag { get; set; }

        public List<Usuario> usuarioNoAmigos { get; set; }

        public Usuario logedUser;
        private DAL DB;

        public RedSocial()
        {
            Usuarios = new List<Usuario>();
            Post = new List<Post>();
            Comentario = new List<Comentario>();
            Reaccion = new List<Reaccion>();
            Tag = new List<Tag>();



            DB = new DAL();
            inicializarAtributos();
        }

        public void inicializarAtributos()
        {
            Usuarios = DB.inicializarUsuarios();
            Post = DB.inicializarPost();
        }

        public bool agregarUsuario(string Nombre, string Apellido, string Dni, string Email, string Password, bool EsADM, int IntentosFallidos, bool Bloqueado)
        {
            //comprobación para que no me agreguen usuarios con DNI duplicado
            bool esValido = true;
            foreach (Usuario u in Usuarios)
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
                    Usuarios.Add(nuevo);
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
        /*public bool agregarPost(Usuario Usuario, string Contenido)
        {
            //comprobación para que no me agreguen post con id duplicado
            bool esValido = true;
            foreach (Post u in Post)
            {
                if (u.Contenido == Contenido)
                    esValido = false;
            }
            if (esValido)
            {
                int idNuevoPost;
                idNuevoPost = DB.agregarPost(Usuario, Contenido);
                if (idNuevoPost != -1)
                {
                    //Ahora sí lo agrego en la lista
                    Post nuevo = new Post(idNuevoPost, Usuario, Contenido);
                    Post.Add(nuevo);
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
        }*/

        public bool IniciarSesion(string Dni, string Password)
        {
            int userId;
            userId = DB.iniciarSesion(Dni, Password);
            if (userId != -1)
            {
                this.logedUser = DB.getUserFromDatabase(userId);
                if (this.logedUser.EsADM)
                {
                    this.Usuarios = DB.inicializarUsuarios();
                    this.Post = DB.inicializarPost();
                }
                else
                {
                    this.logedUser.Amigos = DB.obtenerAmigos(userId);
                    this.usuarioNoAmigos = DB.inicializarUsuariosNoAmigos(userId);
                    this.Post = DB.obtenerPostAmigos(userId);
                    Console.Out.WriteLine(this.Post[0].Contenido);
                }
            }

            bool resultLogin = userId != -1;
            return resultLogin;
        }
        public bool IdUsuarioLogueado(int _codigoRetorno)
        {
            bool result = _codigoRetorno != 1;
            return result;
        }

        public bool EliminarUsuario(int idUser)
        {
            bool resultEliminarRelAmigos = DB.eliminarRelacionesAmigos(idUser);
            bool resultEliminar = DB.eliminarUsuario(idUser);
            if (resultEliminar && resultEliminarRelAmigos)
            {
                return true;
            }
            else
            {
                return false;
            }
            //Elimina los comentarios, reacciones y posts del usuario. Luego elimina el
            //usuario UsuarioActual(ver en método debajo).
            //logedUser.MisComentarios.Remove;
            //logedUser.MisPost.Remove;
            //logedUser.MisReacciones.Remove;


        }


        public bool modificarUsuario(int Id, string Nombre, string Apellido, string Dni, string Email, bool EsADM, int IntentosFallido, bool Bloqueado)
        {
            //primero me aseguro que lo pueda agregar a la base
            if (DB.modificarUsuario(Id, Nombre, Apellido, Dni, Email, EsADM, IntentosFallido, Bloqueado) == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    for (int i = 0; i < Usuarios.Count; i++)
                        if (Usuarios[i].Id == Id)
                        {
                            Usuarios[i].Nombre = Nombre;
                            Usuarios[i].Apellido = Apellido;
                            Usuarios[i].Dni = Dni;
                            Usuarios[i].Email = Email;
                            Usuarios[i].EsADM = EsADM;
                            Usuarios[i].IntentosFallidos = IntentosFallido;
                            Usuarios[i].Bloqueado = Bloqueado;
                        }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                //algo salió mal con la query porque no generó 1 registro
                return false;
            }
        }
        
        public void CerrarSesion()
        {
            //Cerrar la sesion actual.
            logedUser = null;
        }
        public bool AgregarAmigo(int nuevoAmigoId)
        {
            int index = this.usuarioNoAmigos.FindIndex(a => a.Id == nuevoAmigoId);
            bool resultAgregarAmigo = DB.agregarAmigo(this.logedUser.Id, nuevoAmigoId);
            if (resultAgregarAmigo)
            {
                this.logedUser.Amigos.Add(this.usuarioNoAmigos[index]);
                this.usuarioNoAmigos.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool QuitarAmigo(int exAmigoId)
        {
            int index = this.logedUser.Amigos.FindIndex(a => a.Id == exAmigoId);
            bool resultEliminarmeDeMiAmigo ;
            bool resultEliminarAmigo = DB.eliminarAmigo(this.logedUser.Id, exAmigoId);
            if (resultEliminarAmigo)
            {
                resultEliminarmeDeMiAmigo = DB.eliminarAmigo(exAmigoId, this.logedUser.Id);
                if (resultEliminarmeDeMiAmigo)
                {
                    this.usuarioNoAmigos.Add(this.logedUser.Amigos[index]);
                    this.logedUser.Amigos.RemoveAt(index);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void Postear(in Post Postt)
        {
            //in List<Tag>Tags
            /*Agrega el Post p a la lista de posts, agrega el post a la lista del
            usuario UsuarioActual. Revisa los tags, si no están en la lista de tags los agrega, luego para cada
            tag agrega el post p a su lista de posts y agrega los tags en t a la lista de tags del post p.*/
            DB.agregarPost(Postt);
            //logedUser.MisPost.Add(Postt);
        }
        public void ModificarPost(in Post Post)
        {
            if (Post is null)
            {
                throw new ArgumentNullException(nameof(Post));
            }
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
            //this.logedUser.Amigos = DB.obtenerPostAmigos(userId);
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