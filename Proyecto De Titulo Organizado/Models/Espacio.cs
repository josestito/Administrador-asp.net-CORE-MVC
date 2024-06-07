using System.ComponentModel.DataAnnotations;

namespace Proyecto_De_Titulo_Organizado.Models
{
    public class Espacio
    {
        [MaxLength(100, ErrorMessage = "el nombre del espacio puede contener como maximo 100 caracteres")]
        public string? nom_esp { get; set; }
        [MaxLength(100, ErrorMessage = "el id del espacio puede contener como maximo 100 caracteres")]
        public string? id_esp { get; set; }
        public IEnumerable<Espacio>? ListaDeEspacios { get; set; }
    }
}
