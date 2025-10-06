using eWorldCup.Application.Interfaces;

namespace eWorldCup.Application.Services;

public class RoundMetricsService : IRoundMetricsService
{
    // long overloads
    public long GetMaxNumRounds(long participantCount) => participantCount - 1;
    public long GetTotalPairs(long participantCount) => (participantCount * (participantCount - 1)) / 2;
    public long GetPlayedPairs(long participantCount, long round) => (round - 1) * (participantCount / 2);

    public long GetRemainingPairs(long participantCount, long round)
    {
        var total = GetTotalPairs(participantCount);
        var played = GetPlayedPairs(participantCount, round);
        return total - played;
    }
}
