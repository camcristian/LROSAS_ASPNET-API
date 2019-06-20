using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Eventos
{
    public class CrearEventosViewModel
    {

        public int ID_USUARIO { get; set; }
        public string title { get; set; }
        public string details { get; set; }
        public string date { get; set; }
        public int Tipo { get; set; }

    }
}
