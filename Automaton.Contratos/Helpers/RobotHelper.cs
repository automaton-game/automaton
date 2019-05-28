using System.Collections.Generic;
using System.Linq;
using Automaton.Contratos.Entorno;
using Automaton.Contratos.Robots;

namespace Automaton.Contratos.Helpers
{
    /// <summary>
    /// Helper Robot
    /// </summary>
    public static class RobotHelper
    {
        /// <summary>
        /// Obtener la posicion del objeto
        /// </summary>
        /// <param name="robot">Objeto Robot</param>
        /// <returns></returns>
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

        public static IEnumerable<IRobot> GetOtherRobots(this IRobot robot)
        {
            return robot.Tablero.GetCasilleros().SelectMany(c => c.Robots).Where(c => c != null && c != robot);
        }

        public static bool EstoyUltimaColumna(this IRobot robot)
        {
            var pos = robot.GetPosition();
            return pos.NroColumna == pos.Fila.Tablero.GetMax().NroColumna;
        }

        public static bool EstoyUltimaFila(this IRobot robot)
        {
            var casillero = robot.GetPosition();
            return casillero.NroFila == casillero.Fila.Tablero.GetMax().NroFila;
        }

        public static bool EstoyPrimeraColumna(this IRobot robot)
        {
            return robot.GetPosition().NroColumna == 0;
        }

        public static bool EsPrimeraFila(this IRobot robot)
        {
            return robot.GetPosition().NroFila == 0;
        }

        public static Casillero BuscarRelativo(this IRobot robot, int desplazamientoHorizontal, int desplazamientoVertical)
        {
            var casillero = robot.GetPosition();
            var x = casillero.NroColumna + desplazamientoHorizontal;
            var y = casillero.NroFila + desplazamientoVertical;
            return casillero.Fila.Tablero.GetPosition(x, y);
        }

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
