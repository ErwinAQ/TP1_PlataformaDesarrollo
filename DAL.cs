using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace TP1_PlataformaDesarrollo
{
    class DAL
    {
        private string connectionString;
        public DAL()
        {
            //Cargo la cadena de conexión desde el archivo de properties
            connectionString = Properties.Resources.ConnectionStr;
        }

        public List<Usuario> inicializarUsuarios()
        {
            List<Usuario> misUsuarios = new List<Usuario>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[usuarios] WHERE [es_admin] != 1;";

            
            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        
                        Usuario user = new Usuario();
                        user.Id = Convert.ToInt32(reader[0]);
                        user.Nombre = Convert.ToString(reader[1]);
                        user.Apellido = Convert.ToString(reader[2]);
                        user.Dni = Convert.ToString(reader[3]);
                        user.Email = Convert.ToString(reader[4]);
                        user.Password = Convert.ToString(reader[5]);
                        user.EsADM = Convert.ToBoolean(reader[6]);
                        user.IntentosFallidos = Convert.ToInt32(reader[7]);
                        user.Bloqueado = Convert.ToBoolean(reader[8]);
                        misUsuarios.Add(user);
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return misUsuarios;
        }



        public List<Usuario> inicializarUsuariosNoAmigos(int logedUserId)
        {
            List<Usuario> usuariosNoAmigos = new List<Usuario>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * from [dbo].[usuarios] where (id NOT IN " +
                "(SELECT amigo_id FROM [dbo].[usuarios_amigos] where [usuario_id] = @logedUserId))" +
                "AND ([id] != @logedUserId) AND ([es_admin] != 1);";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@logedUserId", SqlDbType.BigInt));
                command.Parameters["@logedUserId"].Value = logedUserId;
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Usuario user = new Usuario();
                        user.Id = Convert.ToInt32(reader[0]);
                        user.Nombre = Convert.ToString(reader[1]);
                        user.Apellido = Convert.ToString(reader[2]);
                        user.Dni = Convert.ToString(reader[3]);
                        user.Email = Convert.ToString(reader[4]);
                        user.Password = Convert.ToString(reader[5]);
                        user.EsADM = Convert.ToBoolean(reader[6]);
                        user.IntentosFallidos = Convert.ToInt32(reader[7]);
                        user.Bloqueado = Convert.ToBoolean(reader[8]);
                        usuariosNoAmigos.Add(user);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return usuariosNoAmigos;
        }

        public List<Usuario> obtenerAmigos(int logedUserId)
        {
            List<Usuario> amigos = new List<Usuario>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * from [dbo].[usuarios] where (id IN " +
                "(SELECT amigo_id FROM [dbo].[usuarios_amigos] where [usuario_id] = @logedUserId));";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@logedUserId", SqlDbType.BigInt));
                command.Parameters["@logedUserId"].Value = logedUserId;
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Usuario user = new Usuario();
                        user.Id = Convert.ToInt32(reader[0]);
                        user.Nombre = Convert.ToString(reader[1]);
                        user.Apellido = Convert.ToString(reader[2]);
                        user.Dni = Convert.ToString(reader[3]);
                        user.Email = Convert.ToString(reader[4]);
                        user.Password = Convert.ToString(reader[5]);
                        user.EsADM = Convert.ToBoolean(reader[6]);
                        user.IntentosFallidos = Convert.ToInt32(reader[7]);
                        user.Bloqueado = Convert.ToBoolean(reader[8]);
                        //aux = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetInt32(7), reader.GetBoolean(8));
                        amigos.Add(user);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return amigos;
        }

        public int iniciarSesion(string dni, string password)
        {
            int idUser = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "SELECT * FROM [dbo].[usuarios] WHERE [dni]=@dni AND [password]=@password;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters["@dni"].Value = dni;
                command.Parameters["@password"].Value = password;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idUser = Convert.ToInt32(reader[0]);
                    reader.Close();
                    connection.Close();
                    return idUser;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return idUser;
                }
            }
        }


        public Usuario getUserFromDatabase(int userId)
        {
            Usuario user = new Usuario();
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "SELECT * FROM [dbo].[usuarios] WHERE [id]=@id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt));
                command.Parameters["@id"].Value = userId;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    user.Id = Convert.ToInt32(reader[0]);
                    user.Nombre = Convert.ToString(reader[1]);
                    user.Apellido = Convert.ToString(reader[2]);
                    user.Dni = Convert.ToString(reader[3]);
                    user.Email = Convert.ToString(reader[4]);
                    user.Password = Convert.ToString(reader[5]);
                    user.EsADM = Convert.ToBoolean(reader[6]);
                    user.IntentosFallidos = Convert.ToInt32(reader[7]);
                    user.Bloqueado = Convert.ToBoolean(reader[8]);
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return user;
            }
        }

        public bool eliminarAmigo(int userId, int exAmigoId)
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuarios_amigos] WHERE [usuario_id]=@userId " +
                                    "AND ([amigo_id] = @exAmigoId);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.BigInt));
                command.Parameters["@userId"].Value = userId;
                command.Parameters.Add(new SqlParameter("@exAmigoId", SqlDbType.BigInt));
                command.Parameters["@exAmigoId"].Value = exAmigoId;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        public bool agregarAmigo(int userId, int nuevoAmigoId)
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[usuarios_amigos] ([usuario_id],[amigo_id]) VALUES" +
                                    "(@userId, @nuevoAmigoId), (@nuevoAmigoId, @userId);";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.BigInt));
                command.Parameters.Add(new SqlParameter("@nuevoAmigoId", SqlDbType.BigInt));
                command.Parameters["@userId"].Value = userId;
                command.Parameters["@nuevoAmigoId"].Value = nuevoAmigoId;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        public bool eliminarRelacionesAmigos(int idUser) 
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuarios_amigos] WHERE [usuario_id]=@userId or [amigo_id]=@userId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.BigInt));
                command.Parameters["@userId"].Value = idUser;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }
        }

        public bool eliminarUsuario(int idUser) 
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[usuarios] WHERE [id]=@userId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@userId", SqlDbType.BigInt));
                command.Parameters["@userId"].Value = idUser;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }

        }


        //devuelve el ID del usuario agregado a la base, si algo falla devuelve -1
        public int agregarUsuario(string Nombre, string Apellido, string Dni, string Email, string Password, bool EsADM, int IntentosFallidos, bool Bloqueado)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoUsuario = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[usuarios] ([nombre],[apellido],[dni],[email],[password],[es_admin],[intentos_fallidos],[bloqueado]) VALUES (@nombre,@apellido,@dni,@email,@password,@esadm,@intentosFallidos,@bloqueado);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@intentosFallidos", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@apellido"].Value = Apellido;
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@email"].Value = Email;
                command.Parameters["@password"].Value = Password;
                command.Parameters["@esadm"].Value = EsADM;
                command.Parameters["@intentosFallidos"].Value = IntentosFallidos;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([id]) FROM [dbo].[usuarios]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoUsuario = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar: " + ex.Message);
                    return -1;
                }
                return idNuevoUsuario;
            }
        }


        public int modificarUsuario(int Id, string Nombre, string Apellido, string Dni, string Email, bool EsADM, int IntentosFallidos, bool Bloqueado)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[usuarios] SET [nombre]=@nombre, [apellido]=@apellido, [dni]=@dni, [email]=@email, [es_admin]=@esadm, [intentos_fallidos]=@intentosFallidos, [bloqueado]=@bloqueado WHERE [id]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@nombre", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@apellido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@dni", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@esadm", SqlDbType.Bit));
                command.Parameters.Add(new SqlParameter("@intentosFallidos", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@bloqueado", SqlDbType.Bit));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@nombre"].Value = Nombre;
                command.Parameters["@apellido"].Value = Apellido;
                command.Parameters["@dni"].Value = Dni;
                command.Parameters["@email"].Value = Email;
                command.Parameters["@esadm"].Value = EsADM;
                command.Parameters["@intentosFallidos"].Value = IntentosFallidos;
                command.Parameters["@bloqueado"].Value = Bloqueado;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        
        public List<Post> inicializarPost()
        {
            List<Post> misPost = new List<Post>();
            

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[posts];";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Post post = new Post();
                        post.Id = Convert.ToInt32(reader[0]);
                        post.Usuario = this.getUserFromDatabase(Convert.ToInt32(reader[1]));
                        post.Contenido = Convert.ToString(reader[2]);
                        post.Fecha = Convert.ToDateTime(reader[3]);
                        
                        misPost.Add(post);
                        
                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return misPost;
        }

        //public int obtenerPostAmigo(Usuario amigo, string contenido)
       //{

        //}

        public int agregarPost(Post post)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoPost = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[posts] ([usuario_id],[contenido],[created_at]) VALUES (@usuario,@contenido,@fecha);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@usuario", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@contenido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters["@usuario"].Value = post.Usuario.Id;
                command.Parameters["@contenido"].Value = post.Contenido;
                command.Parameters["@fecha"].Value = post.Fecha;


                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([id]) FROM [dbo].[posts]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoPost = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar: " + ex.Message);
                    return -1;
                }
                return idNuevoPost;
            }
        }

        public int agregarComentario(Comentario comentario)
        {
            //primero me aseguro que lo pueda agregar a la base
            int resultadoQuery;
            int idNuevoComentario = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[comentarios] ([post_id], [usuario_id],[contenido],[created_at]) " +
                                    "VALUES (@postId, @usuarioId, @contenido,@fecha);";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@postId", SqlDbType.BigInt));
                command.Parameters.Add(new SqlParameter("@usuarioId", SqlDbType.BigInt));
                command.Parameters.Add(new SqlParameter("@contenido", SqlDbType.NVarChar));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.DateTime));
                command.Parameters["@postId"].Value = comentario.Post.Id;
                command.Parameters["@usuarioId"].Value = comentario.Usuario.Id;
                command.Parameters["@contenido"].Value = comentario.Contenido;
                command.Parameters["@fecha"].Value = comentario.FechaComentario;

                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //*******************************************
                    //Ahora hago esta query para obtener el ID
                    string ConsultaID = "SELECT MAX([id]) FROM [dbo].[comentarios]";
                    command = new SqlCommand(ConsultaID, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    idNuevoComentario = Convert.ToInt32(reader[0]);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar: " + ex.Message);
                    return -1;
                }
                return idNuevoComentario;
            }
        }

        public bool eliminarComentario(int idComentario)
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[comentarios] WHERE [id]=@comentarioId;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@comentarioId", SqlDbType.BigInt));
                command.Parameters["@comentarioId"].Value = idComentario;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }

        }

        public List<Comentario> obtenerComentariosByPost(int postId)
        {
            List<Comentario> comentarios = new List<Comentario>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[comentarios] WHERE [post_id] = @postId;";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@postId", SqlDbType.BigInt));
                command.Parameters["@postId"].Value = postId;
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Comentario comentario = new Comentario();
                        comentario.Id = Convert.ToInt32(reader[0]);
                        comentario.Post.Id = Convert.ToInt32(reader[1]);
                        comentario.Usuario = this.getUserFromDatabase(Convert.ToInt32(reader[2]));
                        comentario.Contenido = Convert.ToString(reader[3]);
                        comentario.FechaComentario = Convert.ToDateTime(reader[4]);
                        comentarios.Add(comentario);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return comentarios;
        }

        public List<Post> obtenerPostAmigos(int logedUserId)
        {
            List<Post> postAmigos = new List<Post>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[posts] WHERE [usuario_id] " +
                "IN (SELECT amigo_id from [dbo].[usuarios_amigos] WHERE [usuario_id] = @logedUserId);";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@logedUserId", SqlDbType.BigInt));
                command.Parameters["@logedUserId"].Value = logedUserId;
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Post amigoPost = new Post();
                        amigoPost.Id = Convert.ToInt32(reader[0]);
                        amigoPost.Usuario = this.getUserFromDatabase(Convert.ToInt32(reader[1]));
                        amigoPost.Contenido = Convert.ToString(reader[2]);
                        amigoPost.Fecha = Convert.ToDateTime(reader[3]);   
                        //aux = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetInt32(7), reader.GetBoolean(8));
                        postAmigos.Add(amigoPost);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return postAmigos;
        }

        public List<Post> obtenerMisPosts(int logedUserId)
        {
            List<Post> misPosts = new List<Post>();

            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[posts] WHERE [usuario_id] = @logedUserId order by [created_at] desc;";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@logedUserId", SqlDbType.BigInt));
                command.Parameters["@logedUserId"].Value = logedUserId;
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {
                        Post myPost = new Post();
                        myPost.Id = Convert.ToInt32(reader[0]);
                        myPost.Usuario = this.getUserFromDatabase(Convert.ToInt32(reader[1]));
                        myPost.Contenido = Convert.ToString(reader[2]);
                        myPost.Fecha = Convert.ToDateTime(reader[3]);
                        //aux = new Usuario(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetBoolean(6), reader.GetInt32(7), reader.GetBoolean(8));
                        misPosts.Add(myPost);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return misPosts;
        }

        public bool eliminarPost(int idPost)
        {
            bool result = false;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[posts] WHERE [id]=@idPost;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@idPost", SqlDbType.BigInt));
                command.Parameters["@idPost"].Value = idPost;
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return result;
            }

        }

        public int modificarPost(int Id, string Contenido)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[posts] SET [contenido]=@contenido WHERE [id]=@id;";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@contenido", SqlDbType.NVarChar));
                command.Parameters["@id"].Value = Id;
                command.Parameters["@contenido"].Value = Contenido;
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    return command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        public List<Tag> inicializarTags()
        {

            List<Tag> misTags = new List<Tag>();
            /*select t.id, t.palabra, p.id
            from posts as p
            join posts_tags as r on r.post_id = p.id
            join tags as t on t.id = r.tag_id*/


            //Defino el string con la consulta que quiero realizar
            string queryString = "SELECT * FROM [dbo].[tags];";

            // Creo una conexión SQL con un Using, de modo que al finalizar, la conexión se cierra y se liberan recursos
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Defino el comando a enviar al motor SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(queryString, connection);
                try
                {
                    //Abro la conexión
                    connection.Open();
                    //mi objecto DataReader va a obtener los resultados de la consulta, notar que a comando se le pide ExecuteReader()
                    SqlDataReader reader = command.ExecuteReader();
                    //mientras haya registros/filas en mi DataReader, sigo leyendo
                    while (reader.Read())
                    {

                        Tag tag = new Tag();
                        tag.Id = Convert.ToInt32(reader[0]);
                        tag.Palabra = Convert.ToString(reader[1]);
                        misTags.Add(tag);

                    }
                    //En este punto ya recorrí todas las filas del resultado de la query
                    reader.Close();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return misTags;
        }
    }
}