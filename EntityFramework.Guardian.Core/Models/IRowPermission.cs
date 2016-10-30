namespace EntityFramework.Guardian.Models
{
    public interface IRowPermission
    {
        string EntityTypeName { get; set; }
        string RowIdentifier { get; set; }
        AccessTypes AccessType { get; set; }
    }
}
