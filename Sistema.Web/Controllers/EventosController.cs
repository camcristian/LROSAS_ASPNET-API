using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public EventosController(DbContextSistema context)
        {
            _context = context;
        }


        // GET: api/Eventos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<EventosViewModel>> Listar()
        {
            var EVENTOS = await _context.Evento.ToListAsync();

            return EVENTOS.Select(e => new EventosViewModel
            {
                ID =e.ID,
                ID_USUARIO = e.ID_USUARIO,
                title = e.title,
                details = e.details,
                date =e.date,
                open=false,
               Tipo =e.Tipo
    });

        }



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
                Tipo = 1,
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


        private bool EventoExists(int id)
        {
            return _context.Evento.Any(e => e.ID == id);
        }
    }
}