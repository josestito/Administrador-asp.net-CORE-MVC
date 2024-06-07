using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;
using System.Drawing;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class LoginController : Controller
    {
        private readonly ServicioEmpleado servicioEmpleado;

        public LoginController(ServicioEmpleado servicioEmpleado)
        {
            this.servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Empleado empleado)
        {

            empleado.ListaDeEmpleados = await servicioEmpleado.ObtenerTodosLosEmpleados();


            //ASIGNANDO LA ID DEL USUARIO QUE INGRESO A LA VARIABLE SESION
            foreach (var item in empleado.ListaDeEmpleados)
            {
                if (item.rut == empleado.rut && item.contra_emp == empleado.contra_emp && item.tipo_empleado_tipo_empleado_id == 1)
                {
                    
                    HttpContext.Session.SetInt32("sesion", 1);//asigna el valor 1 para sesion como admin
                    return RedirectToAction("AdministrarEmpleado", "Empleado");
                }
            }
        
            TempData["ErrorMessage"] = "Las credenciales no coinciden";
            return View("Login");
        }


        //POST PARA CERRAR SESION
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Elimina todas las variables de sesión
            return RedirectToAction("Login"); // Redirige al usuario a la página de login
        }


    }
}
