namespace VesperAPI.Repository
{
    public interface IRepository
    {
        Task<T?> GetAsync<T>(Guid id) where T : VesperDocument;
        Task<IEnumerable<T>> GetAllAsync<T>() where T : VesperDocument;
        Task AddAsync<T>(T item) where T : VesperDocument;
        Task UpdateAsync<T>(T item) where T : VesperDocument;
        Task DeleteAsync<T>(Guid id) where T : VesperDocument;
    }
}
