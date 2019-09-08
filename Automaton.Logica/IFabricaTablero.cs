using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IFabricaTablero
    {
        TableroDto Crear();

        T Clone<T>(TableroDto tablero) where T : Tablero, new();
    }
}