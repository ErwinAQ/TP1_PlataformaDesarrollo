using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1_PlataformaDesarrollo
{
    public class Tag
    {
        public int Id { get; set; }

        public string Palabra { get; set; }

        public Tag() { }
        public Tag(string palabra)
        {
            Palabra = palabra;
        }

        public List<Post> Posts { get; set; }
    }
}
