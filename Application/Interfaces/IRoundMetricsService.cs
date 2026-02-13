namespace eWorldCup.Application.Interfaces;

public interface IRoundMetricsService
{
    long GetMaxNumRounds(long participantCount);
    long GetTotalPairs(long participantCount);
    long GetPlayedPairs(long participantCount, long round);
    long GetRemainingPairs(long participantCount, long round);
}
