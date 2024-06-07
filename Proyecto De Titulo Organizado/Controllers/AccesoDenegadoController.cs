using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto_De_Titulo_Organizado.Controllers
{
    public class AccesoDenegadoController : Controller
    {
        public AccesoDenegadoController()
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> AccesoDenegado()
        {
            return View();
        }

    }
}
