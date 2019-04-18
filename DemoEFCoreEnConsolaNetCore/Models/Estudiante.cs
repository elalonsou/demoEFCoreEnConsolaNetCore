using System;
using System.Collections.Generic;
using System.Text;

namespace DemoEFCoreEnConsolaNetCore.Models
{
    class Estudiante
    {
        public int Id { get; set; }

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value + "_Modi";
            }
        }

        public DateTime Fecha { get; set; }

    }
}
