using AutoMapper;
using Automaton.Logica;
using Automaton.Logica.Registro;
using Automaton.Web.Models;
using Automaton.Web.Models.Torneo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        [HttpGet("[action]")]
        public async Task<PartidosTorneoModel> Get()
        {
            var partidas = await registroPartidas.ObtenerUltimasPartidasAsync();
            var maped = mapper.Map<PartidosTorneoModel>(partidas);
            return maped;
        }

        // GET: api/Torneo/{id}
        [HttpGet]
        public async Task<JuegoResponse> Get(int id)
        {
            var partida = await registroPartidas.ObtenerPartidaAsync(id);
            var maped = mapper.Map<JuegoResponse>(partida);
            return maped;
        }

        // POST: api/Torneo
        [HttpPost]
        [Authorize]
        public async Task Post(string logica)
        {
            var usuario = this.HttpContext.User.Identity.Name;
            await registroPartidas.RegistrarRobotAsync(new LogicaRobotDto { Usuario = usuario, Logica = logica } );
        }
    }
}
