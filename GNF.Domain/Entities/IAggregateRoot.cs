using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNF.Domain.Entities
{
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity
    {

    }

    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>//, IGeneratesDomainEvents
    {

    }

    //public interface IGeneratesDomainEvents
    //{
    //    ICollection<IEventData> DomainEvents { get; }
    //}
}
