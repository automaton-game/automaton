using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public interface IRegistroNotificador
    {
        Task NotificarUltimasPartidas(ICollection<IRegistroPartidaDto> valor);
    }
}
