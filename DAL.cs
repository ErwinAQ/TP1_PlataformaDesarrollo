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
        //comentar despues
        public List<Post> inicializarPost(int id, int id_usuario, string contenido, DateTime created_at)
        {
            List<Post> crearPost = new List<Post>();

            //Defino el string pero FALTA CREAR POST
            int resultadoQuery;
            int idNuevoPost = -1;
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "INSERT INTO [dbo].[posts] ([id],[id_usuario],[contenido],[created_at]) VALUES(@id,@id_usuario,@contenido,@created_at)"; //verificar post id, paso intermedio para idusuario
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt));
                command.Parameters.Add(new SqlParameter("@usuario_id", SqlDbType.BigInt));
                command.Parameters.Add(new SqlParameter("@contenido", SqlDbType.Text));
                command.Parameters.Add(new SqlParameter("@created_at", SqlDbType.DateTime));
                command.Parameters["@id"].Value = id;
                command.Parameters["@usuario_id"].Value = id_usuario;
                command.Parameters["@contenido"].Value = contenido;
                command.Parameters["@created_at"].Value = created_at;

                //Verificar
                try
                {
                    connection.Open();
                    //esta consulta NO espera un resultado para leer, es del tipo NON Query
                    resultadoQuery = command.ExecuteNonQuery();

                    //***************
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
    }
        /*public int eliminarPost(int Post_id) //varchar o int a la hora de crear el post
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "DELETE FROM [dbo].[Post] WHERE Post_id=@post_id";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@post_id", SqlDbType.Int));
                command.Parameters["@post_id"].Value = Post_id;
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
        }*/

        /*public int modificarPost(int Post_id, date Fecha, string Contenido)
        {
            string connectionString = Properties.Resources.ConnectionStr;
            string queryString = "UPDATE [dbo].[Usuarios] SET Post_id=@post_id, Fecha=@fecha,Contenido=@contenido,";
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.Add(new SqlParameter("@post_id", SqlDbType.Int));
                command.Parameters.Add(new SqlParameter("@fecha", SqlDbType.Date));
                command.Parameters.Add(new SqlParameter("@contenido", SqlDbType.NVarChar));
                
                command.Parameters["@post_id"].Value = Post_id;
                command.Parameters["@fecha"].Value = Fecha;
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
        }*/

    
}