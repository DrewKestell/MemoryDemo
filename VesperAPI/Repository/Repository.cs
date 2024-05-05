namespace VesperAPI.Repository
{
    using Microsoft.Azure.Cosmos;
    using Microsoft.Extensions.Options;
    using System.Threading.Tasks;
    using VesperAPI.Configuration;

    public class Repository : IRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public Repository(IOptions<CosmosDbConfig> cosmosDbConfig)
        {
            _cosmosClient = new CosmosClient(cosmosDbConfig.Value.ConnectionString);
            _container = _cosmosClient.GetContainer(cosmosDbConfig.Value.Database, cosmosDbConfig.Value.Container);
        }

        public async Task<T?> GetAsync<T>(Guid id) where T : VesperDocument
        {
            try
            {
                ItemResponse<T> response = await _container.ReadItemAsync<T>(id.ToString(), new PartitionKey(typeof(T).Name));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : VesperDocument
        {
            var query = new QueryDefinition($"SELECT * FROM c WHERE c.pk = '{typeof(T).Name}'");
            var results = new List<T>();

            using FeedIterator<T> feedIterator = _container.GetItemQueryIterator<T>(query);
            while (feedIterator.HasMoreResults)
            {
                foreach (var item in await feedIterator.ReadNextAsync())
                {
                    results.Add(item);
                }
            }

            return results;
        }

        public async Task AddAsync<T>(T item) where T : VesperDocument
        {
            await _container.CreateItemAsync(item);
        }

        public async Task UpdateAsync<T>(T item) where T : VesperDocument
        {
            await _container.ReplaceItemAsync(item, item.Id.ToString(), new PartitionKey(item.PartitionKey));
        }

        public async Task DeleteAsync<T>(Guid id) where T : VesperDocument
        {
            await _container.DeleteItemAsync<T>(id.ToString(), new PartitionKey(typeof(T).Name));
        }
    }
}
