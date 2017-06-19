using System;
using System.Collections.Generic;
using System.Reflection;

namespace GNF.Common.Utility
{
    public class AttributeUtility
    {
        /// <summary>
        /// 获取枚举特性集合列表
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <returns>返回指定特性对象集合</returns>
        public static IEnumerable<TArrtibute> GetEnumAttributes<TArrtibute>(Enum obj, bool inhert = false) where TArrtibute : Attribute
        {
            var type = obj.GetType();
            if (type.IsEnum)
            {
                var fields = type.GetFields();
                foreach (var fd in fields)
                {
                    var tas = (TArrtibute[])fd.GetCustomAttributes(typeof(TArrtibute), inhert);
                    if (tas.Length > 0)
                    {
                        yield return tas[0];
                    }
                }
            }
        }

        /// <summary>
        /// 获取枚举特性集合字典    
        /// </summary>
        /// <typeparam name="TArrtibute">特性类型类</typeparam>
        /// <typeparam name="TEnum">枚举泛型</typeparam>
        /// <returns>返回指定特性对象字典</returns>
        public static IDictionary<TEnum, TArrtibute> GetEnumAttributeDictionary<TEnum, TArrtibute>(bool inhert = false) where TArrtibute : Attribute
        {
            IDictionary<TEnum, TArrtibute> dictionary = new Dictionary<TEnum, TArrtibute>();
            var type = typeof(TEnum);
            if (type.IsEnum)
            {
                var fields = type.GetFields();
                foreach (var fd in fields)
                {
                    if (fd.FieldType.IsEnum)
                    {
                        var enumItem = (TEnum)fd.GetValue(type);
                        var tas = (TArrtibute[])fd.GetCustomAttributes(typeof(TArrtibute), inhert);
                        if (tas.Length > 0)
                        {
                            dictionary.Add(enumItem, tas[0]);
                        }
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 获取枚举特性集合字典
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <param name="obj">枚举对象</param>
        /// <param name="inhert">是否找继承类</param>
        /// <returns>返回指定特性对象字典</returns>
        public static IDictionary<Enum, TArrtibute> GetEnumAttributeDictionary<TArrtibute>(Enum obj, bool inhert = false) where TArrtibute : Attribute
        {
            IDictionary<Enum, TArrtibute> dictionary = new Dictionary<Enum, TArrtibute>();
            var type = obj.GetType();
            if (type.IsEnum)
            {
                var fields = type.GetFields();
                foreach (var fd in fields)
                {
                    if (fd.FieldType.IsEnum)
                    {
                        var enumItem = (Enum)fd.GetValue(type);
                        var tas = (TArrtibute[])fd.GetCustomAttributes(typeof(TArrtibute), inhert);
                        if (tas.Length > 0)
                        {
                            dictionary.Add(enumItem, tas[0]);
                        }
                    }
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 获取特性对象
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <param name="fieldInfo">类字段</param>
        /// <param name="inhert">是否找继承类</param>
        /// <returns>返回指定特性对象</returns>
        public static TArrtibute GetAttribute<TArrtibute>(FieldInfo fieldInfo, bool inhert = false) where TArrtibute : Attribute
        {
            var attrs = fieldInfo.GetCustomAttributes(typeof(TArrtibute), inhert) as TArrtibute[];
            return attrs?.Length > 0 ? attrs[0] : null;
        }

        /// <summary>
        /// 获取特性对象
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <param name="propertyInfo">类属性</param>
        /// <param name="inhert">是否找继承类</param>
        /// <returns>返回指定特性对象</returns>
        public static TArrtibute GetAttribute<TArrtibute>(PropertyInfo propertyInfo, bool inhert = false) where TArrtibute : Attribute
        {
            var attrs = propertyInfo.GetCustomAttributes(typeof(TArrtibute), inhert) as TArrtibute[];
            return attrs?.Length > 0 ? attrs[0] : null;
        }

        /// <summary>
        /// 获取特性对象
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <param name="methodInfo">方法信息</param>
        /// <param name="inhert">是否找继承类</param>
        /// <returns>返回指定特性对象</returns>
        public static TArrtibute GetAttribute<TArrtibute>(MethodInfo methodInfo, bool inhert = false) where TArrtibute : Attribute
        {
            var attrs = methodInfo.GetCustomAttributes(typeof(TArrtibute), inhert) as TArrtibute[];
            return attrs?.Length > 0 ? attrs[0] : null;
        }

        /// <summary>
        /// 获取特性对象
        /// </summary>
        /// <typeparam name="TArrtibute">指定特性类</typeparam>
        /// <param name="parameterInfo">参数信息</param>
        /// <param name="inhert">是否找继承类</param>
        /// <returns>返回指定特性对象</returns>
        public static TArrtibute GetAttribute<TArrtibute>(ParameterInfo parameterInfo, bool inhert = false) where TArrtibute : Attribute
        {
            var attrs = parameterInfo.GetCustomAttributes(typeof(TArrtibute), inhert) as TArrtibute[];
            return attrs?.Length > 0 ? attrs[0] : null;
        }
    }
}
