using VesperAPI.Models;
using VesperAPI.Repository;

namespace VesperAPI.Services
{
    public class GameReviewService(IRepository repository) : IGameReviewService
    {
        private readonly IRepository _repository = repository;

        public async Task<OperationResult<GameReview>> GetGameReviewAsync(Guid id)
        {
            var gameReviewDocument = await _repository.GetAsync<GameReviewDocument>(id);
            if (gameReviewDocument == null)
                return new OperationResult<GameReview>(OperationStatusCode.NotFound);

            var gameReview = new GameReview(gameReviewDocument);
            return new OperationResult<GameReview>(OperationStatusCode.OK, gameReview);
        }

        public async Task<OperationResult<IEnumerable<GameReview>>> ListGameReviewsAsync()
        {
            var gameReviewDocuments = await _repository.GetAllAsync<GameReviewDocument>();
            var gameReviews = gameReviewDocuments.Select(d => new GameReview(d));
            return new OperationResult<IEnumerable<GameReview>>(OperationStatusCode.OK, gameReviews);
        }

        public async Task<OperationResult> CreateGameReviewAsync(GameReview gameReview)
        {
            var existingGameReviewDocument = await _repository.GetAsync<GameReviewDocument>(gameReview.Id);
            if (existingGameReviewDocument != null)
                return new OperationResult(OperationStatusCode.AlreadyExists);

            var gameReviewDocument = new GameReviewDocument(gameReview);
            await _repository.AddAsync(gameReviewDocument);
            return new OperationResult(OperationStatusCode.OK);
        }
    }
}
