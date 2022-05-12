using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TP1_PlataformaDesarrollo
{
    public class Comentario
    {
        public int Id { get; set; }

        public string Contenido { get; set; }

        public DateTime FechaComentario { get; set; }

        public Comentario() { }
        public Comentario(string contenido, DateTime fecha)
        {
            Contenido = contenido;
            FechaComentario = fecha;
        }

        public Usuario Usuario { get; set; }

        public Post Post { get; set; }
    }
}
