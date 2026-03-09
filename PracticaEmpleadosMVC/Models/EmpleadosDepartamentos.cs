namespace PracticaEmpleadosMVC.Models
{
    public class EmpleadosDepartamentos
    {

        public Departamento Departamento { get; set; }
        public List<Empleado> Empleados { get; set; }
        public int NumeroRegistros { get; set; }
    }
}
