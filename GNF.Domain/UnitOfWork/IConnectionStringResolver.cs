namespace GNF.Domain.UnitOfWork
{
    /// <summary>
    /// Used to get connection string when a database connection is needed.
    /// </summary>
    public interface IConnectionStringResolver
    {
        string GetConnectionString();
        string GetNameOrConnectionString();
    }
}
