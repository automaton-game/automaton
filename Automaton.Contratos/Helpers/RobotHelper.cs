using System.Collections.Generic;
using System.Linq;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;

namespace Automaton.Contratos.Helpers
{
    /// <summary>
    /// Atajos para el robot. 
    /// </summary>
    /// <category>CAT1</category>
    public static class RobotHelper
    {
        /// <summary>
        /// Obtener la posicion del objeto
        /// </summary>
        /// <param name="robot">Objeto Robot</param>
        /// <returns></returns>
        /// <categories>
        ///     <category>FIRST</category>
        /// </categories>
        public static Casillero GetPosition(this IRobot robot)
        {
            var tablero = robot.Tablero;
            return tablero.Filas.SelectMany(f => f.Casilleros).First(c => c.ContieneRobot(robot));
        }

        /// <summary>
        /// Devuelve el casillero correspondiente a las coordenadas indicadas
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Casillero GetPosition(this IRobot robot, int x, int y)
        {
            var tablero = robot.Tablero;
            var fila = tablero.Filas.FirstOrDefault(f => f.NroFila == y);
            if (fila == null)
            {
                return null;
            }

            return fila.Casilleros.FirstOrDefault(c => c.NroColumna == x);
        }

        /// <summary>
        /// Devuelve un listado de robots enemigos
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static IEnumerable<IRobot> GetOtherRobots(this IRobot robot)
        {
            return robot.Tablero.GetCasilleros().SelectMany(c => c.Robots).Where(c => c != null && c != robot);
        }

        /// <summary>
        /// Verifica si el robot se encuentra en la ultima columna
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static bool EstoyUltimaColumna(this IRobot robot)
        {
            var pos = robot.GetPosition();
            return pos.NroColumna == pos.Fila.Tablero.GetMax().NroColumna;
        }

        /// <summary>
        /// Verifica si el robot se encuentra en la ultima fila
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static bool EstoyUltimaFila(this IRobot robot)
        {
            var casillero = robot.GetPosition();
            return casillero.NroFila == casillero.Fila.Tablero.GetMax().NroFila;
        }

        /// <summary>
        /// Verifica si el robot se encuentra en la primera columna
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static bool EstoyPrimeraColumna(this IRobot robot)
        {
            return robot.GetPosition().NroColumna == 0;
        }

        /// <summary>
        /// Verifica si el robot se encuentra en la primera fila
        /// </summary>
        /// <param name="robot"></param>
        /// <returns></returns>
        public static bool EsPrimeraFila(this IRobot robot)
        {
            return robot.GetPosition().NroFila == 0;
        }

        /// <summary>
        /// Devuelve el casillero correspondiente al desplazamiento indicado para el robot en su posicion actual.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="desplazamientoHorizontal"></param>
        /// <param name="desplazamientoVertical"></param>
        /// <returns></returns>
        public static Casillero BuscarRelativo(this IRobot robot, int desplazamientoHorizontal, int desplazamientoVertical)
        {
            var casillero = robot.GetPosition();
            var x = casillero.NroColumna + desplazamientoHorizontal;
            var y = casillero.NroFila + desplazamientoVertical;
            return casillero.Fila.Tablero.GetPosition(x, y);
        }

        /// <summary>
        /// Devuelve el casillero de la ubicacion adyancente indicada, segun la posicion actual del robot.
        /// </summary>
        /// <param name="robot"></param>
        /// <param name="movimiento"></param>
        /// <returns></returns>
        public static Casillero BuscarRelativo(this IRobot robot, DireccionEnum movimiento)
        {
            var casillero = robot.GetPosition();
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
    }
}
