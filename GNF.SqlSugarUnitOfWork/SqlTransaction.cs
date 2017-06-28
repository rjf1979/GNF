using GNF.Domain.UnitOfWork;

namespace GNF.SqlSugarUnitOfWork
{
    public class SqlTransaction:ITransaction
    {
        private readonly SqlDbContext _sqlDbProxy;
        public SqlTransaction(SqlDbContext sqlDbProxy)
        {
            _sqlDbProxy = sqlDbProxy;
        }

        public void BeginTran()
        {
            _sqlDbProxy?.Current.Ado.BeginTran();
        }

        public void RollbackTran()
        {
            _sqlDbProxy?.Current.Ado.RollbackTran();
        }

        public void CompleteTran()
        {
            _sqlDbProxy?.Current.Ado.CommitTran(); 
        }
    }
}
