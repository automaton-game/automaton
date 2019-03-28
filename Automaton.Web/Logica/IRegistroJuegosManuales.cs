using Automaton.Logica;
using Automaton.Web.Models;
using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public interface IRegistroJuegosManuales
    {
        string Guardar(IJuego2v2 juego);
        IJuego2v2 Obtener(string id);
        ICollection<Tablero> GuardarTablero(string idTablero, Tablero tablero);

        ICollection<Tablero> ObtenerTableros(string idTablero);
    }
}