﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Models
{
    class EstudianteCurso
    {
        public int EstudianteId { get; set; }

        public int CursoId { get; set; }

        public Estudiante Estudiante { get; set; }

        public Curso Curso { get; set; }
    }
}
