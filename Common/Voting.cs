namespace Common;

/// <summary>
/// A voting where people casts votes about storypoints for a (possible) ticket.
/// </summary>
public class Voting
{
    public string? TicketId { get; set; }

    public Dictionary<Guid, Vote> Votes { get; set; } = [];

    public DateTime? EndCountDownStarted { get; set; }
}

public static class VotingExtensions
{
    public static int? GetAverageStoryPoints(this Voting session)
    {
        if (session.Votes.All(vote => !vote.Value.StoryPoints.HasValue)) return null;
        return (int)session.Votes.Values.Average(vote => vote.StoryPoints)!;
    }
}