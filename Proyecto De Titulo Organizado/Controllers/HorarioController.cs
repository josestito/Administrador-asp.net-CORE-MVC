using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class HorarioController : Controller
    {
        private readonly ServicioEspacio servicioEspacio;
        private readonly ServicioHorario servicioHorario;

        public HorarioController(ServicioEspacio servicioEspacio, ServicioHorario servicioHorario)
        {
            this.servicioEspacio = servicioEspacio;
            this.servicioHorario = servicioHorario;
        }

        [HttpGet]
        public async Task<IActionResult> AdministrarHorario()
        {
            if (HttpContext.Session.GetInt32("sesion") != 1)
            {
                return RedirectToAction("AccesoDenegado", "AccesoDenegado");
            }

            var modelo = new Horario { 
                
                ListaDeEspacios = await servicioEspacio.ObtenerTodosLosEspacios(),
                ListaDeHorarios = await servicioHorario.ObtenerTodosLosHorarios()
                };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarHorario(Horario horario)
        {
            TempData["SuccessMessage"] = "Bloque de horario ingresado correctamente.";
            await servicioHorario.AgregarHorario(horario);
            return RedirectToAction("AdministrarHorario");
        }

        [HttpPost]
        public async Task<IActionResult> BorrarHorario(Horario horario)
        {
            if (!await servicioHorario.ExisteHorario(horario))
            {
                var modelo = new Horario
                {

                    ListaDeEspacios = await servicioEspacio.ObtenerTodosLosEspacios(),
                    ListaDeHorarios = await servicioHorario.ObtenerTodosLosHorarios()
                };

                TempData["ErrorMessage"] = "El ID del horario que intenta eliminar no existe en los registros.";
                return View("AdministrarHorario", modelo);
            }
            await servicioHorario.EliminarHorario(horario);
            return RedirectToAction("AdministrarHorario");
        }


    }
}
