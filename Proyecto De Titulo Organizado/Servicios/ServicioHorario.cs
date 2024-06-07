using Npgsql;
using Proyecto_De_Titulo_Organizado.Models;
using Dapper;


namespace Proyecto_De_Titulo_Organizado.Servicios
{
    public class ServicioHorario
    {
        private readonly string connectionString = "Host=dpg-coi2rrdjm4es739hoqh0-a.oregon-postgres.render.com;Username=admin;Password=27mkJDxEeTgFA9oWunHDm889rp5vHEyu;Database=bd_portafolio_filr";

        public async Task<IEnumerable<Horario>> ObtenerTodosLosHorarios()
        {
            var connection = new NpgsqlConnection(connectionString);
            var horarios = await connection.QueryAsync<Horario>(@"SELECT * FROM horario");
            return horarios;
        }

        public async Task AgregarHorario(Horario horario)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                await connection.ExecuteAsync(@"INSERT INTO horario (id_esp, nom_esp, hora_entrada, hora_salida, dia) 
                            VALUES (@id_esp, @nom_esp, @hora_entrada, @hora_salida, @dia)", horario);
            }
        }

        public async Task EliminarHorario(Horario horario)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                await connection.ExecuteAsync("DELETE FROM horario WHERE id = @id", horario);
            }
        }

        public async Task<bool> ExisteHorario(Horario horario)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var result = await connection.ExecuteScalarAsync<int?>("SELECT 1 FROM horario WHERE id = @id", new { horario.id });

                return result.HasValue;
            }
        }

    }
}
