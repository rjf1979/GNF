using System;

namespace GNF.Domain.UnitOfWork
{
    public interface IDbContext<out TDbContext>:IDisposable
    {
        TDbContext Current { get; }
    }
}
