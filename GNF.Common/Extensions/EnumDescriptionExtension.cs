using System;
using System.Collections.Generic;
using System.Linq;
using GNF.Common.EnumAttributes;
using GNF.Common.Utility;

namespace GNF.Common.Extensions
{
    public static class EnumDescriptionExtension
    {
        static readonly IDictionary<string, IDictionary<Enum, EnumDescriptionAttribute>> _dictionary = new Dictionary<string, IDictionary<Enum, EnumDescriptionAttribute>>();
        static readonly object _lockObj = new object();

        /// <summary>
        /// 获取描述内容
        /// </summary>
        /// <param name="enumItem">枚举项</param>
        /// <returns>返回描述字符串</returns>
        public static string GetDescriptionText(this Enum enumItem)
        {
            var key = enumItem;
            var fullTypeName = enumItem.GetType().FullName;
            lock (_lockObj)
            {
                if (_dictionary.ContainsKey(fullTypeName))
                {
                    if (_dictionary[fullTypeName].ContainsKey(key))
                    {
                        return _dictionary[fullTypeName][key].Text;
                    }
                    return string.Empty;
                }
                var enumAttributeDictionary = AttributeUtility.GetEnumAttributeDictionary<EnumDescriptionAttribute>(enumItem);
                if (enumAttributeDictionary.Count == 0) return string.Empty;
                _dictionary.Add(fullTypeName, enumAttributeDictionary);
                return enumAttributeDictionary[key].Text;
            }
        }

        public static IDictionary<TEnum, string> ToEnumDescriptionDictionary<TEnum>()
        {
            var list = AttributeUtility.GetEnumAttributeDictionary<TEnum, EnumDescriptionAttribute>();
            return list.ToDictionary(a => a.Key, b => b.Value.Text);
        }
    }
}
