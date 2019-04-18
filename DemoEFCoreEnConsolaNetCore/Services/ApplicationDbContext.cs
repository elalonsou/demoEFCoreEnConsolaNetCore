using DemoEFCoreEnConsolaNetCore.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Services
{
    class ApplicationDbContext: DbContext
    {
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Usamos una instancia de SqlServer localDb que es suficiente para realizar este demo.
            //Tambien configuramos para que las consultas que se realicen se muestren por consola.
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoEFCoreEnConsolaNetCore;Integrated Security=True")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Un ejemplo de mapeo. Se mapea la propiedad Nombre con el campo del modelo _nombre.
            modelBuilder.Entity<Estudiante>().Property(x => x.Nombre).HasField("_nombre");

            base.OnModelCreating(modelBuilder);
        }

        //Al crear un DbSet hacemos que se cree la tabla al realizar una migración
        public DbSet<Estudiante> Estudiantes { get; set; }


        //**********************************************************************************************************************************
        //************************************************    MIGRACIONES   ****************************************************************
        //**********************************************************************************************************************************
        //++++++ Nueva Migracion
        //  add-migration nombreMigracion(Package manager console)
        //  dotnet ef migrations add nombreMigracion(CLI net core)
        //
        //++++++ Actualizar cambios en BBDD
        //  update-database(Package manager console)
        //  dotnet ef database update(CLI net core)
        //
        //++++++ Generar Script Cambios
        //  script-migration
        //
        //++++++ Eliminar ultima migración
        //dotnet ef migrations remove (CLI net core)
        //**********************************************************************************************************************************
    }
}
