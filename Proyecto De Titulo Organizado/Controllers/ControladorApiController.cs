using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Servicios;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class ControladorApiController : Controller
    {
        private readonly ServicioEspacio servicioEspacio;
        private readonly ServicioRegistro servicioRegistro;

        public ControladorApiController(ServicioEspacio servicioEspacio,ServicioRegistro servicioRegistro)
        {
            this.servicioEspacio = servicioEspacio;
            this.servicioRegistro = servicioRegistro;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEspaciosJson()
        {
            var modelo = await servicioEspacio.ObtenerTodosLosEspacios();
            return Json(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerRegistrosJson()
        {
            var modelo = await servicioRegistro.ObtenerTodosLosRegistros();
            return Json(modelo);
        }
    }
}
