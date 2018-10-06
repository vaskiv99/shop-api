using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using ShopService.Common.Enums;

namespace ShopService.Common.Utils
{
    public static class EnumUtil
    {
        private static IDictionary<int, string> ErrorDescriptions { get; }

        static EnumUtil()
        {
            ErrorDescriptions = new Dictionary<int, string>();
            var errors = Enum.GetValues(typeof(ErrorType));
            
            foreach (ErrorType enumName in errors)
            {
                ErrorDescriptions.Add((int)enumName, GetEnumDescription(enumName));
            }
        }

        public static string GetErrorTypeDescription(ErrorType errorType)
        {
            return ErrorDescriptions[(int) errorType];
        }

        #region helpers

        private static string GetEnumDescription(Enum value)
        {
            var field = value.GetType().GetRuntimeField(value.ToString());
            var attributes =
                (DescriptionAttribute[])field.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        #endregion
    }
}