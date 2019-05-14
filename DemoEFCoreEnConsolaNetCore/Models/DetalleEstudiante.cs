namespace DemoEFCoreEnConsolaNetCore.Models
{
    public class DetalleEstudiante
    {
        public int Id { get; set; }
        
        public string Identificacion { get; set; }

        //Relación con el Estudiante. Se relaciona solo por convencion por nombre clase + Id
        public int EstudianteId { get; set; }

    }
}