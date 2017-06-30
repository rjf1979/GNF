using GNF.Domain.Entities;

namespace GNF.DapperUow.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class AggregateRoot : AggregateRoot<int>, IAggregateRoot
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {

    }
}