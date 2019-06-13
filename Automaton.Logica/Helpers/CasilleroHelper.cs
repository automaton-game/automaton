using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Linq;

namespace Automaton.Logica.Helpers
{
    public static class CasilleroHelper
    {
        public static void AgregarRobot(this Casillero casillero, IRobot robot)
        {
            if (casillero.Robots == null)
            {
                casillero.Robots = new[] { robot };
            }
            else
            {
                casillero.Robots = casillero.Robots.Concat(new[] { robot }).ToArray();
            }
        }

        public static void QuitarRobot(this Casillero casillero, IRobot robot)
        {
            if (casillero.Robots != null)
            {
                casillero.Robots = casillero.Robots.Except(new[] { robot }).ToArray();
            }
        }
    }
}
