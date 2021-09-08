using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dgm.Common.Enums
{
    public enum RoleTypeEnum
    {
        [Description("EMP")]
        Employee = 1,
        [Description("CUST")]
        Customer = 2,
    }

    public static class RoleTypeEnumConversion
    {
        public static List<KeyValuePair<string, int>> GetEnumList()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var en in System.Enum.GetValues(typeof(RoleTypeEnum)))
            {
                list.Add(new KeyValuePair<string, int>(en.ToString(), (int)en));
            }
            return list;
        }

        public static string GetDescriptionByValue(int enumValue)
        {
            Type type = typeof(RoleTypeEnum);
            Array values = Enum.GetValues(type);
            foreach (int val in values)
            {
                if (val == enumValue)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));

                    if (memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
            return null; // could also return string.Empty
        }

        public static string GetDescription(this RoleTypeEnum statusEnum)
        {
            Type type = statusEnum.GetType();
            Array values = Enum.GetValues(type);
            int enumValue = (int)statusEnum;
            foreach (int val in values)
            {
                if (val == enumValue)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));

                    if (memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
            return null; // could also return string.Empty
        }

    }
}
