using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Clientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Clientes

{
    public class SocioMap : IEntityTypeConfiguration<Socio>
    {
        public void Configure(EntityTypeBuilder<Socio> builder)
        {
            builder.ToTable("_socios")
        .HasKey(r => r.NROSOCIO);
        }
    }
}
