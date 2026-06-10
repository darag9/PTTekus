using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tekus.Application.Features.Dashboard.DTOs;
using Tekus.Application.Features.Dashboard.Queries.GetDashboard;

namespace Tekus.API.Controllers;

[Authorize]
public class DashboardController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<DashboardDto>> GetDashboard()
    {
        return await Mediator.Send(new GetDashboardQuery());
    }
}
