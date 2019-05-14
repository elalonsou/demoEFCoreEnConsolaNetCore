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
                PruebasSeleccionRelacionesTablas(context);
                PruebasInserccion(context);
                PruebasActualzacion(context);
                PruebasEliminacion(context);
            }
        }

        //***************************************************************************************************************************************************
        //********************************************************     INICIALIZACION DATOS    **************************************************************
        //***************************************************************************************************************************************************
        static void InicializarDatos ( ApplicationDbContext context )
        {
            //Vaciamos la base de datos por si ya existen datos.
            //este comando necesita    using Microsoft.EntityFrameworkCore;
            context.Database.ExecuteSqlCommand("DELETE FROM [Estudiantes]");

            Random r = new Random();
            int aleatorio;
            DireccionEstudiante dir;

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

                //------ Añadimos la/s direccion/es ------
                //No es necesario indicar el ID del Estudiante porque al asociarlo al modelo el sabe que pertenece al
                //estudiante y le pone automaticamente el ID correspondiente.
                aleatorio = r.Next(1, 3);
                if (aleatorio == 1)
                {
                    estudiante.Direcciones = new List<DireccionEstudiante>();
                    dir = new DireccionEstudiante();
                    dir.Direccion = "Calle Prueba " + contador + 10;
                    estudiante.Direcciones.Add(dir);
                }
                else
                {
                    estudiante.Direcciones = new List<DireccionEstudiante>();
                    dir = new DireccionEstudiante();
                    dir.Direccion = "Calle Prueba " + contador + 10;
                    estudiante.Direcciones.Add(dir);
                    dir = new DireccionEstudiante();
                    dir.Direccion = "Calle Prueba " + contador + 14;
                    estudiante.Direcciones.Add(dir);
                }

                //------ Añadimos detalle del estudiante ------
                DetalleEstudiante detalle;
                detalle = new DetalleEstudiante();
                detalle.Identificacion = $"1-{r.Next(1000,2000)}-{r.Next(100000,900000)}";
                estudiante.DetalleEstudiante=detalle;

                lstEstudiantes.Add(estudiante);
            }

            context.AddRange(lstEstudiantes);
            context.SaveChanges();
        }

        //***************************************************************************************************************************************************
        //***********************************************************        SELECCION       ****************************************************************
        //***************************************************************************************************************************************************
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


            //------------------------     Agrupaciones     ---------------------------
            var reporte = context.Estudiantes.GroupBy(x => new { x.Borrado })
                                             .Select(y=> new {y.Key,count= y.Count(), y.Key.Borrado}).ToList();
            //var reporte = context.Estudiantes.GroupBy(x => new { x.Borrado, x.SegundoCampoAgrupacion }).Select(y => new { y.Key, count = y.Count() }).ToList();

            // Ahora es el reporte con having. Ver la diferencia, si el where se pone delante antes que group by entonces es where sino es having
            var reporte2 = context.Estudiantes.Where(x=> x.Nombre.StartsWith("Est"))
                                              .GroupBy(x => new { x.Borrado })
                                              .Where(x => x.Count() > 4)
                                              .Select(y => new { y.Key, count = y.Count(), y.Key.Borrado })
                                              .ToList();


            //------------------------     Querys Texto     ---------------------------
            //Se puede realizar un query directamente con la consulta
            lstEstu = context.Estudiantes.FromSql("Select * from Estudiantes where Nombre like '%2%'")
                                         .ToList();
            //Tambien se puede realizar un mapeo
            lstEstu = context.Estudiantes.FromSql("Select * from Estudiantes where Nombre like '%2%'")
                                        .Select(x => new Estudiante { Nombre = x.Nombre, Id = x.Id, Fecha = x.Fecha })
                                        .ToList();
        }

        //***************************************************************************************************************************************************
        //***********************************************************        INSERCCION      ****************************************************************
        //***************************************************************************************************************************************************
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

            //--------   Tabla relacionadas   -------
            //Teniendo el Id se puede agregar datos de una tabla relacionada
            //Vamos a obtener un Id de estudiante para el ejemplo pero lo normal es que el Id venga externamente.
            int IdEstu;
            IdEstu = context.Estudiantes.Select(x => x.Id).FirstOrDefault();

            DireccionEstudiante dir;
            dir = new DireccionEstudiante
            {
                EstudianteId = IdEstu,
                Direccion = "Prueba insercción dirección"
            };
            context.Add(dir);
            context.SaveChanges();

        }


            
        //***************************************************************************************************************************************************
        //*********************************************************        ACTUALIZACION      ***************************************************************
        //***************************************************************************************************************************************************
        static void PruebasActualzacion(ApplicationDbContext context)
        {
            Estudiante estu1;
            Estudiante estu2;

            //------------------      Modelo Conectado     --------------------
            estu1 = context.Estudiantes.First(x => x.Nombre.Contains("1"));
            estu1.Nombre = "Nombre_Act_conectado";
            context.SaveChanges();


            //------------------      Modelo Desconectado     --------------------
            //Usamos un contexto distinto para simular un modelo desconectado
            estu2 = estu1 = context.Estudiantes.First(x => x.Nombre.Contains("2"));
            estu2.Nombre = "Nombre_Act_desconectado_1";
            using (ApplicationDbContext context2 = new ApplicationDbContext())
            {
                //El estudiante 2 se obtiene de un contexto distinto por lo que no esta conectado.
                //por ejemplo en un API.
                //Esta forma de hacer marcar como modificado hace que se actualice todo el registro y no solo el nombre.
                context2.Entry(estu2).State = EntityState.Modified;
                context2.SaveChanges();
            }

            estu2 = estu1 = context.Estudiantes.First(x => x.Nombre.Contains("3"));
            estu2.Nombre = "Nombre_Act_desconectado_2";
            using (ApplicationDbContext context2 = new ApplicationDbContext())
            {
                //De esta forma indicamos que solo se ha modificado el nombre.
                Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Estudiante> entrada = context2.Attach(estu2);
                entrada.Property(x => x.Nombre).IsModified = true;
                context2.SaveChanges();
            }
        }

        //***************************************************************************************************************************************************
        //***********************************************************       ELIMINACION      ****************************************************************
        //***************************************************************************************************************************************************
        static void PruebasEliminacion(ApplicationDbContext context)
        {
            Estudiante estu1;
            Estudiante estu2;

            //------------------      Modelo Conectado     --------------------
            estu1 = context.Estudiantes.First(x => x.Nombre.Contains("1"));
            Console.WriteLine("El estudiante " + estu1.Nombre + " se va a eliminar de forma conectada.");
            if (estu1 != null)
            {
                context.Remove(estu1);
                context.SaveChanges();
            }

            //------------------      Modelo Desconectado     --------------------
            //Vamos a obtener un estudiante para saber un id pero realmente no es necesario hacer un select.
            estu2 = context.Estudiantes.First();
            int idEstudiante = estu2.Id;
            Console.WriteLine("El estudiante " + estu2.Nombre + " se va a eliminar de forma desconectada.");
            using ( ApplicationDbContext context2 = new ApplicationDbContext())
            {
                Estudiante estuParaEliminar = new Estudiante();
                estuParaEliminar.Id = idEstudiante;
                context2.Entry(estuParaEliminar).State = EntityState.Deleted;
                context2.SaveChanges();
            }

        }

        //***************************************************************************************************************************************************
        //*********************************************************       SELECCION RELACION      ***********************************************************
        //***************************************************************************************************************************************************
        static void PruebasSeleccionRelacionesTablas ( ApplicationDbContext context)
        {
            Estudiante estu;
            estu=context.Estudiantes.Include(x => x.Direcciones).Where(x => x.Nombre.Contains("2")).FirstOrDefault();

            //Si se quiere incluir otra tabla relacionada con direcciones se usaria thenInclude
            //estu = context.Estudiantes.Include(x => x.Direcciones).ThenInclude(x => x.OtraRelacion)

            Estudiante estu2;
            estu2=context.Estudiantes.Include(x => x.Direcciones).Include(x => x.DetalleEstudiante).Where(x => x.Nombre.Contains("2")).FirstOrDefault();

        }

    }
}
