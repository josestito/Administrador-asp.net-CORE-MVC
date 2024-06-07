using Npgsql;
using Proyecto_De_Titulo_Organizado.Models;
using Dapper;

namespace Proyecto_De_Titulo_Organizado.Servicios
{
    public class ServicioEspacio
    {
        private readonly string connectionString = "Host=dpg-coi2rrdjm4es739hoqh0-a.oregon-postgres.render.com;Username=admin;Password=27mkJDxEeTgFA9oWunHDm889rp5vHEyu;Database=bd_portafolio_filr";


        public async Task<IEnumerable<Espacio>> ObtenerTodosLosEspacios()
        {
            var connection = new NpgsqlConnection(connectionString);
            var espacios = await connection.QueryAsync<Espacio>(@"SELECT * FROM espacio");
            return espacios;
        }

        public async Task AgregarEspacio(Espacio espacio)
        {
            using var connection = new NpgsqlConnection(connectionString);

            await connection.ExecuteAsync($@"INSERT INTO espacio (nom_esp, id_esp) VALUES (@nom_esp,@id_esp);", espacio);
        }

        public async Task BorrarEspacioPorID(Espacio espacio)
        {
            using var connection = new NpgsqlConnection(connectionString);
            await connection.ExecuteAsync($@"DELETE FROM espacio WHERE id_esp = @id_esp;", espacio);
        }
    }
}
