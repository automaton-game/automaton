using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IFabricaTablero
    {
        Tablero Crear();

        T Clone<T>(T tablero) where T : Tablero, new();
    }
}