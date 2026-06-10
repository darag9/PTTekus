using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Features.Auth.Commands.Login;
using Tekus.Application.Features.Auth.DTOs;

namespace Tekus.API.Controllers;

[AllowAnonymous]
public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginCommand command)
    {
        return await Mediator.Send(command);
    }
}
