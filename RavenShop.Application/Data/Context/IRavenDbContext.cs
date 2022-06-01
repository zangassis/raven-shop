namespace RavenShop.Application.Data.Context;

public interface IRavenDbContext
{
    public IDocumentStore store { get; }
}

