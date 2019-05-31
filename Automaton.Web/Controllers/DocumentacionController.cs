using Automaton.Contratos.Robots;
using Microsoft.AspNetCore.Mvc;
using Tools.Documentador;

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
            var values = assemblyReader.ReadAssembly(typeof(IRobot));
            return Ok(values);
        } 
    }
}