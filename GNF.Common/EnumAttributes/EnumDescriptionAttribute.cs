using System;

namespace GNF.Common.EnumAttributes
{
    /// <summary>
    /// 枚举描述
    /// </summary>
    public class EnumDescriptionAttribute: Attribute
    {
        public EnumDescriptionAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
