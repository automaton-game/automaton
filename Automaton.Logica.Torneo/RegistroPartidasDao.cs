using Automaton.Logica.Dtos;
using Automaton.Logica.Registro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Automaton.Logica.Torneo
{
    public class RegistroPartidasDao : IRegistroPartidasDao
    {
        private readonly Dictionary<int, IRegistroPartidaDto> registroPartidas = new Dictionary<int, IRegistroPartidaDto>();

        public RegistroPartidasDao()
        {
            if(semaphoreSlim == null)
            {
                //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
                semaphoreSlim = new SemaphoreSlim(1);
            }
        }

        //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        private static SemaphoreSlim semaphoreSlim;

        public async Task<T> Create<T>() where T : IRegistroPartidaDto, new()
        {
            //Asynchronously wait to enter the Semaphore. If no-one has been granted access to the Semaphore, code execution will proceed, otherwise this thread waits here until the semaphore is released 
            await semaphoreSlim.WaitAsync();
            try
            {
                var dto = new T();
                var id = registroPartidas.Values.Count + 1;
                dto.IdPartida = id;
                registroPartidas[id] = dto;
                return dto;
            }
            finally
            {
                //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
                //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
                semaphoreSlim.Release();
            }
        }

        public Task Delete(int id)
        {
            registroPartidas.Remove(id);
            return Task.Run(() => { });
        }

        public Task<IRegistroPartidaDto> Get(int id)
        {
            var dto = registroPartidas[id];
            return Task.FromResult(dto);
        }

        public Task<IEnumerable<IRegistroPartidaDto>> GetAll()
        {
            var dtos = registroPartidas.Select(t => t.Value);
            return Task.FromResult(dtos);
        }

        public Task<T> Update<T>(int idPartida) where T : IRegistroPartidaDto, new()
        {
            var dto = registroPartidas[idPartida];
            return Task.FromResult((T)dto);
        }
    }
}
