using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public interface IProcesadorPartidas
    {
        Task ProcesarAsync(CancellationToken cancellationToken);
    }
}