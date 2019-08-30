using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public class RegistroPartidas : IRegistroPartidas
    {
        private readonly ITareasTorneo tareasTorneo;
        private readonly IRegistroPartidasDao registroPartidasDao;
        private readonly IRegistroJugadoresDao registroJugadoresDao;
   

        public RegistroPartidas(
            ITareasTorneo tareasTorneo,
            IRegistroPartidasDao registroPartidasDao,
            IRegistroJugadoresDao registroJugadoresDao)
        {
            this.tareasTorneo = tareasTorneo;
            this.registroPartidasDao = registroPartidasDao;
            this.registroJugadoresDao = registroJugadoresDao;
        }

     

        public async Task<PartidaResueltaDto> ObtenerPartidaAsync(int idPartida)
        {
            var partida = await registroPartidasDao.Get(idPartida);
            return partida as PartidaResueltaDto;
        }

        public async Task<IEnumerable<IRegistroPartidaDto>> ObtenerUltimasPartidasAsync(string usuario)
        {
            var partidas = await registroPartidasDao.GetAll();
            return partidas.Where(p => p.Jugadores.Contains(usuario));
        }

        public async Task<IEnumerable<IRegistroPartidaDto>> ObtenerUltimasPartidasAsync()
        {
            var partidas = await registroPartidasDao.GetAll();
            return partidas;
        }

        public async Task RegistrarRobotAsync(LogicaRobotDto logicaRobotDto)
        {
            // Registrar usuario nuevo
            var usuarioExistente = await registroJugadoresDao.Insert(logicaRobotDto);

            // Obtengo las ultimas versiones de cada jugador
            var ultimosJugadores = await registroJugadoresDao.GetAll();

            //Busco Ultima version de logica de cada Jugador
            List<RegistroJugadorDto> JugadoresHabilitados = ultimosJugadores.ToList(); //jugadoresUnicos(ultimosJugadores.ToList());

            if (JugadoresHabilitados.Count() > 1)
            {
                //Partida ida
                CreadorPartidas(JugadoresHabilitados);
                //Invierto la lista de jugadores para crear partido de vuelta
                JugadoresHabilitados.Reverse();
                //Partida vuelta
                CreadorPartidas(JugadoresHabilitados);
            }

        }

        public  void CreadorPartidas(List<RegistroJugadorDto> jugadores)
        {
            for (int i = 0; i < jugadores.Count(); i++)
            {
                for (int j = 0; j < jugadores.Count() - 1; j++)
                {
                    LogicaRobotDto jugadorLocal = new LogicaRobotDto();
                    LogicaRobotDto jugadorVisitante = new LogicaRobotDto();

                    jugadorLocal = jugadores[i];

                    if (j + i + 1 < jugadores.Count)
                    {
                        jugadorVisitante = jugadores[j + i + 1];

                        List<LogicaRobotDto> partidaActual = new List<LogicaRobotDto>();
                        partidaActual.Add(jugadorLocal);
                        partidaActual.Add(jugadorVisitante);

                        var partida = tareasTorneo.IniciarPartida(partidaActual);
                    }
                }
            }
        }

        //public List<RegistroJugadorDto> jugadoresUnicos(List<RegistroJugadorDto> ListaJugadores)
        //{
        //    List<RegistroJugadorDto> JugadoresHabilitados = new List<RegistroJugadorDto>();

        //    for (int i = 0; i < ListaJugadores.Count; i++)
        //    {
        //        RegistroJugadorDto unico = ListaJugadores.LastOrDefault(j => j.Usuario == ListaJugadores[i].Usuario);
        //        if (!JugadoresHabilitados.Contains(unico))
        //        {
        //            JugadoresHabilitados.Add(unico);
        //        }
        //    }
        //    return JugadoresHabilitados;
        //}


    }

}
