using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrases.MODELO
{
    public class Frase
    {
        public int Id { get; set; }
        public String Texto { get; set; }
        public int Autor { get; set; }
        public int Categoria { get; set; }
    }
}