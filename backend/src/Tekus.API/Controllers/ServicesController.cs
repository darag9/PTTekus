using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Common;
using Tekus.Application.Features.Services.Commands.CreateService;
using Tekus.Application.Features.Services.Commands.UpdateService;
using Tekus.Application.Features.Services.DTOs;
using Tekus.Application.Features.Services.Queries.GetServiceById;
using Tekus.Application.Features.Services.Queries.GetServices;

namespace Tekus.API.Controllers;

[Authorize]
public class ServicesController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ServiceDto>>> GetServices(
        [FromQuery] string? search,
        [FromQuery] string? sortBy,
        [FromQuery] bool ascending = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        return await Mediator.Send(new GetServicesQuery(search, sortBy, ascending, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceDto>> GetService(Guid id)
    {
        return await Mediator.Send(new GetServiceByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateService(CreateServiceCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateService(Guid id, UpdateServiceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteService(Guid id)
    {
        await Mediator.Send(new Tekus.Application.Features.Services.Commands.DeleteService.DeleteServiceCommand(id));
        return NoContent();
    }
}
