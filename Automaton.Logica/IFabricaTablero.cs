using Automaton.Contratos.Entorno;
using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IFabricaTablero
    {
        TableroLogico Crear();

        Tablero Clone(Tablero tablero);
    }
}