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




        //using System;    using System.Collections.Generic;    using System.Data;    using System.Data.SqlClient;    using System.Linq;    using System.Threading.Tasks;        namespace MVCAdoDemo.Models    {        public class EmployeeDataAccessLayer        {            string connectionString = "Your Connection String here";                //To View all employees details              public IEnumerable<Employee> GetAllEmployees()            {                List<Employee> lstemployee = new List<Employee>();                    using (SqlConnection con = new SqlConnection(connectionString))                {                    SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);                    cmd.CommandType = CommandType.StoredProcedure;                        con.Open();                    SqlDataReader rdr = cmd.ExecuteReader();                        while (rdr.Read())                    {                        Employee employee = new Employee();                            employee.ID = Convert.ToInt32(rdr["EmployeeID"]);                        employee.Name = rdr["Name"].ToString();                        employee.Gender = rdr["Gender"].ToString();                        employee.Department = rdr["Department"].ToString();                        employee.City = rdr["City"].ToString();                            lstemployee.Add(employee);                    }                    con.Close();                }                return lstemployee;            }                //To Add new employee record              public void AddEmployee(Employee employee)            {                using (SqlConnection con = new SqlConnection(connectionString))                {                    SqlCommand cmd = new SqlCommand("spAddEmployee", con);                    cmd.CommandType = CommandType.StoredProcedure;                        cmd.Parameters.AddWithValue("@Name", employee.Name);                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);                    cmd.Parameters.AddWithValue("@Department", employee.Department);                    cmd.Parameters.AddWithValue("@City", employee.City);                        con.Open();                    cmd.ExecuteNonQuery();                    con.Close();                }            }                //To Update the records of a particluar employee            public void UpdateEmployee(Employee employee)            {                using (SqlConnection con = new SqlConnection(connectionString))                {                    SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);                    cmd.CommandType = CommandType.StoredProcedure;                        cmd.Parameters.AddWithValue("@EmpId", employee.ID);                    cmd.Parameters.AddWithValue("@Name", employee.Name);                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);                    cmd.Parameters.AddWithValue("@Department", employee.Department);                    cmd.Parameters.AddWithValue("@City", employee.City);                        con.Open();                    cmd.ExecuteNonQuery();                    con.Close();                }            }                //Get the details of a particular employee            public Employee GetEmployeeData(int? id)            {                Employee employee = new Employee();                    using (SqlConnection con = new SqlConnection(connectionString))                {                    string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;                    SqlCommand cmd = new SqlCommand(sqlQuery, con);                        con.Open();                    SqlDataReader rdr = cmd.ExecuteReader();                        while (rdr.Read())                    {                        employee.ID = Convert.ToInt32(rdr["EmployeeID"]);                        employee.Name = rdr["Name"].ToString();                        employee.Gender = rdr["Gender"].ToString();                        employee.Department = rdr["Department"].ToString();                        employee.City = rdr["City"].ToString();                    }                }                return employee;            }                //To Delete the record on a particular employee            public void DeleteEmployee(int? id)            {                    using (SqlConnection con = new SqlConnection(connectionString))                {                    SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);                    cmd.CommandType = CommandType.StoredProcedure;                        cmd.Parameters.AddWithValue("@EmpId", id);                        con.Open();                    cmd.ExecuteNonQuery();                    con.Close();                }            }        }    }



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




        // PUT: api/Eventos/Posponer
      //  [Authorize(Roles = "Administrador")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Posponer([FromBody] EventosPosponerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.ID <= 0)
            {
                return BadRequest();
            }

            var evento = await _context.Evento.FirstOrDefaultAsync(u => u.ID == model.ID);

            if (evento == null)
            {
                return NotFound();
            }

            evento.ID = model.ID;
            evento.details = model.details;
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
        public async Task<IActionResult> DescartarPosponer([FromRoute] int id)
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
            evento.details = "Pospuesto";

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