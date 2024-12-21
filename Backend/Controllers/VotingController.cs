using Backend.Services.Models.VotingServiceModels;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers;

public class VotingController(VotingService service) : PlapokControllerBase
{
    private readonly VotingService Service = service;

    [HttpPost("start")]
    public IActionResult StartVoting([FromBody] TicketModel? ticketModel) =>
        Service.StartVoting(ticketModel).Succeeded(out var error)
            ? NoContent()
            : error;

    [HttpPost("stop")]
    public IActionResult StopVoting() =>
        Service.EndVoting().Succeeded(out var error)
            ? NoContent()
            : error;

    public IActionResult GetStatus() =>
        Service.GetStatus().Succeeded(out var status, out var error)
            ? Ok(status)
            : error;

    public IActionResult Vote([FromBody] string vote)
    {
        var userGuid = GetGuid(User);
        return Service.AddOrUpdateVote(userGuid, vote).Succeeded(out var error)
            ? NoContent()
            : error;
    }

    private static Guid GetGuid(ClaimsPrincipal User)
    {
        return Guid.Empty;
    }
}