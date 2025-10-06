using eWorldCup.Application.Interfaces;
using eWorldCup.Domain.Entities;

namespace eWorldCup.Application;

public class RoundRobinPairGenerator(ILogger<RoundRobinPairGenerator> logger) : IRoundPairGenerator
{
    private readonly ILogger<RoundRobinPairGenerator> _logger = logger;

    public List<MatchPair> GeneratePairs(IReadOnlyList<Participant> participants, int round)
    {
        int n = participants.Count;
        if (n % 2 != 0)
        {
            _logger.LogError("Number of participants must be even. Provided count: {Count}", n);
            throw new ArgumentException("Number of participants must be even.");
        }
        if (round < 1 || round > n - 1)
        {
            _logger.LogError("Invalid round number: {Round}. Must be between 1 and {MaxRound}", round, n - 1);
            throw new ArgumentOutOfRangeException(nameof(round), "Round must be between 1 and n-1.");
        }

        var order = RoundRobinPairing.GetRotatedOrder(n, round);
        _logger.LogInformation("Participants order for round {Round}: {Order}",
            round, string.Join(", ", order.Select(i => participants[i].Name)));

        var rawPairs = RoundRobinPairing.GetRoundPairs(n, round);
        var result = new List<MatchPair>(rawPairs.Count);
        foreach (var (a, b) in rawPairs)
        {
            result.Add(new MatchPair(participants[a].Name, participants[b].Name));
        }

        _logger.LogInformation("Generated pairs for round {Round}: {Pairs}",
            round, string.Join(", ", result.Select(p => $"{p.ParticipantA} vs {p.ParticipantB}")));

        return result;
    }
}
