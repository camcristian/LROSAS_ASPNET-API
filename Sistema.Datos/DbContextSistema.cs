using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Clientes;
using Sistema.Datos.Mapping.Eventos;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Datos.Mapping.Riesgo;
using Sistema.Entidades.Clientes;
using Sistema.Entidades.Eventos;
using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Riesgo;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Evento> Evento { get; set; }

        public DbSet<RIESGO_PRESTAMOS_SOLICITADOS> RIESGO_PRESTAMOS_SOLICITADOS { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new SocioMap());
            modelBuilder.ApplyConfiguration(new EventoMap());
            modelBuilder.ApplyConfiguration(new RIESGO_PRESTAMOS_SOLICITADOSMap());
        }

    }
}
