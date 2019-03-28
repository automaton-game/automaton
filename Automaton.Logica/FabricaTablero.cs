using System.Linq;
using Automaton.Contratos.Entorno;

namespace Automaton.Logica
{
    public class FabricaTablero : IFabricaTablero
    {
        private const int filas = 5;
        private const int columnas = 5;

        public Tablero Crear()
        {
            var tablero = new Tablero();
            tablero.Filas = Enumerable.Range(1, filas).Select(f => {
                var fila = new FilaTablero
                {
                    NroFila = f,
                    Tablero = tablero
                };

                fila.Casilleros = Enumerable.Range(1, columnas).Select(c => new Casillero { Fila = fila, NroFila = f, NroColumna = c }).ToArray();
                return fila;
            }).ToArray();
            return tablero;
        }
    }
}
