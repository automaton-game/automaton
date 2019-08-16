using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using Automaton.Web.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Web.Logica
{
    public class RegistroNotificador : IRegistroNotificador
    {
        private readonly IHubContext<RegistroNotificadorHub, IRegistroNotificador> turnoHubClient;

        public RegistroNotificador(
            IHubContext<RegistroNotificadorHub, IRegistroNotificador> turnoHubClient)
        {
            this.turnoHubClient = turnoHubClient;
        }

        public async Task NotificarUltimasPartidas(IEnumerable<IRegistroPartidaDto> valor)
        {
            await this.turnoHubClient.Clients.All.NotificarUltimasPartidas(valor);
        }
    }
}
