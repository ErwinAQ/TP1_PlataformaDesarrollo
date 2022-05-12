using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TP1_PlataformaDesarrollo
{
    public class Post
    {
        public int Id { get; set; }

        public string Contenido { get; set; }

        public DateTime Fecha { get; set; }

        public Post() { }
        public Post(string contenido, DateTime fecha)
        {
            Contenido = contenido;
            Fecha = fecha;
        }

        public Usuario Usuario { get; set; }

        public List<Comentario> Comentarios { get; set; }

        public List<Reaccion> Reacciones { get; set; }

        public List<Tag> Tags { get; set; }
    }
}
