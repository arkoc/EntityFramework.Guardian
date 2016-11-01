using System.Collections.Generic;

namespace EntityFramework.Guardian.Policies
{
    public class RetrievePolicyResult
    {
        public RetrievePolicyResult()
        {
        }

        public RetrievePolicyResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
        public List<string> RestrictedProperties { get; set; } = new List<string>();
    }
}
