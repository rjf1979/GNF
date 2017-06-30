using System.Transactions;
using GNF.Domain.UnitOfWork;

namespace GNF.EFUow
{
    public class SqlTransaction:ITransaction
    {
        private readonly TransactionScope _transactionScope;

        public SqlTransaction()
        {
            _transactionScope = new TransactionScope();
        }

        public void BeginTran()
        {
        }

        public void RollbackTran()
        {
            
        }

        public void CompleteTran()
        {
            _transactionScope.Complete();
        }
    }
}
