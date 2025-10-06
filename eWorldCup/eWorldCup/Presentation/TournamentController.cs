using eWorldCup.Application.Interfaces;
using eWorldCup.Application.Repositories;
using eWorldCup.Application.Responses;
using eWorldCup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eWorldCup.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly IRoundPairGenerator _pairGenerator;
        private readonly IRoundMetricsService _metrics;
        private readonly IDirectMatchResolver _directMatch;
        private readonly IParticipantRepository _participantsRepo;

        public TournamentController(
            IRoundPairGenerator pairGenerator,
            IRoundMetricsService metrics,
            IDirectMatchResolver directMatch,
            IParticipantRepository participantsRepo)
        {
            _pairGenerator = pairGenerator;
            _metrics = metrics;
            _directMatch = directMatch;
            _participantsRepo = participantsRepo;
        }

        [HttpGet("participants")]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipants()
        {
            var participants = await _participantsRepo.GetAllAsync();
            if (participants == null || participants.Count == 0) return NotFound();
            return Ok(participants);
        }

        [HttpPost("round")]
        public ActionResult<PairsResponse> GenerateRoundPairs([FromBody] GeneratePairsRequest request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var pairs = _pairGenerator.GeneratePairs(request.Participants, request.Round);
                return Ok(new PairsResponse(pairs));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponse(ex.Message));
            }
        }

        [HttpGet("max-rounds")]
        public ActionResult<MaxRoundsResponse> GetMaxRounds([FromQuery] long participantCount)
        {
            if (participantCount <= 0)
                return BadRequest(new ErrorResponse($"ParticipantCount must be > 0. Received: {participantCount}"));
            var max = _metrics.GetMaxNumRounds(participantCount);
            return Ok(new MaxRoundsResponse((int)participantCount, (int)max));
        }

        [HttpGet("remaining-pairs")]
        public ActionResult<RemainingPairsResponse> GetRemainingPairs(
            [FromQuery] long participantCount,
            [FromQuery] long roundsPlayed)
        {
            if (participantCount <= 0)
                return BadRequest(new ErrorResponse($"ParticipantCount must be > 0. Received: {participantCount}"));
            if (roundsPlayed < 0)
                return BadRequest(new ErrorResponse($"RoundsPlayed must be >= 0. Received: {roundsPlayed}"));

            var remaining = _metrics.GetRemainingPairs(participantCount, roundsPlayed);
            return Ok(new RemainingPairsResponse((int)participantCount, (int)roundsPlayed, (int)remaining));
        }

        [HttpPost("direct-match")]
        public ActionResult<DirectMatchResponse> GetDirectMatch([FromBody] DirectMatchRequest request)
        {
            if (request.ParticipantCount <= 0)
                return BadRequest(new ErrorResponse($"ParticipantCount must be > 0. Received: {request.ParticipantCount}"));
            if (request.PlayerIndex < 0 || request.PlayerIndex >= request.ParticipantCount)
                return BadRequest(new ErrorResponse($"PlayerIndex must be in [0,{request.ParticipantCount - 1}]"));
            if (request.Round < 1 || request.Round > request.ParticipantCount - 1)
                return BadRequest(new ErrorResponse($"Round must be between 1 and {request.ParticipantCount - 1}"));

            var (pIdx, oIdx) = _directMatch.Resolve(request.ParticipantCount, request.PlayerIndex, request.Round);
            var playerName = request.Participants[pIdx].Name;
            var opponentName = request.Participants[oIdx].Name;
            return Ok(new DirectMatchResponse(playerName, opponentName));
        }
    }

    public class GeneratePairsRequest
    {
        public int Round { get; set; }
        public List<Participant> Participants { get; set; } = new();
    }

    public class DirectMatchRequest
    {
        public int ParticipantCount { get; set; }
        public int PlayerIndex { get; set; }
        public int Round { get; set; }
        public List<Participant> Participants { get; set; } = new();
    }
}

