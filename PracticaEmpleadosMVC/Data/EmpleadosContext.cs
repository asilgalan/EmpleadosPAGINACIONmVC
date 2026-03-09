using Microsoft.EntityFrameworkCore;
using PracticaEmpleadosMVC.Models;

namespace PracticaEmpleadosMVC.Data
{
    public class EmpleadosContext:DbContext
    {
        public EmpleadosContext(DbContextOptions<EmpleadosContext> options) : base(options)
        { }
        
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
    
    }
}
