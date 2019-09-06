using Automaton.Logica;
using Automaton.Logica.Dtos.Model;
using Automaton.Web.Models;
using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public interface IRegistroJuegosManuales
    {
        string Guardar(IJuego2v2 juego);
        IJuego2v2 Obtener(string id);
        ICollection<TableroModel> GuardarTablero(string idTablero, TableroModel tablero);

        ICollection<TableroModel> ObtenerTableros(string idTablero);
    }
}