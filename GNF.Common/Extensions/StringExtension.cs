using System;
using System.Text;

namespace GNF.Common.Extensions
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 转换到GUID
        /// </summary>
        /// <param name="value">传递字符串值</param>
        /// <returns>返回GUID值</returns>
        public static Guid ToGuid(this string value)
        {
            return !Guid.TryParse(value, out Guid val) ? Guid.Empty : val;
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="value">传递二级制数组值</param>
        /// <param name="encoding">转换的编码</param>
        /// <returns>返回字符串</returns>
        public static string ToString(this byte[] value, Encoding encoding)
        {
            if (value == null) return null;
            if (value.Length == 0) return string.Empty;
            if (encoding != null) return encoding.GetString(value);
            return Encoding.Default.GetString(value);
        }

        /// <summary>
        /// 转换成双精度浮点数
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="value">传递字符串值</param>
        /// <param name="defaultValue">默认返回0</param>
        /// <returns>返回双精度浮点数</returns>
        public static double ToDouble(this string value, double defaultValue = 0)
        {
            return !double.TryParse(value, out double val) ? defaultValue : val;
        }

        /// <summary>
        /// 转换成十进制数，转换错了，默认返回0
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="value">传递字符串值</param>
        /// <param name="defaultValue">默认返回0</param>
        /// <returns>返回十进制数</returns>
        public static decimal ToDecimal(this string value,decimal defaultValue = 0)
        {
            return !decimal.TryParse(value, out decimal val) ? defaultValue : val;
        }

        /// <summary>
        /// 转换成整数
        /// </summary>
        /// <param name="value">传递字符串值</param>
        /// <param name="defaultValue">默认返回0</param>
        /// <returns>返回整型数字</returns>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            return !int.TryParse(value, out int val) ? defaultValue : val;
        }

        /// <summary>
        /// 转换二进制
        /// </summary>
        /// <param name="value">传递字符串值</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>返回二进制数组</returns>
        public static byte[] ToBytes(this string value, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (encoding != null) return encoding.GetBytes(value);
            return Encoding.Default.GetBytes(value);
        }

        /// <summary>
        /// 转换到Bool值
        /// </summary>
        /// <param name="value">传参字符串值</param>
        /// <returns>返回bool值</returns>
        public static bool ToBool(this string value)
        {
            return value.Equals("true", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
