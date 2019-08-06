using Automaton.Contratos.Entorno;
using System;
using System.Collections.Generic;

namespace Automaton.Logica.Dtos
{
    public class TableroLogico : Tablero 
    {
        private readonly IDictionary<Type, IAdicionalTablero> adicionales = new Dictionary<Type, IAdicionalTablero>();

        public T Set<T>() where T : IAdicionalTablero, new()
        {
            if(adicionales.TryGetValue(typeof(T), out var adicional))
            {
                return (T)adicional;
            }
            else
            {
                var adicionalNuevo = new T();
                adicionales[typeof(T)] = adicionalNuevo;
                return adicionalNuevo;
            }
        }
    }
}
