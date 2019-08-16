using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidasDao
    {
        Task<IRegistroPartidaDto> Get(int id);

        Task<ICollection<IRegistroPartidaDto>> GetAll();

        Task Save(IRegistroPartidaDto dto);
    }
}
