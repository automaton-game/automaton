using System;
using System.Threading.Tasks;
using Automaton.Contratos.Robots;
using Automaton.Logica;

namespace Automaton.Compilador
{
    public class FabricaRobotAsync : IFabricaRobotAsync
    {
        private readonly IFabricaRobot fabricaRobot;

        public FabricaRobotAsync(IFabricaRobot fabricaRobot)
        {
            this.fabricaRobot = fabricaRobot;
        }

        public async Task<IRobot> ObtenerRobotAsync(Type tipo)
        {
            return await Task.FromResult(fabricaRobot.ObtenerRobot(tipo));
        }

        public async Task<IRobot> ObtenerRobotAsync(string tipo)
        {
            return await Task.FromResult(fabricaRobot.ObtenerRobot(tipo));
        }
    }
}
