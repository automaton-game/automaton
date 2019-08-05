using Automaton.Web.Models;

namespace Automaton.Web.Logica
{
    public interface IDirectorJuego
    {
        JuegoResponse Iniciar(string logicaRobot, string usuario);
    }
}