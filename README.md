# Demo Entity Framework Core
Se trata de un proyecto de prueba para la práctica y aprendizaje de Entity Framework Net Core con el objetivo de terminar con un proyecto comentado con los principales usos para tener ejemplos a mano.
## Instalar Entity Framework Core
### Paquetes de Nuget:
`Install-Package Microsoft.EntityFrameworkCore.SqlServer`

`Install-Package Microsoft.EntityFrameworkCore.Tools`

`Install-Package Microsoft.Extensions.Logging.Console`

> Se puede ver una lista de proveedores disponibles a parte de SqlServer en [Este enlace](https://docs.microsoft.com/es-es/ef/core/providers/index)
### __En aplicaciones net framework__
Se crea una clase que herede de DbContext y sobreescribimos el metodo *OnConfiguring* para configurar la cadena de conexión.
```
class ApplicationDbContext: DbContext
{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DemoWinFormsUdemy;Integrated Security=True")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));
        }
}
```
### __En aplicaciones ASP Net Core__
En este caso no suele ser necesario instalar los paquetes nuget porque ya vienen configurados.

Clase con la configuración:

Este caso es distinto de netFramework. Crearemos la clase DbContext con el constructor que se pase a la clase padre y la configuración la crearemos en la clase `startup.cs` que es donde se configura el middleware y entonces agregaremos el dbcontext como un servicio.
