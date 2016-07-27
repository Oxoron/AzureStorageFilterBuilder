using System;
using System.Globalization;
using System.Xml;

namespace AzureStorageFilterBuilder
{


    /* This type contains methods for presenting constants in requests.
     For.ex., guid constant is presented as guid'a455c695-df98-5678-aaaa-81d3367e5a34'
     and bool are presented as true\false.
     See format rules here: https://msdn.microsoft.com/en-us/library/azure/dd894031.aspx */

    internal static class FormatHelper
    {
        public static string AsTypedConstant(this string value)
        {
            return $"'{value}'";
        }

        public static string AsTypedConstant(this int value)
        {
            return value.ToString();
        }

        public static string AsTypedConstant(this long value)
        {
            return value.ToString();
        }

        public static string AsTypedConstant(this bool value)
        {
            return value ? "true" : "false";
        }
        
        public static string AsTypedConstant(this DateTime value)
        {
            // msdn recommends: https://msdn.microsoft.com/en-us/library/azure/dd894027.aspx
            return $"datetime'{XmlConvert.ToString(value.ToUniversalTime(), XmlDateTimeSerializationMode.RoundtripKind)}'";
        }

        public static string AsTypedConstant(this Guid value)
        {
            return $"guid'{value}'";
        }

        public static string AsTypedConstant(this double value)
        {
            // http://stackoverflow.com/questions/4076789/converting-double-to-string-with-n-decimals-dot-as-decimal-separator-and-no-th
            NumberFormatInfo nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};
            return value.ToString(nfi);
        }
    }
}