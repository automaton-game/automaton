using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo.Tests
{
    [TestClass]
    public class RegistroPartidasDaoTest
    {
        RegistroPartidasDao registro;
        IRegistroPartidaDto registroPartidaDto;
        IDictionary<string, RegistroPartidaEnCursoDto> partidasEnCurso;

        [TestInitialize]
        public void TestInitialize()
        {
            registro = new RegistroPartidasDao();
            partidasEnCurso = new Dictionary<string, RegistroPartidaEnCursoDto>();
        }

        [TestMethod]
        public async Task CrearPartida()
        {
            var partida = await registro.Create<RegistroPartidaEnCursoDto>();

        }
        [TestMethod]
        public async Task CrearPartidas()
        {
            var cantidadPartidasACrear = 100;

            // Almaceno la partidas creadas
            var partidas = new List<RegistroPartidaEnCursoDto>();

            // Invoco simultaneamente varias veces la tarea de creacion de partidas. Almaceno las tareas en una lista.
            var tareasRegistro = new Dictionary<Task, RegistroPartidaEnCursoDto>();
            var rnd = new Random();
            for (var i = 0; i < cantidadPartidasACrear; i++)
            {
                // Creo una remota inicial 
                var delay = Task.Delay(cantidadPartidasACrear-i);
                var crearPartida = delay.ContinueWith(t => {
                    var taskDto = registro.Create<RegistroPartidaEnCursoDto>();
                    taskDto.Wait();
                    return taskDto.Result;
                });
                var registrarTarea = crearPartida.ContinueWith(t => {
                    t.Wait();
                    tareasRegistro[t] = (t.Result);
                    return;
                    });
                tareasRegistro[crearPartida] = null;
            }

            // Espero a que finalicen las tareas
            await Task.WhenAll(tareasRegistro.Keys.ToArray());

            // Verifico que se hayan generado todos los partidos
            var cantPartidos = tareasRegistro.Values.Where(c => c == null).Count();
            Assert.AreEqual(0, cantPartidos);

            // Verifico que tengan IDs distintos
            var cantPartidasCreadas = tareasRegistro.Values.GroupBy(g => g.IdPartida).Count();
            Assert.AreEqual(cantidadPartidasACrear, cantPartidasCreadas);


        }

        [TestMethod]
        public async Task BorrarPartida()
        {
            var a = await registro.Create<MiClase>();
            var b = await registro.Create<MiClase>();
            var c = await registro.Create<MiClase>();
            
            var asd = registro.Delete(1);


        }

        [TestMethod]
        public async Task GetPartida()
        {
            var a = await registro.Create<MiClase>();
            var b = await registro.Create<MiClase>();
            var c = await registro.Create<MiClase>();

            var asd = registro.Get(2);



        }

        [TestMethod]
        public async Task GetAllPartida()
        {
            var a = await registro.Create<MiClase>();
            var b = await registro.Create<MiClase>();
            var c = await registro.Create<MiClase>();

            var asd = registro.GetAll();
            
        }

        [TestMethod]
        public async Task ActualizarPartida()
        {
            var a = await registro.Create<MiClase>();
            var b = await registro.Create<MiClase>();
            var c = await registro.Create<MiClase>();

            var asd = registro.Update<MiClase>(2);


        }

       



    }
    public class MiClase : IRegistroPartidaDto
    {


        public int IdPartida { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public short PorcentajeProgreso { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Ganador { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IList<string> Jugadores { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
