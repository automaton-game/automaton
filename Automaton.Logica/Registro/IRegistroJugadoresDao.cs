using Automaton.Logica.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public interface IRegistroJugadoresDao
    {
        Task<RegistroJugadorDto> Get(string id);

        Task<IEnumerable<RegistroJugadorDto>> GetAll();

        Task<bool> Insert(JugadorRobotDto dto);
    }
}
