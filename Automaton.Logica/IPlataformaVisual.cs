using Automaton.Contratos.Entorno;

namespace Automaton.Logica
{
    public interface IPlataformaVisual
    {
        void Dibujar(Tablero tablero);

        void Consola(string msg);
    }
}