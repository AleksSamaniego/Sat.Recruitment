using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sat.Recruitment.Models.Models;
using System.Net;
using Sat.Recruitment.Api.Features;

namespace Sat.Recruitment.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/create-user")]
    [ProducesResponseType(typeof(Result), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> CreateUser([FromBody, BindRequired] CreateUserCommand user)
    {
        var result = await _mediator.Send(user);
        return result == null ? Conflict(result) : Ok(result);
    }
}
