using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Models
{
    class Estudiante
    {
        public int Id { get; set; }
         
        private string _nombre;
        
        //Ejemplo de data notations que hacen que permiten realizar configuraciones sobre el campo.
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value + "_Modi";
            }
        }

        public DateTime Fecha { get; set; }

        public bool Borrado { get; set; }

        //Relacion 1 a N con direccionEstudiante
        public List<DireccionEstudiante> Direcciones { get; set; }

        public List<EstudianteCurso> EstudiantesCursos { get; set; }
        
        //Relacion con el detalle del estudiante
        public DetalleEstudiante DetalleEstudiante { get; set; }
    }
}
