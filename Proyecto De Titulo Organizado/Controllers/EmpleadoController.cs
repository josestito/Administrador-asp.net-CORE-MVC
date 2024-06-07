using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_De_Titulo_Organizado.Models;
using Proyecto_De_Titulo_Organizado.Servicios;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ServicioTipoEmpleado servicioTipoEmpleado;
        private readonly ServicioEmpleado servicioEmpleado;

        public EmpleadoController(ServicioTipoEmpleado servicioTipoEmpleado, ServicioEmpleado servicioEmpleado)
        {
            this.servicioTipoEmpleado = servicioTipoEmpleado;
            this.servicioEmpleado = servicioEmpleado;
        }

        [HttpGet]
        public async Task<IActionResult> AdministrarEmpleado()
        {
            if (HttpContext.Session.GetInt32("sesion") != 1)//verifica que la sesion a sido iniciada para obtener la vista
            {
                return RedirectToAction("AccesoDenegado", "AccesoDenegado");
            }

            var empleado = await CrearListas();//crea las listas que se envian a la vista y se las asigna al modelo de empleado.
            return View(empleado);//retorna la vista y envia el modelo hacia la vista.
        }

        [HttpPost]
        public async Task<IActionResult> AgregarEmpleado(Empleado empleado)
        {
            //validacion de campos vacios para agregar empleados
            if (empleado.rut == null)
            {
                ModelState.AddModelError(nameof(empleado.rut), "El campo RUT es obligatorio para AGREGAR.");
            }
            if (empleado.nombre == null)
            {
                ModelState.AddModelError(nameof(empleado.nombre), "El campo NOMRE es obligatorio para AGREGAR.");
            }
            if (empleado.apellido_p == null)
            {
                ModelState.AddModelError(nameof(empleado.apellido_p), "El campo APELLIDO PATERNO es obligatorio para AGREGAR.");
            }
            if (empleado.apellido_m == null)
            {
                ModelState.AddModelError(nameof(empleado.apellido_m), "El campo APELLIDO MATERNO es obligatorio para AGREGAR.");
            }
            if (empleado.contra_emp == null)
            {
                ModelState.AddModelError(nameof(empleado.contra_emp), "El campo CONTRASEÑA es obligatorio para AGREGAR.");
            }
            //fin validacion de campos vacios.

            
            if (await servicioEmpleado.ExisteEmpleado(empleado))//verifica si el empleado ya existe en la base de datos.
            {
                TempData["ErrorMessage"] = "El RUT del empleado ya existe en los registros.";

                var empleadoConListas = await CrearListas();//crea las listas que se envian a la vista y se las asigna al modelo de empleado.

                return View("administrarempleado", empleadoConListas);//si es que ya existe, devuelve nuevamente a la vista.
            }
            
            if (!ModelState.IsValid)//valida si el modelo es valido
            {
                var empleadoConListas = await CrearListas();//crea las listas que se envian a la vista y se las asigna al modelo de empleado.

                return View("administrarempleado", empleadoConListas);
            }


            await servicioEmpleado.AgregarEmpleado(empleado);//agrega el empleado a la base de datos

            return RedirectToAction("administrarempleado");
        }

        [HttpPost]
        public async Task<IActionResult> BorrarEmpleado(Empleado empleado)
        {
            //VALIDACION SI EL RUT QUE SE INTENTA BORRAR ESTA VACIO
            if (empleado.rut == null)
            {
                ModelState.AddModelError(nameof(empleado.rut), "El campo RUT es obligatorio para ELIMINAR.");

                var empleadoConListas = await CrearListas();

                return View("administrarempleado", empleadoConListas);
            }

            //VALIDACION SI EL RUT QUE SE INTENTA ELIMINAR NO EXISTE EN LOS REGISTROS
            if (!await servicioEmpleado.ExisteEmpleado(empleado))
            {
                TempData["ErrorMessage"] = "El RUT que se intenta eliminar no existe en los registros.";

                var empleadoConListas = await CrearListas();

                return View("administrarempleado", empleadoConListas);
            }

            //VALIDACION SI EL MODELO NO ES VALIDO
            if (!ModelState.IsValid)
            {
                var empleadoConListas = await CrearListas();

                return View("administrarempleado", empleadoConListas);
            }
            //MENSAJE DE EXITO AL ELIMINAR UN EMPLEADO DE LOS REGISTROS
            TempData["SuccessMessage"] = "Empleado eliminado de los registros.";

            await servicioEmpleado.EliminarEmpleado(empleado);
            return RedirectToAction("administrarempleado");
        }


        [HttpPost]
        public async Task<IActionResult> BuscarEmpleado(Empleado empleado)
        {
            if (empleado.rut == null)
            {
                ModelState.AddModelError(nameof(empleado.rut), "El campo RUT es obligatorio para BUSCAR.");
                var empleadoConListas = await CrearListas();

                return View("administrarempleado", empleadoConListas);
            }

            if (!await servicioEmpleado.ExisteEmpleado(empleado))
            {
                var empleadoConListas = await CrearListas();
                TempData["ErrorMessage"] = "No existe ningun empleado en los registros con ese rut.";

                return View("administrarempleado", empleadoConListas);
            }

            var modelo = new Empleado
            {
                ListaDeEmpleados = await servicioEmpleado.BuscarEmpleado(empleado)
            };

            return View("EmpleadosEncontrados", modelo);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarEmpleado(Empleado empleado)
        {
            if (empleado.rut == null)
            {
                ModelState.AddModelError(nameof(empleado.rut), "El campo RUT es obligatorio para actualizar.");

                empleado = await CrearListas();

                return View("AdministrarEmpleado", empleado);
            }

            if (!await servicioEmpleado.ExisteEmpleado(empleado))
            {
                var empleadoConListas = await CrearListas();
                TempData["ErrorMessage"] = "No existe ningun empleado en los registros con ese rut para actualizar.";

                return View("administrarempleado", empleadoConListas);
            }

            // Validar otros campos si es necesario
            bool hasValidFields = !string.IsNullOrEmpty(empleado.nombre) ||
                                  !string.IsNullOrEmpty(empleado.apellido_p) ||
                                  !string.IsNullOrEmpty(empleado.apellido_m) ||
                                  !string.IsNullOrEmpty(empleado.contra_emp) ||
                                  empleado.tipo_empleado_tipo_empleado_id != null;

            if (!hasValidFields)
            {
                ModelState.AddModelError("", "Debe proporcionar al menos un campo válido para actualizar.");

                empleado = await CrearListas();

                return View("AdministrarEmpleado", empleado);
            }

            await servicioEmpleado.ActualizarEmpleado(empleado);
            return RedirectToAction("AdministrarEmpleado");
        }

        //METODO PARA CREAR LAS LISTAS QUE SE ENVIAN HACIA LA VISTA
        public async Task<Empleado> CrearListas()
        {
            var empleado = new Empleado
            {
                ListaDeEmpleados = await servicioEmpleado.ObtenerTodosLosEmpleados(),
                ListaDeTipoEmpleado = await servicioTipoEmpleado.ObtenerTodosLosTiposTrabajador()
            };
            return empleado;
        }

    }
}
