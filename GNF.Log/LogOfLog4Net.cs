using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using log4net;

namespace GNF.Log
{
    public class LogOfLog4Net
    {

        static ILog GetLog(Type type)
        {
            return LogManager.GetLogger(type);
        }

        static string BuildMessage(string message, params object[] parameters)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"消息：{message}");
            stringBuilder.AppendLine($"参数：{SerializeOfJson.Serialize(parameters)}");
            return stringBuilder.ToString();
        }

        public static void Warn(Type type, string message, Exception exception, params object[] parameters)
        {
            var msg = BuildMessage(message, parameters);
            GetLog(type).Warn(msg, exception);
        }

        public static void Error(Type type, string message, Exception exception, params object[] parameters)
        {
            var msg = BuildMessage(message, parameters);
            GetLog(type).Error(msg, exception);
        }
    }
}
