namespace eWorldCup.Application.DTOs;

public record ErrorResponse(string Message);
public record MaxRoundsResponse(int ParticipantCount, int MaxNumRounds);
public record RemainingPairsResponse(int ParticipantCount, int RoundsPlayed, int RemainingPairs);
public record DirectMatchResponse(string Player, string Opponent);
public record PairsResponse(IEnumerable<MatchPair> Pairs);
