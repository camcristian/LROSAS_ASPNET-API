using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Clientes.Socios
{
    public class SociosViewModel
    {
        public int NROSOCIO { get; set; }
        public long RUT { get; set; }
        public string DVRUT { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string Nombres { get; set; }
        public int ESTADO { get; set; }

    }
}
