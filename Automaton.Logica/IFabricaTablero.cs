using Automaton.Logica.Dtos;

namespace Automaton.Logica
{
    public interface IFabricaTablero
    {
        TableroLogico Crear();

        TableroLogico Clone(TableroLogico tablero);
    }
}