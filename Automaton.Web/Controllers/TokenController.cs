using Automaton.Web.Logica;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Automaton.Web.Controllers
{

    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        [HttpPost("[action]")]
        public IActionResult Create(LoginInputModel inputModel)
        {
            var builder = new JwtTokenBuilder();
            var token = builder.GenerateTokenJwt(inputModel.UserName);
            return Ok(new { token });
        }
    }
}
