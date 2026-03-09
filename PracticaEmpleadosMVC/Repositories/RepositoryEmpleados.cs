using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using PracticaEmpleadosMVC.Data;
using PracticaEmpleadosMVC.Models;

namespace PracticaEmpleadosMVC.Repositories
{

    #region procedure
    /*
      create procedure SP_EMPLEADOS_DEPARTAMENTOS_OUT (@posicion int, @iddpto int, @registros int out)
as
   select @registros = count(EMP_NO) from EMP where DEPT_NO =@iddpto   select EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO from (select cast( ROW_NUMBER() over (order by APELLIDO) as int) as POSICION, EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO FROM EMP where DEPT_NO = @iddpto) query 
  where POSICION >= @posicion AND POSICION < (@posicion + 1)
go
     */
    #endregion
    public class RepositoryEmpleados
    {

        private readonly EmpleadosContext context;

        public RepositoryEmpleados(EmpleadosContext context)
        {
            this.context = context;
        }


        public async Task<List<Departamento>> GetDepartamentoAsync() {

            
                return await this.context.Departamentos.ToListAsync();
            }

        public async Task<EmpleadosDepartamentos> GetEmpleadosDepartamentosAsync(int posicion,int idDepartamento)
       
        {
            Departamento departamento =  await this.context.Departamentos.Where(x => x.Id == idDepartamento).FirstOrDefaultAsync();

            string sql = "SP_EMPLEADOS_DEPARTAMENTOS_OUT @posicion,@iddpto,@registros out";

            SqlParameter pamPosicion = new SqlParameter("@posicion", posicion);
            SqlParameter pamIdDpto = new SqlParameter("@iddpto", idDepartamento);
            SqlParameter pamRegistros = new SqlParameter("@registros", 0);
            pamRegistros.Direction = System.Data.ParameterDirection.Output;

            var consulta = this.context.Empleados.FromSqlRaw(sql, pamPosicion, pamIdDpto, pamRegistros);
            List<Empleado> empleados=await consulta.ToListAsync();

            int registros=int.Parse(pamRegistros.Value.ToString());

            return new EmpleadosDepartamentos
            {
                Departamento = departamento,
                Empleados = empleados,
                NumeroRegistros = registros
            };

        }
    }
       
}
