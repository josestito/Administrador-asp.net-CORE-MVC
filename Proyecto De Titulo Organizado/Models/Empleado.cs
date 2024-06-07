using Microsoft.AspNetCore.Mvc.ModelBinding;
using Proyecto_De_Titulo_Organizado.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_De_Titulo_Organizado.Models
{
    public class Empleado
    {
        [ValidarRutChilenoAtributte] //validacion personalizada de rut chileno valido y sin guion
        [MaxLength(12,ErrorMessage ="El rut puede contener como maximo 12 caracteres")]
        public string? rut { get; set; }
        [MaxLength(100,ErrorMessage ="el nombre puede contener como maximo 100 caracteres")]
        public string? nombre { get; set; }
        [MaxLength(100, ErrorMessage = "el apellido paterno puede contener como maximo 100 caracteres")]
        public string? apellido_p { get; set; }
        [MaxLength(100, ErrorMessage = "el apellido materno puede contener como maximo 100 caracteres")]
        public string? apellido_m { get; set; }
        [MaxLength(12, ErrorMessage = "la contraseña puede tener como maximo 12 caracteres")]
        public string? contra_emp { get; set; }
        public int? tipo_empleado_tipo_empleado_id { get; set; }
        public IEnumerable<Empleado>? ListaDeEmpleados { get; set; }
        public IEnumerable<TipoEmpleado>? ListaDeTipoEmpleado { get; set; }
    }
}
