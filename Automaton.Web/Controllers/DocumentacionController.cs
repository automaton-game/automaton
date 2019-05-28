using Automaton.Contratos.Helpers;
using Microsoft.AspNetCore.Mvc;
using Tools.Documentador;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentacionController : ControllerBase
    {

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            var lectorXml = new LectorXml();
            var f = lectorXml.Leer(typeof(RobotHelper), "GetPosition");
            return Ok(new { Obs = f });
        } 
    }
}