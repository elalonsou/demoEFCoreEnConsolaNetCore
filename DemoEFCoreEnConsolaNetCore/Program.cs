using DemoEFCoreEnConsolaNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoEFCoreEnConsolaNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Estudiantes.FirstOrDefault() == null)
                {
                    InicializarDatos(context);
                }

                PruebasSeleccion(context);

            }
        }


        static void InicializarDatos ( ApplicationDbContext context )
        {
            List<Estudiante> lstEstudiantes = new List<Estudiante>();
            for (int contador = 0; contador < 40; contador += 1)
            {
                Estudiante estudiante = new Estudiante
                {
                    Nombre = "Estudiante" + contador,
                    Fecha = DateTime.Now
                };
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
        }

        static void PruebasInserccion(ApplicationDbContext context)
        {
            //Estudiante estudiante = new Estudiante();
            //estudiante.Nombre = "Pepe";
            ////estudiante.Fecha = new DateTime(2019,7,20);
            //estudiante.Fecha = DateTime.Now;
            //context.Estudiantes.Add(estudiante);

            //context.Estudiantes.AddRange(lstEstudiantes);

            //context.SaveChanges();
        }

        static void PruebasEliminacion(ApplicationDbContext context)
        {

        }
    }
}
