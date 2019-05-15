using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Models
{
    class Curso
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<EstudianteCurso> EstudiantesCursos { get; set; }

    }
}
