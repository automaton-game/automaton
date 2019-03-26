using System;
using AutomataNETjuegos.Contratos.Robots;

namespace AutomataNETjuegos.Logica
{
    public class FabricaRobotType
    {
        public IRobot ObtenerRobot(Type tipo)
        {
            return (IRobot)Activator.CreateInstance(tipo);
        }
    }
}
