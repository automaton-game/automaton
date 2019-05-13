using Automaton.Contratos.Entorno;

namespace Automaton.Logica
{
    public interface IFabricaTablero
    {
        Tablero Crear();

        Tablero Clone(Tablero tablero);
    }
}