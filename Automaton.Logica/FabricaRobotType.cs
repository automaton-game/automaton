using System;
using Automaton.Contratos.Robots;

namespace Automaton.Logica
{
    public class FabricaRobotType
    {
        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo);
        }
    }
}
