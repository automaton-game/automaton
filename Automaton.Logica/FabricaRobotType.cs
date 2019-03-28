using System;
using Automaton.Contratos.Robots;

namespace Automaton.Logica
{
    public class FabricaRobotType
    {
        public IRobot ObtenerRobot(Type tipo)
        {
            try
            {
                return (IRobot)Activator.CreateInstance(tipo);
            }
            catch(Exception ex)
            {
                throw new Exception($"Hubo un problema al instanciar {tipo.Name}", ex);
            }
        }

        public IRobot ObtenerRobot<T>(Type tipo)
        {
            try
            {
                return (IRobot)Activator.CreateInstance(tipo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Hubo un problema al instanciar {tipo.Name}", ex);
            }
        }
    }
}
