using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Riesgo;
using System;
using System.Collections.Generic;
using System.Text;
namespace Sistema.Datos.Mapping.Riesgo
{
   

        public class RIESGO_PRESTAMOS_SOLICITADOSMap : IEntityTypeConfiguration<RIESGO_PRESTAMOS_SOLICITADOS>
        {
         
            public void Configure(EntityTypeBuilder< RIESGO_PRESTAMOS_SOLICITADOS > builder)
            {
            builder.ToTable("_RIESGO_PRESTAMOS_SOLICITADOS")
   .HasKey(r => r.id_solicitud);
        }
        }
    




}
