using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema.Datos;
using Sistema.Entidades.Clientes;
using Sistema.Web.Models.Clientes.Socios;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public SociosController(DbContextSistema context)
        {
            _context = context;
        }


        // GET: api/Socios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SociosViewModel>> Listar()
        {
            var socios = await _context.Socios.Take(100).ToListAsync();

            return socios.Select(u => new SociosViewModel
            {

                NROSOCIO = u.NROSOCIO,
                RUT = u.RUT,
                DVRUT = u.DVRUT,
                Paterno = u.Paterno,
                Materno = u.Materno,
                Nombres = u.Nombres,
                ESTADO = u.Estado
            });
        }


        // GET: api/Socios/Mostrar/1
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {

            var socio = await _context.Socios.
                SingleOrDefaultAsync(a => a.NROSOCIO == id);

            if (socio == null)
            {
                return NotFound();
            }

            return Ok(new SociosViewModel
            {
                NROSOCIO = socio.NROSOCIO,
                RUT = socio.RUT,
                DVRUT = socio.DVRUT,
                Paterno = socio.Paterno,
                Materno = socio.Materno,
                Nombres = socio.Nombres,
                ESTADO = socio.Estado
            });
        }


        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.NROSOCIO == id);
        }
    }
}