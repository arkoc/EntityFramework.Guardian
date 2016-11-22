// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// ModifyPolicyResult that result from <see cref="IAlteringPolicy.Check(AlteringPolicyContext)"/> method"/> 
    /// </summary>
    public class AlteringPolicyResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlteringPolicyResult"/> class.
        /// </summary>
        public AlteringPolicyResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlteringPolicyResult"/> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="errorMessage">The error message.</param>
        public AlteringPolicyResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
