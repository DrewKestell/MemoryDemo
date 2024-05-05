using VesperAPI.Models;

namespace VesperAPI.Services
{
    public interface IGameReviewService
    {
        Task<OperationResult<GameReview>> GetGameReviewAsync(Guid id);
        Task<OperationResult<IEnumerable<GameReview>>> ListGameReviewsAsync();
        Task<OperationResult> CreateGameReviewAsync(GameReview gameReview);
    }
}
