using Automaton.Logica.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public interface IRegistroPartidasDao
    {
        Task<IRegistroPartidaDto> Get(int id);

        Task<IEnumerable<IRegistroPartidaDto>> GetAll();

        Task<T> Create<T>() where T : IRegistroPartidaDto, new();

        Task Update(IRegistroPartidaDto dto);

        Task Delete(int id);
    }
}
