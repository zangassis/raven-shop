using RavenShop.Application.Data.Context;

namespace RavenShop.Application.Data.Repositories
{
    public class RavenDbRepository<T> : IRavenDbRepository<T>
    {
        private readonly IRavenDbContext _context;

        public RavenDbRepository(IRavenDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> SelectAll(int pageSize, int pageNumber)
        {
            int skip = pageSize * (pageNumber - 1);
            int take = pageSize;

            using var session = _context.store.OpenSession();

            var elements = session.Query<T>().Skip(skip).Take(take).ToList();
            return elements;
        }

        public T Select(string id)
        {
            using var session = _context.store.OpenSession();

            var element = session.Load<T>(id);
            return element;
        }

        public void Create(T obj)
        {
            using var session = _context.store.OpenSession();

            session.Store(obj);
            session.SaveChanges();
        }

        public void Update(T obj)
        {
            using var session = _context.store.OpenSession();

            session.Store(obj);
            session.SaveChanges();
        }

        public void Delete(string id)
        {
            using var session = _context.store.OpenSession();
            var element = session.Load<T>(id);

            session.Delete(element);
            session.SaveChanges();
        }
    }
}
