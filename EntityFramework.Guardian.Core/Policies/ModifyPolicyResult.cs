namespace EntityFramework.Guardian.Policies
{
    public class ModifyPolicyResult
    {
        public ModifyPolicyResult()
        {

        }

        public ModifyPolicyResult(bool isSuccess, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
