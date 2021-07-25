using ShopWebAPI.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopWebAPI.Infra.Repositories
{
    public class RavenDbRepository<T> : IRepository<T>
    {
        private readonly IRavenDbContext _context;

        public RavenDbRepository(IRavenDbContext context)
        {
            _context = context;
        }
        public void CreateOrUpdate(T obj)
        {
            try
            {
                using var session = _context.store.OpenSession();
                session.Store(obj);
                session.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public void Delete(string id)
        {
            try
            {
                using var session = _context.store.OpenSession();
                var element = session.Load<T>(id);
                session.Delete(element);
                session.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public T Select(string id)
        {
            try
            {
                using var session = _context.store.OpenSession();
                var element = session.Load<T>(id);
                return element;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }

        public IEnumerable<T> SelectAll(int pageSize, int pageNumber)
        {
            try
            {
                int skip = pageSize * (pageNumber - 1);
                int take = pageSize;

                using var session = _context.store.OpenSession();

                var elements = session
                    .Query<T>()
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                return elements;
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException);
            }
        }
    }
    public class RepositoryException : Exception
    {
        public RepositoryException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
