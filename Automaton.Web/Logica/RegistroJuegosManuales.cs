using Automaton.Logica;
using Automaton.Web.Models;
using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public class RegistroJuegosManuales : IRegistroJuegosManuales
    {
        private IDictionary<string, IJuego2v2> juegos = new Dictionary<string, IJuego2v2>();
        private IDictionary<string, IList<Tablero>> tableros = new Dictionary<string, IList<Tablero>>();

        public string Guardar(IJuego2v2 juego)
        {
            var id = juego.GetHashCode().ToString();
            juegos.Add(id, juego);
            tableros.Add(id, new List<Tablero>());
            return id;
        }

        public ICollection<Tablero> GuardarTablero(string idTablero, Tablero tablero)
        {
            tableros[idTablero].Add(tablero);
            return ObtenerTableros(idTablero);
        }

        public ICollection<Tablero> ObtenerTableros(string idTablero)
        {
            return tableros[idTablero];
        }

        public IJuego2v2 Obtener(string id)
        {
            if(!juegos.TryGetValue(id, out IJuego2v2 juego))
            {
                throw new System.Exception("No se encontro el juego " + id);
            }

            return juego;
        }
    }
}
