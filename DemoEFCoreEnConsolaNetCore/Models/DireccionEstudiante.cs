using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Models
{
    class DireccionEstudiante
    {
        public int Id { get; set; }
        
        //El Id del estudiante relacionado. Por el estandar automaticamente 
        //Entity framework hara la relacion por el nombre usado.
        public int EstudianteId { get; set; }

        public string Direccion { get; set; }

        public Estudiante Estudiante { get; set; }

    }
}
