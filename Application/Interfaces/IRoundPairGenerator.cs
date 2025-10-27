using eWorldCup.Domain.Entities;

namespace eWorldCup.Application.Interfaces;

public interface IRoundPairGenerator
{
    List<MatchPair> GeneratePairs(IReadOnlyList<Participant> participants, int round);
}
