using Backend.Services.Models.VotingServiceModels;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services;

public class VotingService : ServiceBase
{
    private Voting? Voting { get; set; }

    public VotingService()
    {
    }

    public ServiceResult StartVoting(TicketModel? ticketModel)
    {
        if (Voting != null) return Failure(new BadRequestObjectResult("Voting already started."));
        Voting = new()
        {
            TicketId = ticketModel?.TicketId
        };
        return Success();
    }

    public ServiceResult EndVoting()
    {
        if (Voting == null) return Failure(new BadRequestObjectResult("No ongoing voting."));
        Voting = null;
        return Success();
    }

    public ServiceResult<VotingStatusModel> GetStatus()
    {
        return Success(new VotingStatusModel(
            VotingStarted: Voting != null,
            Votes: Voting?.Votes?.Select(x => new CastedVoteModel(x.Key, x.Value)),
            Ticket: Voting?.TicketId,
            VotedUsers: (Voting?.Votes != null ? Voting.Votes.Keys.ToArray() : null) ?? []
        ));
    }

    /// <summary>
    /// Adds or updates a vote of a user.
    /// </summary>
    /// <param name="userGuid">Guid of the user who is voting.</param>
    /// <param name="vote">The vote the user is casting or updating.</param>
    public ServiceResult AddOrUpdateVote(Guid userGuid, Vote vote)
    {
        if (Voting == null) return Failure(new BadRequestObjectResult("No ongoing voting."));
        Voting.Votes[userGuid] = vote;
        return Success();
    }

    /// <summary>
    /// Removes a vote of a user.
    /// </summary>
    /// <param name="userGuid">Guid of the user who is voting.</param>
    public ServiceResult RemoveVote(Guid userGuid)
    {
        if (Voting == null) return Failure(new BadRequestObjectResult("No ongoing voting."));
        return Voting.Votes.Remove(userGuid)
            ? Success()
            : Failure(new BadRequestObjectResult("No vote."));
    }

    /// <summary>
    /// Gets the vote of a user.
    /// </summary>
    /// <param name="userGuid">Guid of the user who is voting.</param>
    public ServiceResult<Vote> GetVote(Guid userGuid)
    {
        if (Voting == null) return Failure<Vote>(new BadRequestObjectResult("No ongoing voting."));
        return Voting.Votes.TryGetValue(userGuid, out var vote)
            ? Success(vote)
            : Failure<Vote>(new BadRequestObjectResult("No vote."));
    }

    /// <summary>
    /// Returns the id of the ticket of the planning poker session.
    /// </summary>
    public ServiceResult<TicketModel> GetTicket()
    {
        if (Voting == null) return Failure<TicketModel>(new BadRequestObjectResult("No ongoing voting."));
        return Success<TicketModel>(new(Voting.TicketId));
    }

    /// <summary>
    /// Returns a listing of the Guids of the users who have voted.
    /// </summary>
    public ServiceResult<VotedUsersListModel> GetVoteStatuses()
    {
        if (Voting == null) return Failure<VotedUsersListModel>(new BadRequestObjectResult("No ongoing voting."));
        return Success<VotedUsersListModel>(new ([.. Voting.Votes.Keys]));
    }
}
