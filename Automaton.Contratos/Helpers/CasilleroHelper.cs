using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Linq;

namespace Automaton.Contratos.Helpers
{
    public static class CasilleroHelper
    {
        public static bool EsUltimaColumna(this Casillero casillero)
        {
            return casillero.NroColumna == casillero.Fila.Tablero.GetMax().NroColumna;
        }

        public static bool EsUltimaFila(this Casillero casillero)
        {
            return casillero.NroFila == casillero.Fila.Tablero.GetMax().NroFila;
        }

        public static bool EsPrimeraColumna(this Casillero casillero)
        {
            return casillero.NroColumna == 0;
        }

        public static bool EsPrimeraFila(this Casillero casillero)
        {
            return casillero.NroFila == 0;
        }

        public static Casillero BuscarRelativo(this Casillero casillero, int desplazamientoHorizontal, int desplazamientoVertical)
        {
            var x = casillero.NroColumna + desplazamientoHorizontal;
            var y = casillero.NroFila + desplazamientoVertical;
            return casillero.Fila.Tablero.GetPosition(x, y);
        }

        public static Casillero BuscarRelativo(this Casillero casillero, DireccionEnum movimiento)
        {
            var x = casillero.NroColumna;
            var y = casillero.NroFila;

            switch (movimiento)
            {
                case DireccionEnum.Arriba:
                    y--;
                    break;
                case DireccionEnum.Abajo:
                    y++;
                    break;
                case DireccionEnum.Izquierda:
                    x--;
                    break;
                case DireccionEnum.Derecha:
                    x++;
                    break;
            }

            return casillero.Fila.Tablero.GetPosition(x, y);
        }

        public static IRobot ObtenerRobotLider(this Casillero casillero)
        {
            return casillero.Robots?.LastOrDefault();
        }

        public static bool ContieneRobot(this Casillero casillero, IRobot robot)
        {
            if (casillero.Robots != null)
            {
                return casillero.Robots.Contains(robot);
            }

            return false;
        }
    }
}
