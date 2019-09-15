using Automaton.Logica.Dtos.Model.Torneo;
using System.Threading.Tasks;

namespace Automaton.Web.Hubs
{
    public interface IRegistroNotificadorHub
    {
        Task NotificarUltimasPartidas(PartidosTorneoModel valor);
    }
}
