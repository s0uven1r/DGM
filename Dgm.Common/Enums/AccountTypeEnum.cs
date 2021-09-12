using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Dgm.Common.Enums
{
    public enum AccountTypeEnum
    {
        [Description("I")]
        Income = 1,
        [Description("E")]
        Expense = 2,
    }

    public static class AccountTypeEnumConversion
    {
        public static List<KeyValuePair<string, int>> GetEnumList()
        {
            var list = new List<KeyValuePair<string, int>>();
            foreach (var en in System.Enum.GetValues(typeof(AccountTypeEnum)))
            {
                list.Add(new KeyValuePair<string, int>(en.ToString(), (int)en));
            }
            return list;
        }

        public static string GetDescriptionByValue(int enumValue)
        {
            Type type = typeof(AccountTypeEnum);
            Array values = Enum.GetValues(type);
            foreach (int val in values)
            {
                if (val == enumValue)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
            return null; // could also return string.Empty
        }

        public static string GetDescription(this AccountTypeEnum statusEnum)
        {
            Type type = statusEnum.GetType();
            Array values = Enum.GetValues(type);
            int enumValue = (int)statusEnum;
            foreach (int val in values)
            {
                if (val == enumValue)
                {
                    var memInfo = type.GetMember(type.GetEnumName(val));
                    var descriptionAttribute = memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() as DescriptionAttribute;

                    if (descriptionAttribute != null)
                    {
                        return descriptionAttribute.Description;
                    }
                }
            }
            return null; // could also return string.Empty
        }

    }
}
