using Automaton.Contratos.Robots;

namespace Automaton.Logica.Robots
{
    public class RobotManual : ARobot
    {
        public AccionRobotDto AccionRobot { get; set; }

        public override AccionRobotDto GetAccionRobot(IConsole console)
        {
            return AccionRobot;
        }
    }
}
