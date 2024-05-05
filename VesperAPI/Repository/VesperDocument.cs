using Newtonsoft.Json;
namespace VesperAPI.Repository
{
    public abstract class VesperDocument(Guid id)
    {
        [JsonProperty("id")]
        public Guid Id { get; } = id;

        [JsonProperty("pk")]
        public string PartitionKey => GetType().Name;
    }
}
