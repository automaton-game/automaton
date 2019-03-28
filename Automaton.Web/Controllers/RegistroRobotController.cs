using System.Collections.Generic;
using System.Linq;
using Automaton.Web.Logica;
using Automaton.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Automaton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroRobotController : ControllerBase
    {
        private readonly IRegistroRobots registroRobots;

        public RegistroRobotController(IRegistroRobots registroRobots)
        {
            this.registroRobots = registroRobots;
        }

        [HttpGet("[action]")]
        public IDictionary<string, int> Get()
        {
            return registroRobots.ObtenerResumen();
        }
    }
}