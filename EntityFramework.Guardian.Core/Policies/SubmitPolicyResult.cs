// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// SubmitPolicyResult that result from <see cref="ISubmitPolicy.Check(SubmitProtectionContext)"/> method"/> 
    /// </summary>
    public class SubmitPolicyResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitPolicyResult"/> class.
        /// </summary>
        public SubmitPolicyResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitPolicyResult"/> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="errorMessage">The error message.</param>
        public SubmitPolicyResult(bool isSuccess, string errorMessage = null)
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
