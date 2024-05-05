using Newtonsoft.Json;
using VesperAPI.Models;

namespace VesperAPI.Repository
{
    [method: JsonConstructor]
    public class GameReviewDocument(
        Guid id,
        string gameName,
        float score,
        string comments) : VesperDocument(id)
    {
        public GameReviewDocument(GameReview gameReview) : this(
            gameReview.Id,
            gameReview.GameName,
            gameReview.Score,
            gameReview.Comments)
        {
        }

        [JsonProperty("gameName")]
        public string GameName { get; } = gameName;

        [JsonProperty("score")]
        public float Score { get; } = score;

        [JsonProperty("comments")]
        public string Comments { get; } = comments;
    }
}
