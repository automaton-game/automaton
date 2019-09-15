using AutoMapper;
using Automaton.Logica.Dtos;
using Automaton.Logica.Dtos.Model.Torneo;
using Automaton.Logica.Registro;
using Automaton.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Web.Logica
{
    public class RegistroNotificador : IRegistroNotificador
    {
        private readonly IHubContext<RegistroNotificadorHub, IRegistroNotificadorHub> turnoHubClient;
        private readonly IMapper mapper;

        public RegistroNotificador(
            IHubContext<RegistroNotificadorHub, IRegistroNotificadorHub> turnoHubClient,
            IMapper mapper)
        {
            this.turnoHubClient = turnoHubClient;
            this.mapper = mapper;
        }

        public async Task NotificarUltimasPartidas(IEnumerable<IRegistroPartidaDto> valor)
        {
            var maped = mapper.Map<PartidosTorneoModel>(valor);
            await this.turnoHubClient.Clients.All.NotificarUltimasPartidas(maped);
        }
    }
}
