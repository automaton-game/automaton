using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Linq;

namespace Automaton.Contratos.Helpers
{
    /// <summary>
    /// Contiene atajos para el elemento Casillero
    /// </summary>
    public static class CasilleroHelper
    {
        /// <summary>
        /// Verifica si el casillero se encuentra en la ultima columna del tablero
        /// </summary>
        /// <param name="casillero"></param>
        /// <returns></returns>
        public static bool EsUltimaColumna(this Casillero casillero)
        {
            return casillero.NroColumna == casillero.Fila.Tablero.GetMax().NroColumna;
        }

        /// <summary>
        /// Verifica si el casillero se encuentra en la ultima fila del tablero
        /// </summary>
        /// <param name="casillero"></param>
        /// <returns></returns>
        public static bool EsUltimaFila(this Casillero casillero)
        {
            return casillero.NroFila == casillero.Fila.Tablero.GetMax().NroFila;
        }

        /// <summary>
        /// Verifica si el casillero se encuentra en la primera columna del tablero
        /// </summary>
        /// <param name="casillero"></param>
        /// <returns></returns>
        public static bool EsPrimeraColumna(this Casillero casillero)
        {
            return casillero.NroColumna == 0;
        }

        /// <summary>
        /// Verifica si el casillero se encuentra en la primera fila del tablero
        /// </summary>
        /// <param name="casillero"></param>
        /// <returns></returns>
        public static bool EsPrimeraFila(this Casillero casillero)
        {
            return casillero.NroFila == 0;
        }

        /// <summary>
        /// Devuelve un casillero relativo al actual, indicando la cantidad de dezplamiento vertical y horizontal
        /// </summary>
        /// <param name="casillero"></param>
        /// <param name="desplazamientoHorizontal"></param>
        /// <param name="desplazamientoVertical"></param>
        /// <returns></returns>
        public static Casillero BuscarRelativo(this Casillero casillero, int desplazamientoHorizontal, int desplazamientoVertical)
        {
            var x = casillero.NroColumna + desplazamientoHorizontal;
            var y = casillero.NroFila + desplazamientoVertical;
            return casillero.Fila.Tablero.GetPosition(x, y);
        }

        /// <summary>
        /// Devuelve un casillero adyacente al actual
        /// </summary>
        /// <param name="casillero"></param>
        /// <param name="movimiento"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Devuelve el ultimo robot que entro en el casillero actual
        /// </summary>
        /// <param name="casillero"></param>
        /// <returns></returns>
        public static IRobot ObtenerRobotLider(this Casillero casillero)
        {
            return casillero.Robots?.LastOrDefault();
        }

        /// <summary>
        /// Verifica si el casillero actual contiene al robot indicado
        /// </summary>
        /// <param name="casillero"></param>
        /// <param name="robot"></param>
        /// <returns></returns>
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
