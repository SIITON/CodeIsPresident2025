using eWorldCup.Application.Interfaces;
using eWorldCup.Domain.Entities;

namespace eWorldCup.Application;

public class RoundRobinPairGenerator(ILogger<RoundRobinPairGenerator> logger) : IRoundPairGenerator
{
    private readonly ILogger<RoundRobinPairGenerator> _logger = logger;

    public List<MatchPair> GeneratePairs(IReadOnlyList<Participant> participants, int round)
    {
        int n = participants.Count;
        try
        {
            var order = RoundRobinPairing.GetRotatedOrder(n, round);
            _logger.LogInformation("Participants order for round {Round}: {Order}",
                round, string.Join(", ", order.Select(i => participants[i].Name)));

            var rawPairs = RoundRobinPairing.GetRoundPairs(n, round);
            var result = MapPairs(rawPairs, participants);

            _logger.LogInformation("Generated pairs for round {Round}: {Pairs}",
                round, string.Join(", ", result.Select(p => $"{p.ParticipantA} vs {p.ParticipantB}")));

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating pairs for round {Round}", round);
            throw;
        }
    }

    private static List<MatchPair> MapPairs(List<(int A, int B)> rawPairs, IReadOnlyList<Participant> participants)
    {
        return rawPairs
            .Select(pair => new MatchPair(participants[pair.A].Name, participants[pair.B].Name))
            .ToList();
    }
}