namespace VesperAPI.Configuration
{
    public class CosmosDbConfig
    {
        public const string AppSettingsKey = "CosmosDb";

        public string ConnectionString { get; set; } = string.Empty;
        public string Database { get; set; } = string.Empty;
        public string Container { get; set; } = string.Empty;
    }
}
