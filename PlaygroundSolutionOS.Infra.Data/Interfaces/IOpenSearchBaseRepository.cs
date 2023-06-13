namespace PlaygroundSolutionOS.Infra.Data.Interfaces;

public interface IOpenSearchBaseRepository<T> where T : class
{
    Task<T?> GetAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> InsertAsync(T t);
    Task<bool> UpdateBaseAsync(T t);
    Task<long> GetTotalCountAsync();
    Task<bool> DeleteByIdAsync(string id);
}