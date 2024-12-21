namespace Common;

public class Vote : IEquatable<Vote>
{
    public int? StoryPoints { get; set; }
    public NonStoryPointVote? NonStoryPointVote { get; set; }

    public Vote(int storyPoints) => StoryPoints = storyPoints;
    public Vote(NonStoryPointVote nonStoryPointVote) => NonStoryPointVote = nonStoryPointVote;
    public Vote() { }

    bool IEquatable<Vote>.Equals(Vote? other)
    {
        if (other == null) return false;
        return StoryPoints == other.StoryPoints && NonStoryPointVote == other.NonStoryPointVote;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (obj is Vote vote) return Equals(vote);
        return false;
    }

    public override int GetHashCode() => HashCode.Combine(StoryPoints, NonStoryPointVote);

    public static implicit operator string(Vote vote)
    {
        if (vote.StoryPoints.HasValue)
        {
            return vote.StoryPoints.Value.ToString();
        }
        else if (vote.NonStoryPointVote.HasValue)
        {
            return vote.NonStoryPointVote switch
            {
                Common.NonStoryPointVote.Coffee => "-",
                Common.NonStoryPointVote.Question => "?",
                _ => throw new NotImplementedException(),
            };
        }
        return string.Empty;
    }

    public static implicit operator Vote(string str)
    {
        return str.TryParse(out var vote) 
            ? vote 
            : throw new ArgumentException("Invalid vote string", nameof(str));
    }
}

public enum NonStoryPointVote
{
    Coffee,
    Question
}

public static class VoteExtensions
{
    public static bool TryParse(this string rawVote, out Vote vote)
    {
        if (int.TryParse(rawVote, out var storyPoints))
        {
            vote = new Vote(storyPoints);
            return true;
        }
        else if (Enum.TryParse<NonStoryPointVote>(rawVote, true, out var nonStoryPointVote))
        {
            vote = new Vote(nonStoryPointVote);
            return true;
        }
        else if (rawVote == "?")
        {
            vote = new Vote(NonStoryPointVote.Question);
            return true;
        }
        else if (rawVote == "-" || rawVote == "☕")
        {
            vote = new Vote(NonStoryPointVote.Coffee);
            return true;
        }

        vote = new Vote();
        return false;
    }
}