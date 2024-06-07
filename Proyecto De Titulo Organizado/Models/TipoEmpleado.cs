using System.ComponentModel.DataAnnotations;

namespace Proyecto_De_Titulo_Organizado.Models
{
    public class TipoEmpleado
    {
        public int? tipo_empleado_id { get; set; }
        [MaxLength(50,ErrorMessage ="Este campo admite como maximo 50 caracteres")]
        public string? rol_emp { get; set; }
        public IEnumerable<TipoEmpleado>? ListaTipoEmpleado { get; set; }
    }
}
