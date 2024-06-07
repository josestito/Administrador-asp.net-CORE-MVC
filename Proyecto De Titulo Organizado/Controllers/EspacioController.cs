using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;
using System.Collections;
using System.Reflection;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class EspacioController : Controller
    {
        private readonly ServicioEspacio servicioEspacio;

        public EspacioController(ServicioEspacio servicioEspacio)
        {
            this.servicioEspacio = servicioEspacio;
        }
        [HttpGet]
        public async Task<IActionResult> AdministrarEspacios()
        {
            if (HttpContext.Session.GetInt32("sesion") != 1)
            {
                return RedirectToAction("AccesoDenegado", "AccesoDenegado");
            }

            var modelo = new Espacio {
                ListaDeEspacios = await servicioEspacio.ObtenerTodosLosEspacios()
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEspacio(Espacio espacio)
        {
            if (espacio.nom_esp == null || espacio.id_esp == null)
            {
                ModelState.AddModelError(nameof(espacio.nom_esp), "Para AGREGAR un espacio debe llenar el NOMBRE del espacio");
                ModelState.AddModelError(nameof(espacio.id_esp), "Para AGREGAR un espacio debe llenar el ID del espacio");

                espacio = new Espacio
                {
                    ListaDeEspacios = await servicioEspacio.ObtenerTodosLosEspacios()
                };
                return View("administrarespacios", espacio);
            }
            await servicioEspacio.AgregarEspacio(espacio);

            return RedirectToAction("administrarespacios");
        }

        public async Task<IActionResult> BorraEspacio(Espacio espacio)
        {
            if (espacio.id_esp == null)
            {
                ModelState.AddModelError(nameof(espacio.id_esp), "Para BORRAR un espacio debe llenar el ID del espacio");

                espacio = new Espacio
                {
                    ListaDeEspacios = await servicioEspacio.ObtenerTodosLosEspacios()
                };
                return View("administrarespacios", espacio);
            }


            TempData["SuccessMessage"] = "Espacio eliminado.";
            await servicioEspacio.BorrarEspacioPorID(espacio);
            return RedirectToAction("administrarespacios");
        }
    }

}
