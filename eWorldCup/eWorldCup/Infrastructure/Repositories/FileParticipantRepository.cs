using System.Text.Json;
using eWorldCup.Application.Repositories;
using eWorldCup.Domain.Entities;

namespace eWorldCup.Infrastructure.Repositories;

public class FileParticipantRepository(IWebHostEnvironment env, ILogger<FileParticipantRepository> logger) : IParticipantRepository
{
    private readonly IWebHostEnvironment _env = env;
    private readonly ILogger<FileParticipantRepository> _logger = logger;
    private readonly string _relativePath = Path.Combine("eWorldCup", "Presentation", "Participants.json");

    public async Task<IReadOnlyList<Participant>> GetAllAsync()
    {
        try
        {
            var fullPath = Path.Combine(_env.ContentRootPath, _relativePath);
            if (!File.Exists(fullPath))
            {
                _logger.LogWarning("Participants file not found at {Path}", fullPath);
                return [];
            }
            await using var stream = File.OpenRead(fullPath);
            var participants = await JsonSerializer.DeserializeAsync<List<Participant>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Participant>();
            return participants;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading participants file");
            return [];
        }
    }
}
