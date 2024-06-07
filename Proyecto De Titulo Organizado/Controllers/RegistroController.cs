using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class RegistroController : Controller
    {
        private readonly ServicioRegistro servicioRegistro;

        public RegistroController(ServicioRegistro servicioRegistro)
        {
            this.servicioRegistro = servicioRegistro;
        }

        [HttpGet]
        public async Task<IActionResult> AdministrarRegistro()
        {
            if (HttpContext.Session.GetInt32("sesion") != 1)
            {
                return RedirectToAction("AccesoDenegado", "AccesoDenegado");
            }
            var registros = new Registro
            {
                ListaDeRegistros = await servicioRegistro.ObtenerTodosLosRegistros()
            };
            return View(registros);
        }

        [HttpPost]
        public async Task<IActionResult> FiltrarRegistrosDelDia()
        {
            var todosLosRegistros = await servicioRegistro.ObtenerTodosLosRegistros();

            var registrosDelDia = todosLosRegistros.Where(x => x.fecha_entrada.Date == DateTime.Today);

            var registros = new Registro
            {
                ListaDeRegistros = registrosDelDia
            };
            return View("AdministrarRegistro",registros);
        }

        [HttpPost]
        public async Task<IActionResult> FiltrarRegistrosDelMes()
        {
            var todosLosRegistros = await servicioRegistro.ObtenerTodosLosRegistros();

            var registrosDelDia = todosLosRegistros.Where(x => x.fecha_entrada.Month == DateTime.Today.Month);

            var registros = new Registro
            {
                ListaDeRegistros = registrosDelDia
            };
            return View("AdministrarRegistro", registros);
        }

        [HttpPost]
        public async Task<IActionResult> FiltrarRegistrosTodos()
        {
            var todosLosRegistros = await servicioRegistro.ObtenerTodosLosRegistros();

            var registrosDelDia = todosLosRegistros;

            var registros = new Registro
            {
                ListaDeRegistros = registrosDelDia
            };
            return View("AdministrarRegistro", registros);
        }

        [HttpPost]
        public async Task<IActionResult> FiltrarRegistrosFechaEspecifica(string dia, string mes, string anio)
        {
            if (!string.IsNullOrEmpty(dia))
            {
                dia = dia.TrimStart('0');
            }

            if (!string.IsNullOrEmpty(mes))
            {
                mes = mes.TrimStart('0');
            }
            
            var todosLosRegistros = await servicioRegistro.ObtenerTodosLosRegistros();

            IEnumerable<Registro> registrosFiltrados;

            
            if (!string.IsNullOrWhiteSpace(dia) && !string.IsNullOrWhiteSpace(mes) && !string.IsNullOrWhiteSpace(anio))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Day.ToString() == dia &&
                    x.fecha_entrada.Month.ToString() == mes &&
                    x.fecha_entrada.Year.ToString() == anio);
            }
            else if (!string.IsNullOrWhiteSpace(dia) && !string.IsNullOrWhiteSpace(mes))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Day.ToString() == dia &&
                    x.fecha_entrada.Month.ToString() == mes);
            }
            else if (!string.IsNullOrWhiteSpace(mes) && !string.IsNullOrWhiteSpace(anio))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Month.ToString() == mes &&
                    x.fecha_entrada.Year.ToString() == anio);
            }
            else if (!string.IsNullOrWhiteSpace(dia))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Day.ToString() == dia);
            }
            else if (!string.IsNullOrWhiteSpace(mes))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Month.ToString() == mes);
            }
            else if (!string.IsNullOrWhiteSpace(anio))
            {
                
                registrosFiltrados = todosLosRegistros.Where(x =>
                    x.fecha_entrada.Year.ToString() == anio);
            }
            else
            {
                
                registrosFiltrados = todosLosRegistros;
            }

            var registros = new Registro
            {
                ListaDeRegistros = registrosFiltrados
            };
            return View("AdministrarRegistro", registros);
        }

    }
}
