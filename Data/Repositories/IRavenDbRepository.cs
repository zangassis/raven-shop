namespace RavenShop.Application.Data.Repositories;

public interface IRavenDbRepository<T>
{
    void Create(T obj);
    void Update(T obj);
    void Delete(string id);
    public T Select(string id);
    public IEnumerable<T> SelectAll(int pageSize, int pageNumber);
}

