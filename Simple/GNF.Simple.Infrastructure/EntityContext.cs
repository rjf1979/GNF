using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GNF.Domain.Entities;

namespace GNF.Simple.Infrastructure
{
    public class EntityContext: DbContext
    {
        public  EntityContext(string connectionName) : base(connectionName)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 180;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class, IEntity
        {
            return base.Set<TEntity>();
        }
    }
}
