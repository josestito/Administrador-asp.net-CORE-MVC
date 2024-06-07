using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;

namespace Proyecto_De_Titulo_Organizado.Controllers
{

    public class TipoEmpleadoController : Controller
    {
        private readonly ServicioTipoEmpleado servicioTipoEmpleado;

        public TipoEmpleadoController(ServicioTipoEmpleado servicioTipoEmpleado)
        {
            this.servicioTipoEmpleado = servicioTipoEmpleado;
        }


        [HttpGet]
        public async Task<IActionResult> AdministrarTipoEmpleado()
        {
            if (HttpContext.Session.GetInt32("sesion") != 1)
            {
                return RedirectToAction("AccesoDenegado", "AccesoDenegado");
            }

            var tipoEmpleado = new TipoEmpleado {
                                                    ListaTipoEmpleado = await servicioTipoEmpleado.ObtenerTodosLosTiposTrabajador()
                                                };
           return View(tipoEmpleado);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarTipoTrabajador(TipoEmpleado tipoEmpleado)
        {
            if (tipoEmpleado.rol_emp == null)
            {
                ModelState.AddModelError(nameof(tipoEmpleado.rol_emp),"Debe completar el campo Tipo Trabajador para AGREGAR");

                 tipoEmpleado = new TipoEmpleado
                {
                    ListaTipoEmpleado = await servicioTipoEmpleado.ObtenerTodosLosTiposTrabajador()
                };

                return View("AdministrarTipoEmpleado", tipoEmpleado);
            }
            await servicioTipoEmpleado.AgregarTipoTrabajador(tipoEmpleado);
            return RedirectToAction("administrartipoempleado");
        }

        [HttpPost]
        public async Task<IActionResult> BorraTipoTrabajador(TipoEmpleado tipoEmpleado)
        {
            await servicioTipoEmpleado.BorrarTipoTrabajadorPorNombre(tipoEmpleado);
            return RedirectToAction("administrartipoempleado");
        }


    }
}
