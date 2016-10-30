namespace EntityFramework.Guardian.Core.Models
{
    public interface IPermission
    {
        string EntityTypeName { get; set; }
        AccessTypes AccessType { get; set; }
    }
}
