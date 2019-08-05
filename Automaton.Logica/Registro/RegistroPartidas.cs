using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Automaton.Logica.Registro
{
    public class RegistroPartidas : IRegistroPartidas
    {
        private readonly IDirectorTorneo directorTorneo;
        private readonly IRegistroNotificador notificador;

        public RegistroPartidas(IDirectorTorneo directorTorneo,
            IRegistroNotificador notificador)
        {
            this.directorTorneo = directorTorneo;
            this.notificador = notificador;
        }

        public PartidaResueltaDto ObtenerPartida(int idPartida)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRegistroPartidaDto> ObtenerUltimasPartidas(string usuario)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IRegistroPartidaDto> ObtenerUltimasPartidas()
        {
            throw new System.NotImplementedException();
        }

        public void RegistrarRobot(LogicaRobotDto logicaRobotDto)
        {
            var partidaEnCurso = directorTorneo.Iniciar(null);
            partidaEnCurso.ContinueWith(PartidaFinalizada);

            throw new System.NotImplementedException();
        }

        private async Task PartidaFinalizada(Task<PartidaResueltaDto> partidaResueltaDto)
        {
            Console.WriteLine("Fin " + partidaResueltaDto.Result.Ganador);
            await notificador.NotificarUltimasPartidas(this.ObtenerUltimasPartidas());
        }
    }
}
