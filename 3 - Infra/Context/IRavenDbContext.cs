using Raven.Client.Documents;

namespace ShopWebAPI.Infra.Context
{
    public interface IRavenDbContext
    {
        public IDocumentStore store { get; }
    }
}