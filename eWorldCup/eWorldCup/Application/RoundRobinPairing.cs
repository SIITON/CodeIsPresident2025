namespace eWorldCup.Application;

internal static class RoundRobinPairing
{
    // Returns the rotated order of indices for a given round (1-based round)
    public static IReadOnlyList<int> GetRotatedOrder(int participantCount, int round)
    {
        Validate(participantCount, round);

        var indices = Enumerable.Range(0, participantCount).ToList();
        for (int r = 0; r < round - 1; r++)
        {
            // same rotation logic used previously
            var last = indices[^1];
            indices.RemoveAt(indices.Count - 1);
            indices.Insert(1, last);
        }
        return indices;
    }

    // Returns list of pairing tuples (A,B) for round
    public static List<(int A, int B)> GetRoundPairs(int participantCount, int round)
    {
        var rotated = GetRotatedOrder(participantCount, round);
        var pairs = new List<(int A, int B)>(participantCount / 2);
        int n = participantCount;
        for (int i = 0; i < n / 2; i++)
        {
            pairs.Add((rotated[i], rotated[n - 1 - i]));
        }
        return pairs;
    }

    public static (int Player, int Opponent) GetDirectMatch(int participantCount, int playerIndex, int round)
    {
        Validate(participantCount, round);

        if (playerIndex < 0 || playerIndex >= participantCount)
            throw new ArgumentOutOfRangeException(nameof(playerIndex));

        var pairs = GetRoundPairs(participantCount, round);
        foreach (var (a, b) in pairs)
        {
            if (a == playerIndex) return (a, b);
            if (b == playerIndex) return (b, a);
        }

        throw new InvalidOperationException("Player not found in computed pairs.");
    }

    private static void Validate(int participantCount, int round)
    {
        if (participantCount < 2)
            throw new ArgumentException("At least two participants required.", nameof(participantCount));
        if (participantCount % 2 != 0)
            throw new ArgumentException("Participant count must be even for round-robin pairing.", nameof(participantCount));
        if (round < 1 || round > participantCount - 1)
            throw new ArgumentOutOfRangeException(nameof(round), "Round must be between 1 and n-1.");
    }
}