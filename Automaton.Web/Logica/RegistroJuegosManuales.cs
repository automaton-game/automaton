using Automaton.Logica;
using Automaton.Logica.Dtos.Model;
using System.Collections.Generic;

namespace Automaton.Web.Logica
{
    public class RegistroJuegosManuales : IRegistroJuegosManuales
    {
        private IDictionary<string, IJuego2v2> juegos = new Dictionary<string, IJuego2v2>();
        private IDictionary<string, IList<TableroModel>> tableros = new Dictionary<string, IList<TableroModel>>();

        public string Guardar(IJuego2v2 juego)
        {
            var id = juego.GetHashCode().ToString();
            juegos.Add(id, juego);
            tableros.Add(id, new List<TableroModel>());
            return id;
        }

        public ICollection<TableroModel> GuardarTablero(string idTablero, TableroModel tablero)
        {
            tableros[idTablero].Add(tablero);
            return ObtenerTableros(idTablero);
        }

        public ICollection<TableroModel> ObtenerTableros(string idTablero)
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
