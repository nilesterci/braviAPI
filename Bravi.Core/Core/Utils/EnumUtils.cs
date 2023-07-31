using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravi.Domain.Core.Utils
{
    public static class EnumUtils
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            if (Attribute.GetCustomAttribute(field!, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                return attribute.Description;
            }

            return string.Empty;
        }

        public static T GetValueFromDescription<T>(this T value, string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null)!;
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null)!;
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
            // Or return default(T);
        }
    }
}
