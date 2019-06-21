using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Entidades.Eventos
{
    public class Evento
    {

        public int ID { get; set; }
        public int ID_USUARIO { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public string date { get; set; }
        public int Tipo { get; set; }
        public int Estado { get; set; }

    }
}
