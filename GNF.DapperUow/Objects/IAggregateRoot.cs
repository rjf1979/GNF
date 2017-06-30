using GNF.Domain.Entities;

namespace GNF.DapperUow.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {

    }
}