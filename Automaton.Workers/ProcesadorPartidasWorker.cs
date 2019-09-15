using Automaton.Logica.Torneo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Workers
{
    public class ProcesadorPartidasWorker : BackgroundService
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        private readonly ILogger<ProcesadorPartidasWorker> logger;

        public ProcesadorPartidasWorker(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<ProcesadorPartidasWorker> logger)
        {
            this.serviceScopeFactory = serviceScopeFactory;
            this.logger = logger;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = serviceScopeFactory.CreateScope())
                {
                    var procesadorPartidas = scope.ServiceProvider.GetRequiredService<IProcesadorPartidas>();
                    try
                    {
                        await procesadorPartidas.ProcesarAsync(stoppingToken);
                    }
                    catch (System.Exception ex)
                    {
                        logger.LogError(ex, "Error al procesar logicas.");
                    }
                }
            }
        }

        
    }
}
