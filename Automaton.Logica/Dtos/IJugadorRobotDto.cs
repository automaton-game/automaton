using Automaton.Contratos.Robots;

namespace Automaton.Logica.Dtos
{
    public interface IJugadorRobotDto
    {
        IRobot Robot { get; set; }

        string Usuario { get; set; }
    }
}
