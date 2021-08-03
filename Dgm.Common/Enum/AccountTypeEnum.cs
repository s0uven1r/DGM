using System.Collections.Generic;

namespace Dgm.Common.Enum
{
    public enum AccountTypeEnum
    {
        Credit = 1,
        Debit = 2,
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
    }
}
