using AutoMapper;
using Automaton.Logica;
using Automaton.Logica.Registro;
using Automaton.Web.Models;
using Automaton.Web.Models.Torneo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TorneoController : ControllerBase
    {
        private readonly IRegistroPartidas registroPartidas;
        private readonly IMapper mapper;

        public TorneoController(
            IRegistroPartidas registroPartidas,
            IMapper mapper)
        {
            this.registroPartidas = registroPartidas;
            this.mapper = mapper;
        }

        // GET: api/Torneo
        [HttpGet]
        public PartidosTorneoModel Get()
        {
            var partidas = registroPartidas.ObtenerUltimasPartidas();
            var maped = mapper.Map<PartidosTorneoModel>(partidas);
            return maped;
        }

        // GET: api/Torneo/{id}
        [HttpGet]
        public JuegoResponse Get(int id)
        {
            var partida = registroPartidas.ObtenerPartida(id);
            var maped = mapper.Map<JuegoResponse>(partida);
            return maped;
        }

        // POST: api/Torneo
        [HttpPost]
        [Authorize]
        public void Post(string logica)
        {
            var usuario = this.HttpContext.User.Identity.Name;
            registroPartidas.RegistrarRobotAsync(new LogicaRobotDto { Usuario = usuario, Logica = logica } );
        }
    }
}
