using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Eventos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sistema.Datos.Mapping.Eventos
{
    class EventoMap : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable("_WEB_EVENTOS")
   .HasKey(r => r.ID);
        }
    }
}
