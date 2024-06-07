namespace Proyecto_De_Titulo_Organizado.Models
{
    public class Registro
    {
        public int id_registro { get; set; }
        public int id_emp { get; set; }
        public string nombre { get; set; }
        public string apellido_p { get; set; }
        public string apellido_m { get; set; }
        public string nom_esp { get; set; }
        public DateTime fecha_entrada { get; set; }
        public DateTime fecha_salida { get; set; }
        public IEnumerable<Registro> ListaDeRegistros { get; set; }
    }
}
