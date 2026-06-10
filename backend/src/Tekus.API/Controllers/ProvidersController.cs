using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Common;
using Tekus.Application.Features.Providers.Commands.AddServiceToProvider;
using Tekus.Application.Features.Providers.Commands.CreateProvider;
using Tekus.Application.Features.Providers.Commands.RemoveServiceFromProvider;
using Tekus.Application.Features.Providers.Commands.UpdateProvider;
using Tekus.Application.Features.Providers.DTOs;
using Tekus.Application.Features.Providers.Queries.GetProviderById;
using Tekus.Application.Features.Providers.Queries.GetProviders;

namespace Tekus.API.Controllers;

[Authorize]
public class ProvidersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PagedResult<ProviderDto>>> GetProviders(
        [FromQuery] string? search,
        [FromQuery] string? sortBy,
        [FromQuery] bool ascending = true,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        return await Mediator.Send(new GetProvidersQuery(search, sortBy, ascending, page, pageSize));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProviderDetailDto>> GetProvider(Guid id)
    {
        return await Mediator.Send(new GetProviderByIdQuery(id));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProvider(CreateProviderCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProvider(Guid id, UpdateProviderCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPost("{id}/services")]
    public async Task<ActionResult> AddService(Guid id, [FromBody] AddServiceRequest request)
    {
        await Mediator.Send(new AddServiceToProviderCommand(id, request.ServiceId, request.CustomHourlyRate));
        return NoContent();
    }

    [HttpDelete("{id}/services/{serviceId}")]
    public async Task<ActionResult> RemoveService(Guid id, Guid serviceId)
    {
        await Mediator.Send(new RemoveServiceFromProviderCommand(id, serviceId));
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProvider(Guid id)
    {
        await Mediator.Send(new Tekus.Application.Features.Providers.Commands.DeleteProvider.DeleteProviderCommand(id));
        return NoContent();
    }
}

public class AddServiceRequest
{
    public Guid ServiceId { get; set; }
    public decimal? CustomHourlyRate { get; set; }
}
