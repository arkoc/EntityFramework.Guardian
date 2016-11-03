namespace EntityFramework.Guardian.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IColumnRestriction
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        /// <value>
        /// The name of the property.
        /// </value>
        string PropertyName { get; set; }
    }
}
