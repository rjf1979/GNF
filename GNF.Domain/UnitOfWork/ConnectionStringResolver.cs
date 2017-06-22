namespace GNF.Domain.UnitOfWork
{
    public class ConnectionStringResolver : IConnectionStringResolver
    {
        private readonly string _connectionKey;
        private readonly ConnectionConfigType _connectionConfigType;

        public ConnectionStringResolver(string connectionKey, ConnectionConfigType connectionConfigType = ConnectionConfigType.ConnectionStrings)
        {
            _connectionKey = connectionKey;
            _connectionConfigType = connectionConfigType;
        }

        public string GetNameOrConnectionString()
        {
            if (_connectionConfigType == ConnectionConfigType.AppSettings)
            {
                return Common.Utility.Configuration.AppSettings[_connectionKey];
            }
            if (_connectionConfigType == ConnectionConfigType.ConnectionStrings)
            {
                return _connectionKey;
            }
            return string.Empty;
        }

        public string GetConnectionString()
        {
            if (_connectionConfigType == ConnectionConfigType.AppSettings)
            {
                return Common.Utility.Configuration.AppSettings[_connectionKey];
            }
            if (_connectionConfigType == ConnectionConfigType.ConnectionStrings)
            {
                return Common.Utility.Configuration.ConnectionStrings[_connectionKey].ConnectionString;
            }
            return string.Empty;
        }
    }
}
