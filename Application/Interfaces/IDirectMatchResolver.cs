namespace eWorldCup.Application.Interfaces;

public interface IDirectMatchResolver
{
    (int playerIndex, int opponentIndex) Resolve(int participantCount, int playerIndex, int round);
}
