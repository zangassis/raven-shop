using System.Collections.Generic;

namespace ShopWebAPI.Infra.Repositories
{
    public interface IRepository<T>
    {
        void CreateOrUpdate(T obj);

        void Delete(string id);

        public T Select(string id);

        public IEnumerable<T> SelectAll(int pageSize, int pageNumber);
    }
}
