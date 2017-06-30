using System.Data.Entity;
using GNF.Domain.Entities;

namespace GNF.EFUow
{
    public class EfDbContext<TEntity, TPrimaryKey> : DbContext where TEntity : Entity<TPrimaryKey>
    {
        public EfDbContext(string connectionName) : base(Validate(connectionName))
        {

        }

        public EfDbContext(DbContext dbContext) : base(dbContext.Database.Connection, false)
        {

        }

        static string Validate(string connectionName)
        {
            if (!connectionName.StartsWith("name="))
            {
                return "name=" + connectionName;
            }
            return connectionName;
        }

        public IDbSet<TEntity> Entities { get; set; }

        public DbContext CurrentContext => this;
    }
}
