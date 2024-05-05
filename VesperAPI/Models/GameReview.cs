using System.Text.Json.Serialization;
using VesperAPI.Repository;

namespace VesperAPI.Models
{
    [method: JsonConstructor]
    public class GameReview (Guid id, string gameName, float score, string comments)
    {
        public GameReview(GameReviewDocument gameReviewDocument) : this(
            gameReviewDocument.Id,
            gameReviewDocument.GameName,
            gameReviewDocument.Score,
            gameReviewDocument.Comments)
        {
        }

        public Guid Id { get; } = id;

        public string GameName { get; } = gameName;

        public float Score { get; } = score;

        public string Comments { get; } = comments;
    }
}
