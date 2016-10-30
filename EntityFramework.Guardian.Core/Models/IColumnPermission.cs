namespace EntityFramework.Guardian.Core.Models
{
    public interface IColumnRestriction
    {
        string EntityTypeName { get; set; }
        string PropertyName { get; set; }
        AccessTypes AccessType { get; set; }
    }
}
