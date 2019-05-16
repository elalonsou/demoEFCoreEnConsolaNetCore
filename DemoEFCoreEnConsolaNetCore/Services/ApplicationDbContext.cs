using DemoEFCoreEnConsolaNetCore.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
                .UseLoggerFactory(GetLoggerFactory());
                
                //Esta forma ya esta deprecada.
                //.UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));  
        }

        //Con este metodo conseguimos que las querys a BBDD se registren en la consola.
        // El uso del Logger Factory es nuevo en Entity Framework core 2.2
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //---------------------------   API FLUENTE   ------------------------------------
            //Aqui se configura el API Fluente
            //Un ejemplo de mapeo. Se mapea la propiedad Nombre con el campo del modelo _nombre.
            modelBuilder.Entity<Estudiante>().Property(x => x.Nombre).HasField("_nombre");

            modelBuilder.Entity<EstudianteCurso>().HasKey(x => new { x.CursoId, x.EstudianteId });

            modelBuilder.ApplyConfiguration(new CursoConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        //Al crear un DbSet hacemos que se cree la tabla al realizar una migración
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<DireccionEstudiante> Direcciones { get; set; }
        public DbSet<DetalleEstudiante> DetalleEstudiante { get; set; }
        public DbSet<Curso> Curso{ get; set; }
        // Tabla para la Relacion N a N entre cursos y estudiantes
        public DbSet<EstudianteCurso> EstudiantesCursos { get; set; }


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
