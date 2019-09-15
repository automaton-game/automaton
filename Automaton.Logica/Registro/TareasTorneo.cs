using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public class TareasTorneo : ITareasTorneo
    {
        private ConcurrentQueue<ICollection<LogicaRobotDto>> partidas = new ConcurrentQueue<ICollection<LogicaRobotDto>>();
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);

        public Task RegistrarPartidaAsync(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            return Task.Run(() => RegistrarPartida(logicaRobotDtos));
        }

        public void RegistrarPartida(ICollection<LogicaRobotDto> logicaRobotDtos)
        {
            partidas.Enqueue(logicaRobotDtos);
            semaphoreSlim.Release();
        }

        public async Task<ICollection<LogicaRobotDto>> ObtenerLogicas(CancellationToken cancellationToken)
        {
            await semaphoreSlim.WaitAsync();

            if (cancellationToken.IsCancellationRequested)
            {
                semaphoreSlim.Release();
                return null;
            }

            partidas.TryDequeue(out var logicas);
            return logicas;
        }
    }
}
