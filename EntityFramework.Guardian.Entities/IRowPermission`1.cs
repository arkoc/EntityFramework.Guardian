using System.Collections.Generic;

namespace EntityFramework.Guardian.Entities
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TColumnRestriction">The type of the column restriction.</typeparam>
    /// <seealso cref="EntityFramework.Guardian.Entities.IRowPermission" />
    public interface IRowPermission<TColumnRestriction> : IRowPermission
        where TColumnRestriction : IColumnRestriction
    {
        /// <summary>
        /// Gets the column restrictions.
        /// </summary>
        /// <value>
        /// The column restrictions.
        /// </value>
        ICollection<TColumnRestriction> ColumnRestrictions { get; }
    }
}
