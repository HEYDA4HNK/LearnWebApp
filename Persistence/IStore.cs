namespace Persistence
{
    public interface IStore<T, IdType>
    {
        public Task Create(T user, string password);
        public Task<T?> GetById(IdType id);
        public Task DeleteById(IdType id);
        public Task<T> UpdateById(IdType id, T value);
        public Task<List<T>> GetObjects();
    }
}
