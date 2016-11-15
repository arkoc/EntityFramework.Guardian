// Copyright (c) Aram Kocharyan. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// RetrievePolicyResult that result from <see cref="IRetrievePolicy.Check(RetrievePolicyContext)"/> method"/> 
    /// </summary>
    public class RetrievePolicyResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievePolicyResult"/> class.
        /// </summary>
        public RetrievePolicyResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetrievePolicyResult"/> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="errorMessage">The error message.</param>
        public RetrievePolicyResult(bool isSuccess, string errorMessage = null)
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

        /// <summary>
        /// Gets or sets the restricted properties.
        /// </summary>
        /// <value>
        /// The restricted properties.
        /// </value>
        public List<string> RestrictedProperties { get; set; } = new List<string>();
    }
}
