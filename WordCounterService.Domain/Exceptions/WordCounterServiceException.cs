using System;

namespace WordCounterService.Domain.Exceptions
{
    /// <summary>
    /// A custom exception created to throw application errors.
    /// </summary>
    public class WordCounterServiceException : Exception
    {
        /// <inheritdoc />
        public WordCounterServiceException()
        {
        }

        /// <inheritdoc />
        public WordCounterServiceException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        public WordCounterServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
