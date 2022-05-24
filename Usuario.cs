using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TP1_PlataformaDesarrollo
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public String Dni { get; set; }

        public int IntentosFallidos { get; set; }

        public bool Bloqueado { get; set; }

        public bool EsADM { get; set; }

        public Usuario() { }
        public Usuario(int id, string nombre, string apellido, String dni, string mail, string pass, bool esADM, int intentosFallidos, bool bloqueado)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Email = mail;
            Password = pass;
            EsADM = esADM;
            IntentosFallidos = intentosFallidos;
            Bloqueado = bloqueado;

            
        }

        public List<Usuario> Amigos { get; set; }

        public List<Post> MisPost { get; set; }

        public List<Comentario> MisComentarios { get; set; }

        public List<Reaccion> MisReacciones { get; set; }
    }
}
