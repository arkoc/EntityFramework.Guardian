// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Data.Entity;
using System.Runtime.Serialization;

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
        public AccessDeniedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public AccessDeniedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessDeniedException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected AccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
