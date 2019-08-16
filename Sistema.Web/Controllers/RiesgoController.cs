using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Riesgo;
using Sistema.Web.Models.Riesgo;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiesgoController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public RiesgoController(DbContextSistema context)
        {
            _context = context;
        } 
        
        
        // GET: api/Riesgo/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RiesgoViewModel>> Listar()
        {
            var riesgo
           = await _context.RIESGO_PRESTAMOS_SOLICITADOS
           .OrderByDescending(v => v.id_solicitud)
           .Take(100)
           .ToListAsync();

            return riesgo.Select(r => new RiesgoViewModel
            {
                id_solicitud = r.id_solicitud,
                Nrosocio = r.Nrosocio,
                fecha_solicitud = r.fecha_solicitud.Substring(0, 10),
                producto = r.producto,
                objetivo = r.objetivo,
                monto_solicitado = r.monto_solicitado,
                Ejecutiva = r.Ejecutiva,
                Estado = r.Estado,
                tasa = r.tasa,
                nrocuotas = r.nrocuotas,
                corcredito = r.corcredito
















            });
        }

        // GET: api/Riesgo/Listar
        [HttpGet("[action]/{FechaInicio}/{FechaFinal}")]
        public async Task<IEnumerable<RiesgoViewModel>> Listarfechas([FromRoute]int FechaInicio, int FechaFinal)
        {
            var riesgo 
           = await _context.RIESGO_PRESTAMOS_SOLICITADOS
            .Where(v=>  Convert.ToInt32(v.fecha_solicitud.Substring(0, 10).Replace("-",""))>= FechaInicio)
            .Where(v => Convert.ToInt32(v.fecha_solicitud.Substring(0, 10).Replace("-", "")) >= FechaFinal)
           .OrderByDescending(v => v.id_solicitud)
           .Take(100)
           .ToListAsync();

            return riesgo.Select(r => new RiesgoViewModel
            {
        id_solicitud = r.id_solicitud,
                Nrosocio = r.Nrosocio,
                fecha_solicitud = r.fecha_solicitud.Substring(0, 10),
                producto = r.producto,
                objetivo= r.objetivo,
                monto_solicitado = r.monto_solicitado,
                Ejecutiva = r.Ejecutiva,
                Estado = r.Estado,
                tasa = r.tasa,
                nrocuotas = r.nrocuotas,
                corcredito = r.corcredito
            });
        }

        private bool RIESGO_PRESTAMOS_SOLICITADOSExists(int id)
        {
            return _context.RIESGO_PRESTAMOS_SOLICITADOS.Any(e => e.id_solicitud == id);
        }
    }
}