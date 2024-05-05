using Microsoft.AspNetCore.Mvc;
using VesperAPI.Instrumentation;
using VesperAPI.Models;
using VesperAPI.Services;

namespace VesperAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameReviewController(IGameReviewService gameReviewService) : ControllerBase
    {
        private readonly IGameReviewService _gameReviewService = gameReviewService;

        [HttpGet("{id}")]
        [Instrumentation("VesperAPI.GameReviewController.Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var operationResult = await _gameReviewService.GetGameReviewAsync(id);
            if (operationResult.StatusCode == OperationStatusCode.NotFound)
                return NotFound();

            return Ok(operationResult.Value);
        }

        [HttpGet]
        [Instrumentation("VesperAPI.GameReviewController.List")]
        public async Task<IActionResult> List()
        {
            var operationResult = await _gameReviewService.ListGameReviewsAsync();

            return Ok(operationResult.Value);
        }

        [HttpPut]
        [Instrumentation("VesperAPI.GameReviewController.Create")]
        public async Task<IActionResult> Create(GameReview gameReview)
        {
            var operationResult = await _gameReviewService.CreateGameReviewAsync(gameReview);
            if (operationResult.StatusCode == OperationStatusCode.AlreadyExists)
                return BadRequest("A game review with the provided id already exists.");

            return Ok();
        }
    }
}
