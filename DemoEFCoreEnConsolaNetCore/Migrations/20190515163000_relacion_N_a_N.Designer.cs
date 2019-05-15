﻿// <auto-generated />
using System;
using DemoEFCoreEnConsolaNetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DemoEFCoreEnConsolaNetCore.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190515163000_relacion_N_a_N")]
    partial class relacion_N_a_N
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.DetalleEstudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EstudianteId");

                    b.Property<string>("Identificacion");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId")
                        .IsUnique();

                    b.ToTable("DetalleEstudiante");
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.DireccionEstudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Direccion");

                    b.Property<int>("EstudianteId");

                    b.HasKey("Id");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Borrado");

                    b.Property<DateTime>("Fecha");

                    b.Property<string>("Nombre");

                    b.HasKey("Id");

                    b.ToTable("Estudiantes");
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.EstudianteCurso", b =>
                {
                    b.Property<int>("CursoId");

                    b.Property<int>("EstudianteId");

                    b.HasKey("CursoId", "EstudianteId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("EstudiantesCursos");
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.DetalleEstudiante", b =>
                {
                    b.HasOne("DemoEFCoreEnConsolaNetCore.Models.Estudiante")
                        .WithOne("DetalleEstudiante")
                        .HasForeignKey("DemoEFCoreEnConsolaNetCore.Models.DetalleEstudiante", "EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.DireccionEstudiante", b =>
                {
                    b.HasOne("DemoEFCoreEnConsolaNetCore.Models.Estudiante", "Estudiante")
                        .WithMany("Direcciones")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DemoEFCoreEnConsolaNetCore.Models.EstudianteCurso", b =>
                {
                    b.HasOne("DemoEFCoreEnConsolaNetCore.Models.Curso", "Curso")
                        .WithMany("EstudiantesCursos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DemoEFCoreEnConsolaNetCore.Models.Estudiante", "Estudiante")
                        .WithMany("EstudiantesCursos")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
