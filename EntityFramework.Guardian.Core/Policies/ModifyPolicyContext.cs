using EntityFramework.Guardian.Entities;
using EntityFramework.Guardian.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace EntityFramework.Guardian.Policies
{
    /// <summary>
    /// Context used in <see cref="IModifyPolicy.Check(ModifyPolicyContext)"/>
    /// </summary>
    public class ModifyPolicyContext
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        /// <value>
        /// The kernel.
        /// </value>
        public GuardianKernel Kernel { get; set; }

        /// <summary>
        /// Gets or sets the name of the entity type.
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Gets or sets the entity row key.
        /// </summary>
        /// <value>
        /// The entity row key.
        /// </value>
        public string EntityRowKey { get; set; }

        /// <summary>
        /// Gets or sets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        public AccessTypes AccessType { get; set; }

        /// <summary>
        /// Gets or sets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IProtectableObject Entity { get; set; }

        /// <summary>
        /// Gets or sets the modified properties.
        /// </summary>
        /// <value>
        /// The modified properties.
        /// </value>
        public List<string> ModifiedProperties { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the general permissions.
        /// </summary>
        /// <value>
        /// The general permissions.
        /// </value>
        public List<IPermission> GeneralPermissions { get; set; } = new List<IPermission>();

        /// <summary>
        /// Gets or sets the row level permissions.
        /// </summary>
        /// <value>
        /// The row level permissions.
        /// </value>
        public List<IRowPermission> RowLevelPermissions { get; set; } = new List<IRowPermission>();

        /// <summary>
        /// Gets or sets the column restrictions.
        /// </summary>
        /// <value>
        /// The column restrictions.
        /// </value>
        public List<IColumnRestriction> ColumnRestrictions { get; set; } = new List<IColumnRestriction>();



        /// <summary>
        /// Creates context for specified entity, accesstype and kernel
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="kernel">The kernel.</param>
        /// <param name="modifiedProperties">The modified properties.</param>
        /// <returns></returns>
        public static ModifyPolicyContext For(GuardianKernel kernel, IObjectAccessEntry entry, List<string> modifiedProperties)
        {
            var entityRowKey = kernel.EntityKeyProvider.GetKey(entry.Entity);
            var entityTypeName = entry.Entity.GetType().Name;

            var generalPermissions = kernel.Principal.GetGeneralPermissions(entityTypeName, entry.AccessType);
            var rowLevelPermissions = kernel.Principal.GetRowLevelPermissions(
                entityTypeName,
                entry.AccessType,
                entityRowKey);

            var columnLevelRestrictionInGeneral = generalPermissions.SelectColumnRestrictions();
            var columnLevelRestrictionInRow = rowLevelPermissions.SelectColumnRestrictions();
            var columnLevelRestrictions = columnLevelRestrictionInGeneral.Concat(columnLevelRestrictionInRow).ToList();

            var policyContext = new ModifyPolicyContext()
            {
                Kernel = kernel,
                AccessType = entry.AccessType,
                Entity = entry.Entity,
                EntityRowKey = entityRowKey,
                EntityTypeName = entityTypeName,
                ModifiedProperties = modifiedProperties,
                GeneralPermissions = generalPermissions,
                RowLevelPermissions = rowLevelPermissions,
                ColumnRestrictions = columnLevelRestrictions
            };

            return policyContext;
        }
    }
}
