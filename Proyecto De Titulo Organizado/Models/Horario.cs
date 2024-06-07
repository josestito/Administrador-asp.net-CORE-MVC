using System.ComponentModel.DataAnnotations;

namespace Proyecto_De_Titulo_Organizado.Models
{
    public class Horario
    {
        public int? id { get; set; }
        [MaxLength(255)]
        public string id_esp { get; set; }
        [MaxLength(255)]
        public string nom_esp { get; set; }
        public TimeSpan hora_entrada { get; set; }
        public TimeSpan hora_salida { get; set; }
        [MaxLength(20,ErrorMessage ="el dia tiene como maximo 20 caracteres")]
        public string dia { get; set; }
        public IEnumerable<Espacio>? ListaDeEspacios { get; set; }
        public IEnumerable<Horario>? ListaDeHorarios { get; set; }
    }
}
