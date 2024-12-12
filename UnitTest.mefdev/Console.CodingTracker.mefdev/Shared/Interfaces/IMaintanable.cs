namespace CodingLogger.Shared
{
    public interface IMaintanable<T>
    {
        Task Add(T obj);
        Task<T> Get(int key);
        Task Update(T obj);
        Task Delete(int key);
        Task<List<T>> GetAll();
    }
}

