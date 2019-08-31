using AutoMapper;
using Automaton.Logica.Registro;
using Automaton.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidaController : ControllerBase
    {
        private readonly IRegistroPartidas registroPartidas;
        private readonly IMapper mapper;

        public PartidaController(
            IRegistroPartidas registroPartidas,
            IMapper mapper)
        {
            this.registroPartidas = registroPartidas;
            this.mapper = mapper;
        }

        // GET: api/Torneo/{id}
        [HttpGet("[action]/{id}")]
        public async Task<JuegoResponse> Get(int id)
        {
            var partida = await registroPartidas.ObtenerPartidaAsync(id);
            var maped = mapper.Map<JuegoResponse>(partida);
            return maped;
        }
    }
}
