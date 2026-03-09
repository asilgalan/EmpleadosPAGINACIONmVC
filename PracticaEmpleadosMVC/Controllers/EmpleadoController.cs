using Microsoft.AspNetCore.Mvc;
using PracticaEmpleadosMVC.Models;
using PracticaEmpleadosMVC.Repositories;

namespace PracticaEmpleadosMVC.Controllers
{
    public class EmpleadoController : Controller
    {

        private readonly RepositoryEmpleados empleados;

        public EmpleadoController(RepositoryEmpleados empleados)
        {
            this.empleados= empleados;
        }
      
        public async Task<IActionResult> DepartamentoView()
        {

            List<Departamento> departamentos = await this.empleados.GetDepartamentoAsync();
            return View(departamentos);
        }

        public async Task<IActionResult> EmpleadosDepartamentoOut(int? posicion, int idDepartamento)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            EmpleadosDepartamentos model = await this.empleados.GetEmpleadosDepartamentosAsync(posicion.Value, idDepartamento);
            ViewData["NUMREGISTROS"] = model.NumeroRegistros;
            ViewData["POSICION"] = posicion;
            int siguiente = posicion.Value + 1;
            if (siguiente > model.NumeroRegistros)
            {
                siguiente = model.NumeroRegistros;

            }
            int anterior=posicion.Value  - 1 ;
            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ULTIMO"] = model.NumeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["DEPT"] = model.Departamento.Id;
            return View(model);

        }
    }
}
