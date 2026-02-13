using System.Text.Json;
using eWorldCup.Application.Repositories;
using eWorldCup.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eWorldCup.Infrastructure.Repositories;

public class FileParticipantRepository : IParticipantRepository
{
    private readonly IHostingEnvironment _env;
    private readonly ILogger<FileParticipantRepository> _logger;
    private readonly string _fileName = "Participants.json";

    public FileParticipantRepository(IHostingEnvironment env, ILogger<FileParticipantRepository> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<IReadOnlyList<Participant>> GetAllAsync()
    {
        try
        {
            var fullPath = Path.Combine(_env.ContentRootPath, _fileName);
            if (!File.Exists(fullPath))
            {
                _logger.LogWarning("Participants file not found at {Path}", fullPath);
                return Array.Empty<Participant>();
            }
            
            await using var stream = File.OpenRead(fullPath);
            var participants = await JsonSerializer.DeserializeAsync<List<Participant>>(stream, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            }) ?? new List<Participant>();
            
            return participants;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error reading participants file");
            return Array.Empty<Participant>();
        }
    }
}
