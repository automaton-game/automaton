using System.Threading.Tasks;

namespace Automaton.Web.Hubs
{
    public interface ITurnoHubClient
    {
        Task FinTurno(string idPartida, string hashRobot);
    }
}
