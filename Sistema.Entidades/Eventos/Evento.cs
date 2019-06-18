using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Eventos
{
    public class Evento
    {

        public int ID { get; set; }
        public int ID_USUARIO { get; set; }
        public long TITULO { get; set; }
        public string DETALLE { get; set; }
        public string FECHA { get; set; }
   
    }
}
