using Automaton.Contratos.Robots;

namespace Automaton.Logica.Dtos
{
    public class JugadorRobotDto : LogicaRobotDto, IJugadorRobotDto
    {
        public IRobot Robot { get; set; }
    }
}
