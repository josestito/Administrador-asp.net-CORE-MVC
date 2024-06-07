using Npgsql;
using Proyecto_De_Titulo_Organizado.Models;
using Dapper;

namespace Proyecto_De_Titulo_Organizado.Servicios
{
    public class ServicioRegistro
    {
        private readonly string connectionString = "Host=dpg-coi2rrdjm4es739hoqh0-a.oregon-postgres.render.com;Username=admin;Password=27mkJDxEeTgFA9oWunHDm889rp5vHEyu;Database=bd_portafolio_filr";

        public async Task<IEnumerable<Registro>> ObtenerTodosLosRegistros()
        {
            var connection = new NpgsqlConnection(connectionString);
            var registro = await connection.QueryAsync<Registro>(@"SELECT registro.id_registro,
                                                                   empleado.id_emp,
                                                                   empleado.nombre,
                                                                   empleado.apellido_p,
	                                                               empleado.apellido_m,
                                                                   espacio.nom_esp,
                                                                   registro.fecha_entrada,
                                                                   registro.fecha_salida
                                                            FROM registro
                                                            LEFT JOIN empleado ON registro.empleado_id_emp = empleado.id_emp
                                                            LEFT JOIN espacio ON registro.espacio_id_esp = espacio.id_esp;");
            return registro;
        }
    }
}
