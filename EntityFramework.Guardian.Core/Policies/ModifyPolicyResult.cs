namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// ModifyPolicyResult that result from <see cref="IModifyProtectionPolicy.Check(ModifyPolicyContext, GuardianKernel)"/> method"/> 
    /// </summary>
    public class ModifyPolicyResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyPolicyResult"/> class.
        /// </summary>
        public ModifyPolicyResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModifyPolicyResult"/> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="errorMessage">The error message.</param>
        public ModifyPolicyResult(bool isSuccess, string errorMessage = null)
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
