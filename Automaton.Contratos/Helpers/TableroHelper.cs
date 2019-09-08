using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Contratos.Helpers
{
    /// <summary>
    /// Atajos para el tablero
    /// </summary>
    public static class TableroHelper
    {
        /// <summary>
        /// Devuelve el casillero que contiene el robot indicado
        /// </summary>
        /// <param name="tablero"></param>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static ICasillero GetPosition(this ITablero tablero, IRobot robot)
        {
            return tablero.Filas.SelectMany(f => f.Casilleros).First(c => c.ContieneRobot(robot));
        }

        /// <summary>
        /// Devuelve el casillero correspondiente a las coordenadas recibidas
        /// </summary>
        /// <param name="tablero"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static ICasillero GetPosition(this ITablero tablero, int x, int y)
        {
            var fila = tablero.Filas.FirstOrDefault(f => f.NroFila == y);
            if (fila == null)
            {
                return null;
            }

            return fila.Casilleros.FirstOrDefault(c => c.NroColumna == x);
        }

        /// <summary>
        /// Devuelve el casillero correspondiente a la fila y columna inferiores.
        /// </summary>
        /// <param name="tablero"></param>
        /// <returns></returns>
        public static ICasillero GetMax(this ITablero tablero)
        {
            return tablero.Filas.Last().Casilleros.Last();
        }

        /// <summary>
        /// Devuelve un listado con todos los casilleros que contiene el tablero.
        /// </summary>
        /// <param name="tablero"></param>
        /// <returns></returns>
        public static IEnumerable<ICasillero> GetCasilleros(this ITablero tablero)
        {
            return tablero.Filas.SelectMany(fila => fila.Casilleros);
        }
    }
}
