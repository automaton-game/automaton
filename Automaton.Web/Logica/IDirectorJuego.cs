using Automaton.Logica.Dtos.Model;

namespace Automaton.Web.Logica
{
    public interface IDirectorJuego
    {
        JuegoResponse Iniciar(string logicaRobot, string usuario);
    }
}