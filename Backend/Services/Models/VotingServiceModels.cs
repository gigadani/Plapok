using Common;
namespace Backend.Services.Models.VotingServiceModels;

public record TicketModel(string? TicketId);

public record VotedUsersListModel(Guid[] UserGuids);

public record CastedVoteModel(Guid UserGuid, Vote Vote);

public record VotingStatusModel(bool VotingStarted, IEnumerable<CastedVoteModel>? Votes, string? Ticket, Guid[] VotedUsers);

public class VoteResult : Result<Decision, string>
{
    public Decision? Decision => Value;
    public string? Message => Error;
    public bool Inconclusive => Success;

    public VoteResult(Decision decision) : base(decision) { }

    public VoteResult(string message) : base(message) { }
}
public record Decision(IEnumerable<CastedVoteModel>? Votes, double Average, int Value);