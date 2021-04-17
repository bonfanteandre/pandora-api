using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Pandora.Core.Helpers
{
    public static class EnumHelper
    {
        public static string ToDescription(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            FieldInfo[] fields = enumType.GetFields();
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(enumValue);
                if (value.Equals(enumValue))
                {
                    Type descriptionType = typeof(DescriptionAttribute);
                    object[] descriptionAttribute = field.GetCustomAttributes(descriptionType, true);

                    if (descriptionAttribute.Length == 0)
                    {
                        return field.Name;
                    }

                    return ((DescriptionAttribute) descriptionAttribute[0]).Description;
                }
            }

            return string.Empty;
        }
    }
}
