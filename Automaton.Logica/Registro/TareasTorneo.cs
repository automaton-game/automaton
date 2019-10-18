using Automaton.Logica.Dtos;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public class TareasTorneo : ITareasTorneo
    {
        private ConcurrentQueue<ICollection<IJugadorRobotDto>> partidas = new ConcurrentQueue<ICollection<IJugadorRobotDto>>();
        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(0);

        public Task RegistrarPartidaAsync(ICollection<IJugadorRobotDto> logicaRobotDtos)
        {
            return Task.Run(() => RegistrarPartida(logicaRobotDtos));
        }

        public void RegistrarPartida(ICollection<IJugadorRobotDto> logicaRobotDtos)
        {
            partidas.Enqueue(logicaRobotDtos);
            semaphoreSlim.Release();
        }

        public async Task<ICollection<IJugadorRobotDto>> ObtenerLogicas(CancellationToken cancellationToken)
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
