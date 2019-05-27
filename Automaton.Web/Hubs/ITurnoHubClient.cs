using Automaton.Web.Models;
using System.Threading.Tasks;

namespace Automaton.Web.Hubs
{
    public interface ITurnoHubClient
    {
        Task FinTurno(FinTurnoDto dto);
    }

    public class FinTurnoDto
    {
        public JuegoManualResponse Juego { get; set; }

        public string HashRobot { get; set; }
    }
}
