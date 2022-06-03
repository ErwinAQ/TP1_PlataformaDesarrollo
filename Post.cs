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

        

        public Post() {

            this.Usuario = new Usuario();
            this.Comentario = new Comentario();
            this.Reaccion = new Reaccion();
            this.Tag = new Tag();

        }
        public Post(int id, Usuario usuario,string contenido  )
        {
            Contenido = contenido;
            
            Usuario = usuario;  

        }

        public Usuario Usuario { get; set; }

        public Comentario Comentario { get; set; } 

        public Reaccion Reaccion { get; set; }

        public Tag Tag { get; set; }

      
    }
}
