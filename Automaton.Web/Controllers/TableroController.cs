using Microsoft.AspNetCore.Mvc;
using Automaton.Web.Models;
using Automaton.Web.Logica;
using Microsoft.AspNetCore.Authorization;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly IDirectorJuego directorJuego;

        public TableroController(
            IDirectorJuego directorJuego
            )
        {
            this.directorJuego = directorJuego;
        }

        [Authorize]
        [HttpPost("[action]")]
        public JuegoResponse GetTablero(TableroRequest tableroRequest)
        {
            var usuario = this.HttpContext.User.Identity.Name;
            return directorJuego.Iniciar(tableroRequest.LogicaRobot, usuario);
        }
    }
}