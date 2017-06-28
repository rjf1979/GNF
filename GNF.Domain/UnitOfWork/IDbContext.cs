namespace GNF.Domain.UnitOfWork
{
    public interface IDbContext<out TDbContext>
    {
        TDbContext Context { get; }
    }
}
