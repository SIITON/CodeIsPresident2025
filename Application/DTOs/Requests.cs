using eWorldCup.Domain.Entities;

namespace eWorldCup.Application.DTOs;

public class GeneratePairsRequest
{
    public int Round { get; set; }
    public List<Participant> Participants { get; set; } = new();
}

public class DirectMatchRequest
{
    public int ParticipantCount { get; set; }
    public int PlayerIndex { get; set; }
    public int Round { get; set; }
    public List<Participant> Participants { get; set; } = new();
}
