using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GNF.Domain.UnitOfWork;

namespace GNF.EFUnitwork.UnitOfWork
{
    public class EFUnitOfWork: UnitOfWorkBase
    {

        private readonly IDbContextResolver _dbContextResolver;

        /// <summary>
        /// 
        /// </summary>
        public EFUnitOfWork(IConnectionStringResolver connectionStringResolver,IDbContextResolver dbContextResolver)
            : base(connectionStringResolver)
        {
            _dbContextResolver = dbContextResolver;}

        public override void Begin()
        {

        }

        public override void SaveChanges()
        {
            
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                
            }
        }

        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
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
