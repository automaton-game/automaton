using System.Collections.Generic;
using System.Linq;
using Automaton.Web.Logica;
using Automaton.Web.Models;
using Microsoft.AspNetCore.Authorization;
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
        public IActionResult Get()
        {
            var diccionario = registroRobots.ObtenerResumen();
            var rta = diccionario.Select(v => new { v.Key, v.Value }).ToArray();
            return Ok(rta);
        }

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult BorrarTodo()
        {
            var usuario = this.HttpContext.User.Identity.Name;
            if(usuario != "12HN12")
            {
                return Unauthorized();
            }

            registroRobots.BorrarTodo();
            return Ok();
        }
    }
}