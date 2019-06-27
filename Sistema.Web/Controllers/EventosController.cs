using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sistema.Datos;
using Sistema.Entidades.Eventos;
using Sistema.Web.Models.Eventos;

namespace Sistema.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly DbContextSistema _context;

        public EventosController(DbContextSistema context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
       
        }

        public IConfiguration Configuration { get; }



        public async Task ActualizarFecha()
        {


      
            using (SqlConnection sql = new SqlConnection(Configuration.GetConnectionString("Conexion")))
            {
                using (SqlCommand cmd = new SqlCommand("_Web_Eventos_FechaLimite", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@value1", value.Value1));
                    //cmd.Parameters.Add(new SqlParameter("@value2", value.Value2));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        // GET: api/Eventos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EventosViewModel>> Listar()
        {
            await ActualizarFecha();
            var EVENTOS = await _context.Evento.ToListAsync();

            return EVENTOS.Select(e => new EventosViewModel
            {
                ID =e.ID,
                ID_USUARIO = e.ID_USUARIO,
                title = e.title,
                details = e.details,
                date =e.date,
                open=false,
               Tipo =e.Tipo,
             Estado=e.Estado,
            Encargado =e.Encargado
    });

        }




        // GET: api/Eventos/ListarUsuario/id

        [HttpGet("[action]/{xusuario}")]
        public async Task<IEnumerable<EventosViewModel>> ListarUsuario([FromRoute] int xusuario)
        {

            await ActualizarFecha();

            var EVENTOS = await _context.Evento
                .Where(i => i.ID_USUARIO == xusuario)
                .ToListAsync();

            return EVENTOS.Select(e => new EventosViewModel
            {
                ID = e.ID,
                ID_USUARIO = e.ID_USUARIO,
                title = e.title,
                details = e.details,
                date = e.date,
                open = false,
                Tipo = e.Tipo,
                Estado = e.Estado,
                Encargado = e.Encargado
            });


        }









        // GET: api/Eventos/ListarUsuario/1
        //[HttpGet("[action]")]

        //public async Task<IEnumerable<EventosViewModel>> ListarUsuario([FromRoute] int xID_USUARIO)
        //{
        //    var EVENTOS = await _context.Evento.Where(u => u.ID_USUARIO == xID_USUARIO).ToListAsync();
          


        //    return EVENTOS.Select(e => new EventosViewModel
        //    {
        //        ID = e.ID,
        //        ID_USUARIO = e.ID_USUARIO,
        //        title = e.title,
        //        details = e.details,
        //        date = e.date,
        //        open = false,
        //        Tipo = e.Tipo,
        //        Estado = e.Estado
        //    });

        //}








        // POST: api/Eventos/Crear
        //  [Authorize(Roles = "Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearEventosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Evento evento = new Evento
            {
               ID_USUARIO= model.ID_USUARIO,
                title = model.title,
                details = model.details,
                date = model.date,
                Tipo =model.Tipo,
                Encargado = model.Encargado,
                Estado=1
            };

            _context.Evento.Add(evento);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


        // PUT: api/Eventos/Descartar/1
        //[Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Descartar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(u => u.ID == id);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Estado = 2;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Eventos/Realizado/1
        //[Authorize(Roles = "Administrador")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Realizado([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(u => u.ID == id);

            if (evento == null)
            {
                return NotFound();
            }

            evento.Estado = 4;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }



        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.ID == id);
        }
    }
}