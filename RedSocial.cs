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

        public List<Tag> Tags { get; set; }

        public List<Usuario> usuarioNoAmigos { get; set; }

        public Usuario logedUser;
        private DAL DB;

        public RedSocial()
        {
            Usuarios = new List<Usuario>();
            Post = new List<Post>();
            DB = new DAL();
            inicializarAtributos();
        }

        public void inicializarAtributos()
        {
            Usuarios = DB.inicializarUsuarios();
            Post = DB.inicializarPost();
            Tags = DB.inicializarTags();
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
                    this.logedUser.MisPost = DB.obtenerMisPosts(userId);
                    this.usuarioNoAmigos = DB.inicializarUsuariosNoAmigos(userId);
                    this.Post = DB.obtenerPostAmigos(userId);
                }
            }
            //Escrito para hacer el push , borrar despues.
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
                this.Post = this.DB.inicializarPost();
                return true;
            }
            else
            {
                return false;
            }
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
                this.Post = DB.obtenerPostAmigos(this.logedUser.Id);
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
                    this.Post = DB.obtenerPostAmigos(this.logedUser.Id);
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
        public bool Postear(in Post Postt)
        {
            //in List<Tag>Tags
            /*Agrega el Post p a la lista de posts, agrega el post a la lista del
            usuario UsuarioActual. Revisa los tags, si no están en la lista de tags los agrega, luego para cada
            tag agrega el post p a su lista de posts y agrega los tags en t a la lista de tags del post p.*/
            int idPost = DB.agregarPost(Postt);
            bool result = idPost != -1;

            if (result)
            {
                this.logedUser.MisPost = DB.obtenerMisPosts(this.logedUser.Id);
            }
            return result;
            //logedUser.MisPost.Add(Postt);
        }
        public bool Comentar(Comentario comentario)
        {
            //in List<Tag>Tags
            /*Agrega el Post p a la lista de posts, agrega el post a la lista del
            usuario UsuarioActual. Revisa los tags, si no están en la lista de tags los agrega, luego para cada
            tag agrega el post p a su lista de posts y agrega los tags en t a la lista de tags del post p.*/
            int idComentario = DB.agregarComentario(comentario);
            if(this.logedUser.Id != comentario.Post.Usuario.Id)
            {
                this.Post.Find(post => post.Id == comentario.Post.Id).Comentarios.Add(comentario);
            }
            else
            {
                this.logedUser.MisPost.Find(post => post.Id == comentario.Post.Id).Comentarios.Add(comentario);
            }
            bool result = idComentario != -1;

            return result;
            //logedUser.MisPost.Add(Postt);
        }

        public bool QuitarComentario(Post post, int idComentario)
        {
            bool resultEliminar = DB.eliminarComentario(idComentario);
            if (resultEliminar)
            {
                post.Comentarios = DB.obtenerComentariosByPost(post.Id);
                return true;
            }
            else 
            {
                return false;
            }
        }

        public List<Comentario> obtenerComentariosByPost(int postId)
        {
            List<Comentario> comentarios = this.DB.obtenerComentariosByPost(postId);
            return comentarios;
        }
        public bool ModificarPost(int Id, string Contenido)
        {
            if (DB.modificarPost(Id, Contenido) == 1)
            {
                try
                {
                    //Ahora sí lo MODIFICO en la lista
                    if (this.logedUser.EsADM)
                    {
                        this.Post = this.DB.inicializarPost();
                    }
                    else
                    {
                        this.logedUser.MisPost = this.DB.obtenerMisPosts(this.logedUser.Id);
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
        public bool eliminarPost(int idPost)
        {

            bool resultEliminar = DB.eliminarPost(idPost);
            if (!this.logedUser.EsADM)
            {
                this.logedUser.MisPost = DB.obtenerMisPosts(this.logedUser.Id);
            }
            return resultEliminar;
        }
        public void ModificarComentario(in Comentario comentario)
        {
            //Idem modificar usuario pero con datos de comentario.
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