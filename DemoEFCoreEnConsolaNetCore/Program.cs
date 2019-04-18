using DemoEFCoreEnConsolaNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using DemoEFCoreEnConsolaNetCore.Services;
using Microsoft.EntityFrameworkCore;

namespace DemoEFCoreEnConsolaNetCore
{
    class Program
    {
        const bool RESTAURAR_BBDD = true;
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Estudiantes.FirstOrDefault() == null || RESTAURAR_BBDD)
                {
                    InicializarDatos(context);
                }

                PruebasSeleccion(context);
                PruebasInserccion(context);
                PruebasActualzacion(context);
                PruebasEliminacion(context);
            }
        }


        static void InicializarDatos ( ApplicationDbContext context )
        {
            //Vaciamos la base de datos por si ya existen datos.
            //este comando necesita    using Microsoft.EntityFrameworkCore;
            context.Database.ExecuteSqlCommand("DELETE FROM [Estudiantes]");

            Random r = new Random();
            int aleatorio;

            List<Estudiante> lstEstudiantes = new List<Estudiante>();
            for (int contador = 0; contador < 40; contador += 1)
            {
                Estudiante estudiante = new Estudiante
                {
                    Nombre = "Estudiante" + contador,
                    Fecha = DateTime.Now
            };
                //El valor de borrado lo ponemos aleatoriamente
                aleatorio= r.Next(1, 3);
                if (aleatorio == 1)
                {
                    estudiante.Borrado = true;
                }
                else
                {
                    estudiante.Borrado = false;
                }
                lstEstudiantes.Add(estudiante);
            }

            context.AddRange(lstEstudiantes);
            context.SaveChanges();
        }


        static void PruebasSeleccion (ApplicationDbContext context)
        {
            List<Estudiante> lstEstu;
            Estudiante estu;
            string cadena;

            //Comience por Estu
            estu = context.Estudiantes.FirstOrDefault(x => x.Nombre.StartsWith("Estu"));
            //El primero que encuentre y se coge solo un campo
            cadena = context.Estudiantes.Select(x => x.Nombre).FirstOrDefault();
            //Se crea un objeto para devolver
            var objEstu = context.Estudiantes.Select(x => new { x.Nombre , x.Fecha }).FirstOrDefault();
            //Se mapea el resultado creando un objeto estudiante.
            estu = context.Estudiantes.Select(x => new Estudiante{ Nombre=x.Nombre, Fecha=x.Fecha }).FirstOrDefault();
            //Devuelve todos los resultados si el nombre contiene "12"
            lstEstu = context.Estudiantes.Where(x => x.Nombre.Contains("12")).ToList();
            //Se ordena por nombre y se salta los 10 primeros y devuelve 20 registros
            lstEstu = context.Estudiantes.OrderBy(x => x.Nombre).Skip(10).Take(20).ToList();
            //Paginación
            var pagina = 1;
            var muestra = 10;
            var estudiantes = context.Estudiantes.Skip((pagina - 1) * muestra).Take(muestra).ToList();
            //Ejecucion diferida. Hasta que no se lanzan extensiones como toList(), FirstOrDefault() no se deseancadena la ejecucion de la consulta
            // de forma que podemos ir creando la consulta en partes.
            var query = context.Estudiantes.Where(x => x.Nombre.Contains("12"));
            query = query.OrderBy(x => x.Nombre);
            estu = query.FirstOrDefault();

            //------ Agrupaciones
            var reporte = context.Estudiantes.GroupBy(x => new { x.Borrado })
                                             .Select(y=> new {y.Key,count= y.Count(), y.Key.Borrado}).ToList();
            //var reporte = context.Estudiantes.GroupBy(x => new { x.Borrado, x.SegundoCampoAgrupacion }).Select(y => new { y.Key, count = y.Count() }).ToList();

            // Ahora es el reporte con having. Ver la diferencia, si el where se pone delante antes que group by entonces es where sino es having
            var reporte2 = context.Estudiantes.Where(x=> x.Nombre.StartsWith("Est"))
                                              .GroupBy(x => new { x.Borrado })
                                              .Where(x => x.Count() > 4)
                                              .Select(y => new { y.Key, count = y.Count(), y.Key.Borrado })
                                              .ToList();
        }

        static void PruebasInserccion(ApplicationDbContext context)
        {
            Estudiante estudiante;
            List<Estudiante> lstEstudiantes = new List<Estudiante>();

            //Podemos agregar estudiantes de uno en uno o en bloque.

            //---- Agregamos solo un registro
            estudiante = new Estudiante();
            estudiante.Nombre = "Pepe";
            estudiante.Fecha = new DateTime(2019,7,20);
            context.Estudiantes.Add(estudiante);
            context.SaveChanges();


            //---- Agregamos varios registros un registro
            //Esta forma genera una solo consulta con la inserccion de todos los estudiantes
            estudiante = new Estudiante
            {
                Nombre = "Estu Inserccion 2",
                Fecha = new DateTime(1988, 7, 15)
            };
            lstEstudiantes.Add(estudiante);

            estudiante = new Estudiante
            {
                Nombre = "Estu Inserccion 2",
                Fecha = DateTime.Now
            };
            lstEstudiantes.Add(estudiante);
            
            context.Estudiantes.AddRange(lstEstudiantes);
            context.SaveChanges();
        }

        static void PruebasEliminacion(ApplicationDbContext context)
        {

        }

        static void PruebasActualzacion(ApplicationDbContext context)
        {
            
        }



    }
}
