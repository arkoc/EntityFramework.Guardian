using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
