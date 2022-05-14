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

        public int Dni { get; set; }

        public int IntentosFallidos { get; set; }

        public bool Bloqueado { get; set; }

        public Usuario() { }
        public Usuario(string nombre, string apellido, string mail, string pass, int dni)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = mail;
            Password = pass;
            Dni = dni;
        }

        public List<Usuario> Amigos { get; set; }

        public List<Post> MisPost { get; set; }

        public List<Comentario> MisComentarios { get; set; }

        public List<Reaccion> MisReacciones { get; set; }
    }
}
