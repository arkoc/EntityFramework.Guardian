namespace EntityFramework.Guardian.Entities
{
    public interface IPermission
    {
        /// <summary>
        /// Gets or sets the name of the entity type.
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        string EntityTypeName { get; set; }

        /// <summary>
        /// Gets or sets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        AccessTypes AccessType { get; set; }
    }
}
