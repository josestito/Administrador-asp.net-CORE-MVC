using Npgsql;
using Proyecto_De_Titulo_Organizado.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Proyecto_De_Titulo_Organizado.Servicios
{
    public class ServicioEmpleado
    {
        private readonly string connectionString = "Host=dpg-coi2rrdjm4es739hoqh0-a.oregon-postgres.render.com;Username=admin;Password=27mkJDxEeTgFA9oWunHDm889rp5vHEyu;Database=bd_portafolio_filr";
        
        
        
        public async Task<IEnumerable<Empleado>> ObtenerTodosLosEmpleados()
        {
            var connection = new NpgsqlConnection(connectionString);
            var empleados = await connection.QueryAsync<Empleado>(@"SELECT * FROM empleado");
            return empleados;
        }

        public async Task AgregarEmpleado(Empleado empleado)
        {
            var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync($@"INSERT INTO empleado 
                                (rut,nombre,apellido_p,apellido_m,contra_emp,tipo_empleado_tipo_empleado_id) 
                                VALUES (@rut,@nombre,@apellido_p,@apellido_m,@contra_emp,@tipo_empleado_tipo_empleado_id);", empleado);
        }

        public async Task EliminarEmpleado(Empleado empleado)
        {
            var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync("DELETE FROM empleado WHERE rut = @rut;", empleado);

        }

        public async Task<IEnumerable<Empleado>> BuscarEmpleado(Empleado empleado)
        {
            var connection = new NpgsqlConnection(connectionString);

            
            var ListaDeTrabajadoresEncontrados = await connection.QueryAsync<Empleado>("SELECT * FROM empleado WHERE rut = @rut;", empleado);

            return ListaDeTrabajadoresEncontrados;
        }

        public async Task ActualizarEmpleado(Empleado empleado)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                var query = new StringBuilder("UPDATE empleado SET ");
                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(empleado.nombre))
                {
                    query.Append("nombre = @nombre, ");
                    parameters.Add("nombre", empleado.nombre);
                }
                if (!string.IsNullOrEmpty(empleado.apellido_p))
                {
                    query.Append("apellido_p = @apellido_p, ");
                    parameters.Add("apellido_p", empleado.apellido_p);
                }
                if (!string.IsNullOrEmpty(empleado.apellido_m))
                {
                    query.Append("apellido_m = @apellido_m, ");
                    parameters.Add("apellido_m", empleado.apellido_m);
                }
                if (!string.IsNullOrEmpty(empleado.contra_emp))
                {
                    query.Append("contra_emp = @contra_emp, ");
                    parameters.Add("contra_emp", empleado.contra_emp);
                }
                if (empleado.tipo_empleado_tipo_empleado_id != null)
                {
                    query.Append("tipo_empleado_tipo_empleado_id = @tipo_empleado_tipo_empleado_id, ");
                    parameters.Add("tipo_empleado_tipo_empleado_id", empleado.tipo_empleado_tipo_empleado_id);
                }

                // Remover la última coma y espacio
                query.Length -= 2;

                query.Append(" WHERE rut = @rut");
                parameters.Add("rut", empleado.rut);

                await connection.ExecuteAsync(query.ToString(), parameters);
            }
        }

        public async Task<bool> ExisteEmpleado(Empleado empleado)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteScalarAsync<int?>("SELECT 1 FROM empleado WHERE rut = @rut", new { empleado.rut });

                return result.HasValue;
            }
        }


    }
}
