using Proyecto_De_Titulo_Organizado.Models;
using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_De_Titulo_Organizado.Servicios
{
    public class ServicioTipoEmpleado
    {

        private readonly string connectionString = "Host=dpg-coi2rrdjm4es739hoqh0-a.oregon-postgres.render.com;Username=admin;Password=27mkJDxEeTgFA9oWunHDm889rp5vHEyu;Database=bd_portafolio_filr";

        //OBTENER TODOS LOS TIPO EMPLEADOS DE LA BASE DE DATOS Y ENVIARLOS COMO UN IENUMERABLE
        public async Task<IEnumerable<TipoEmpleado>> ObtenerTodosLosTiposTrabajador()
        {
            using var connection = new NpgsqlConnection(connectionString);
            var tipostrabajadores = await connection.QueryAsync<TipoEmpleado>(@"SELECT * FROM tipo_empleado ORDER BY tipo_empleado_id");
            return tipostrabajadores;
        }

        public async Task AgregarTipoTrabajador(TipoEmpleado tipoEmpleado)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync($@"INSERT INTO tipo_empleado (rol_emp) VALUES (@rol_emp);", tipoEmpleado);
        }

        public async Task BorrarTipoTrabajadorPorNombre(TipoEmpleado tipoEmpleado)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync($@"DELETE FROM tipo_empleado WHERE rol_emp = @rol_emp;", tipoEmpleado);
        }

    }
}
