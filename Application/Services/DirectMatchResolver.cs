using eWorldCup.Application.Interfaces;

namespace eWorldCup.Application.Services;

public class DirectMatchResolver : IDirectMatchResolver
{
    public (int playerIndex, int opponentIndex) Resolve(int participantCount, int playerIndex, int round)
    {
        var (p, o) = RoundRobinPairing.GetDirectMatch(participantCount, playerIndex, round);
        return (p, o);
    }
}
