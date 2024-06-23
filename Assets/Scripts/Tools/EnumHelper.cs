using System;
using System.Collections.Generic;
using System.Linq;

namespace Deege.Game.Tools
{
    public static class EnumHelper
    {
        // Return all enum values as a list of strings without excluding any value
        public static List<string> GetDisplayNames<TEnum>() where TEnum : Enum
        {
            var list = new List<string>();
            foreach (var value in Enum.GetValues(typeof(TEnum)))
            {
                list.Add(value.ToString());
            }
            return list;
        }

        public static bool IsValidEnumValue<TEnum>(string input) where TEnum : Enum
        {
            return Enum.GetNames(typeof(TEnum))
                       .Any(name => name.Equals(input, StringComparison.OrdinalIgnoreCase));
        }

        public static TEnum ParseEnum<TEnum>(string input) where TEnum : Enum
        {
            if (IsValidEnumValue<TEnum>(input))
            {
                return (TEnum)Enum.Parse(typeof(TEnum), input, true);
            }
            throw new ArgumentException($"The value '{input}' is not a valid {typeof(TEnum).Name}.");
        }
    }
}

