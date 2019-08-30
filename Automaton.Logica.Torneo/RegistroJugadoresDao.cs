using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public class RegistroJugadoresDao : IRegistroJugadoresDao
    {
        public Task<RegistroJugadorDto> Get(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegistroJugadorDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(LogicaRobotDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
