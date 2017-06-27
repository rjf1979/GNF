using System.Threading.Tasks;
using GNF.Domain.UnitOfWork;

namespace GNF.EFUnitwork.UnitOfWork
{
    public class EFUnitOfWork: UnitOfWorkBase
    {
        //private readonly IDbContextResolver _dbContextResolver;

        /// <summary>
        /// 
        /// </summary>
        public EFUnitOfWork(IConnectionStringResolver connectionStringResolver)
            : base(connectionStringResolver)
        {
            
        }

        public override void Begin()
        {

        }

        public override void SaveChanges()
        {
            
        }

        public override async Task SaveChangesAsync()
        {
            
        }

        protected override void CompleteUow()
        {
            SaveChanges();
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
        }
    }
}
