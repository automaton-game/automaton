using Automaton.Logica.Registro;

namespace Automaton.Logica
{
    public interface IDirectorJuego
    {
        PartidaResueltaDto Iniciar(string logicaRobot, string usuario);
    }
}