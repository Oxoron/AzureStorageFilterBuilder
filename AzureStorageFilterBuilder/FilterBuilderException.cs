using System;

namespace AzureStorageFilterBuilder
{
    public class FilterBuilderException : Exception
    {
        public FilterBuilderException()
        {
        }

        public FilterBuilderException(string message)
        : base(message)
        {
        }

        public FilterBuilderException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
