using System.Configuration;

namespace GNF.Common.Utility
{
    /// <summary>
    /// 配置文件类
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// appSettings配置属性
        /// </summary>
        /// <example>
        /// <code>
        /// var str = Configuration.AppSettings["conn"];
        /// </code>
        /// </example>
        public static AppSettingsProvider AppSettings { get; } = new AppSettingsProvider();

        /// <summary>
        /// ConnectionStrings 配置属性
        /// </summary>
        /// <example>
        /// <code>
        /// var providerName = Configuration.ConnectionStrings["conn"].ProviderName;
        /// var connectionString = Configuration.ConnectionStrings["conn"].ConnectionString;
        /// </code>
        /// </example>
        public static ConnectionStringsProvider ConnectionStrings { get; } = new ConnectionStringsProvider();

        #region -- 索引器类

        /// <summary>
        /// AppSettings 索引器
        /// </summary>
        public sealed class AppSettingsProvider
        {
            /// <summary>
            /// AppSettings索引器
            /// </summary>
            /// <param name="key">关键字</param>
            /// <returns>返回配置字符串值</returns>
            public string this[string key]
            {
                get
                {
                    var value = ConfigurationManager.AppSettings[key];
                    return value ?? string.Empty;
                }
            }
        }

        /// <summary>
        /// ConnectionStrings 索引器
        /// </summary>
        public sealed class ConnectionStringsProvider
        {
            /// <summary>
            /// ConnectionStrings索引器
            /// </summary>
            /// <param name="connectionName">连接名称</param>
            /// <returns>返回ConnectionStringsProvider</returns>
            public ConnectionStringsProvider this[string connectionName]
            {
                get
                {
                    lock (this)
                    {
                        var connection = ConfigurationManager.ConnectionStrings[connectionName];
                        if (connection != null)
                        {
                            Name = connection.Name;
                            ProviderName = connection.ProviderName;
                            ConnectionString = connection.ConnectionString;
                        }
                        return this;
                    }
                }
            }

            /// <summary>
            /// 连接名称
            /// </summary>
            public string Name { get; private set; }

            /// <summary>
            /// 连接字符串
            /// </summary>
            public string ConnectionString { get; private set; }

            /// <summary>
            /// 适配器名称
            /// </summary>
            public string ProviderName { get; private set; }
        }

        #endregion -- 索引器类
    }
}