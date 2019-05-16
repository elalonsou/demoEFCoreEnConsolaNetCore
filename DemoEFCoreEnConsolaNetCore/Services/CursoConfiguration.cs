using DemoEFCoreEnConsolaNetCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Services
{
    class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.HasIndex(x => x.Nombre);
            builder.Property(x => x.Nombre).HasMaxLength(100);
        }
    }
}
