namespace RavenShop.Application.Data.Context;

public class RavenDbContext : IRavenDbContext
{
    private readonly DocumentStore _store;
    public IDocumentStore store => _store;

    private readonly PersistenceSettings _persistenceSettings;

    public RavenDbContext(IOptionsMonitor<PersistenceSettings> settings)
    {
        _persistenceSettings = settings.CurrentValue;

        _store = new DocumentStore()
        {
            Database = _persistenceSettings.DatabaseName,
            Urls = _persistenceSettings.Urls
        };

        _store.Initialize();

        EnsureDatabaseIsCreated();
    }

    public void EnsureDatabaseIsCreated()
    {
        try
        {
            _store.Maintenance.ForDatabase(_persistenceSettings.DatabaseName).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            _store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(_persistenceSettings.DatabaseName)));
        }
    }
}

