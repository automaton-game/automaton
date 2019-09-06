using System.Linq;
using AutoMapper;
using Automaton.Contratos.Entorno;
using Automaton.Logica;
using Automaton.Logica.Dtos;

namespace Automaton.Web.Logica
{
    public class FabricaTablero : IFabricaTablero
    {
        private const int filas = 5;
        private const int columnas = 5;
        private readonly IMapper mapper;

        public FabricaTablero(IMapper mapper)
        {
            this.mapper = mapper;
        }


        public Tablero Crear()
        {
            var tablero = new Tablero();
            tablero.Filas = Enumerable.Range(1, filas).Select(f => {
                var fila = new FilaTablero
                {
                    NroFila = f,
                    Tablero = tablero
                };

                fila.Casilleros = Enumerable.Range(1, columnas).Select(c => new Casillero { Fila = fila, NroFila = f, NroColumna = c }).ToArray();
                return fila;
            }).ToArray();
            return tablero;
        }

        public T Clone<T>(T tablero) where T : Tablero, new()
        {
            var rta = new T();
            mapper.Map<Tablero, Tablero>(tablero, rta);
            return rta;
        }
    }
}
