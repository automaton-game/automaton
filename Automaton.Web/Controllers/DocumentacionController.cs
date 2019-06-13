using Automaton.Contratos.Robots;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tools.Documentador.TypeReaders;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentacionController : ControllerBase
    {
        private readonly INameSpaceGrouping nameSpaceGrouping;

        public DocumentacionController(INameSpaceGrouping nameSpaceGrouping)
        {
            this.nameSpaceGrouping = nameSpaceGrouping;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            using (var values = nameSpaceGrouping.ReadNamespaces(typeof(IRobot)))
            {
                var lista = values.ToArray();
                return Ok(new { lista });
            }
        }
    }
}