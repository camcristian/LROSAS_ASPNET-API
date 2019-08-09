using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Riesgo
{
    public class RiesgoViewModel
    {
        public long id_solicitud { get; set; }
        public string Nrosocio { get; set; }
        public string fecha_solicitud { get; set; }
        public string producto { get; set; }
        public string objetivo { get; set; }
        public string monto_solicitado { get; set; }
        public string Ejecutiva { get; set; }
        public string Estado { get; set; }
        public string tasa { get; set; }
        public string nrocuotas { get; set; }
        public string corcredito { get; set; }
    }
}
