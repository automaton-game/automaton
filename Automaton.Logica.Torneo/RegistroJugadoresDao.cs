using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public class RegistroJugadoresDao : IRegistroJugadoresDao
    {
        private readonly Dictionary<string, RegistroJugadorDto> registroJugadores = new Dictionary<string, RegistroJugadorDto>();
        public Task<RegistroJugadorDto> Get(string id)
        {
            //El Id es el Nombre de Usuario
            var jugador = registroJugadores[id];
            return Task.FromResult<RegistroJugadorDto>(jugador);
        }

        public Task<IEnumerable<RegistroJugadorDto>> GetAll()
        {
            var jugadores = registroJugadores.Select(s => s.Value);
            return Task.FromResult(jugadores);
        }

        public async Task<bool> Insert(LogicaRobotDto dto)
        {
            bool jugadorInsertado = false;

            //Creo jugador con datos que vienen por parametro
            RegistroJugadorDto jugador = new RegistroJugadorDto();
            jugador.Logica = dto.Logica;
            jugador.Usuario = dto.Usuario;

            //Verifico si el jugador ya existe. Si existe lo actualizo sino lo actualizo
            if (registroJugadores.ContainsKey(jugador.Usuario))
            {
                registroJugadores[jugador.Usuario].Logica = jugador.Logica;
            }
            else
            {
                registroJugadores.Add(jugador.Usuario, jugador);
            }

            jugadorInsertado = true;
            return await Task.FromResult<bool>(jugadorInsertado);
        }

    }
}
