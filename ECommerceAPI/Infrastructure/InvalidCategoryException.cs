using System;

namespace ECommerceAPI.Infrastructure
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException() : base()
        {
        }

        public InvalidCategoryException(string message) : base(message)
        {
        }

        public InvalidCategoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}