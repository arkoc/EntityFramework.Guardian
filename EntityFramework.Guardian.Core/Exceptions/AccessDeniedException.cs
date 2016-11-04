using System;
using System.Data.Entity;

namespace EntityFramework.Guardian.Exceptions
{
    /// <summary>
    /// Exception that is thrown in <see cref="DbContext.SaveChanges"/> when there is no permission
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class AccessDeniedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        public AccessDeniedException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AccessDeniedException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public AccessDeniedException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
