using DemoEFCoreEnConsolaNetCore.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore
{
    class ApplicationDbContext: DbContext
    {
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoEFCoreEnConsolaNetCore;Integrated Security=True")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));
        }

        //TODO hacer un ejemplo de mapeo
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estudiante>().Property(x => x.Nombre).HasField("_nombre");

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Estudiante> Estudiantes { get; set; }
        

    }
}
