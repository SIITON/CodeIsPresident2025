using eWorldCup.Domain.Entities;

namespace eWorldCup.Application.Repositories;

public interface IParticipantRepository
{
    Task<IReadOnlyList<Participant>> GetAllAsync();
}
