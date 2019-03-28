using Automaton.Contratos.Robots;
using System;

namespace Automaton.Logica
{
    public interface IFabricaRobot
    {
        IRobot ObtenerRobot(Type tipo);

        IRobot ObtenerRobot(string t);
    }
}
