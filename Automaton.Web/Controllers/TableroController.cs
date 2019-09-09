using Microsoft.AspNetCore.Mvc;
using Automaton.Web.Models;
using Automaton.Web.Logica;
using Microsoft.AspNetCore.Authorization;
using Automaton.Logica;
using AutoMapper;
using Automaton.Logica.Dtos.Model;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableroController : Controller
    {
        private readonly IDirectorJuego directorJuego;
        private readonly IMapper mapper;

        public TableroController(
            IDirectorJuego directorJuego,
            IMapper mapper
            )
        {
            this.directorJuego = directorJuego;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpPost("[action]")]
        public JuegoResponse GetTablero(TableroRequest tableroRequest)
        {
            var usuario = this.HttpContext.User.Identity.Name;
            var partida = directorJuego.Iniciar(tableroRequest.LogicaRobot, usuario);
            var juego = mapper.Map<JuegoResponse>(partida);
            return juego;
        }
    }
}