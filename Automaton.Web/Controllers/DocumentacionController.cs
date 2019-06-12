using Automaton.Contratos.Robots;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tools.Documentador;
using Tools.Documentador.Dtos;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentacionController : ControllerBase
    {
        private readonly IAssemblyReader assemblyReader;

        public DocumentacionController(IAssemblyReader assemblyReader)
        {
            this.assemblyReader = assemblyReader;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            using (var values = assemblyReader.ReadAssembly(typeof(IRobot)))
            {
                var lista = values.Where(Filtro).ToArray();
                return Ok(new { lista });
            }
        }
        
        private bool Filtro(IClassInfo classInfo)
        {
            var lista = new[]
            {
                "Automaton.Contratos.Helpers",
                "Automaton.Contratos.Entorno",
            };

            return lista.Any(ss => classInfo.Type.Substring(0, ss.Length) == ss);
        }
    }
}