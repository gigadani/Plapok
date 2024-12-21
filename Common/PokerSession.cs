namespace Common;

public class PokerSession
{
    public List<Ticket> Tickets { get; set; } = [];
    public string? CurrentTicketId { get; set; }
}
