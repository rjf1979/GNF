using System.Data;
using System.Data.SqlClient;

namespace GNF.DapperUow
{
    /// <summary>
    /// 数据库连接工厂
    /// </summary>
    public static class ConnectionFactory
    {
        /// <summary>
        /// 创建一个通用连接
        /// </summary>
        /// <typeparam name="TDbConnection"></typeparam>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection CreateConnection<TDbConnection>(string connectionString) where TDbConnection : IDbConnection, new()
        {
            IDbConnection connection = new TDbConnection();
            connection.ConnectionString = connectionString;
            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbConnection CreateSqlConnection(string connectionString)
        {
            return CreateConnection<SqlConnection>(connectionString);
        }
    }
}
