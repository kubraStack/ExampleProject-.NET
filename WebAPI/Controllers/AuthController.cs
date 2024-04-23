using Business.Features.Auth.Command.Login;
using Business.Features.Auth.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase
    {
        private readonly IMediator _mediator;

        public Authcontroller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            await _mediator.Send(registerCommand);
            return Created();
        }


        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
           var response =  await _mediator.Send(loginCommand);
            return Ok(response);
        }
    }
}
