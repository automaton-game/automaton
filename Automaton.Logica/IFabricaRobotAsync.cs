using Automaton.Contratos.Robots;
using System;
using System.Threading.Tasks;

namespace Automaton.Logica
{
    public interface IFabricaRobotAsync
    {
        Task<IRobot> ObtenerRobotAsync(Type tipo);

        Task<IRobot> ObtenerRobotAsync(string t);
    }
}
