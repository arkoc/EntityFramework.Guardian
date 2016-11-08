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
        /// Initializes a new instance of the <see cref="ModifyPolicyContext"/> class.
        /// </summary>
        private ModifyPolicyContext()
        {
        }

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
        /// Gets or sets the permissions.
        /// </summary>
        /// <value>
        /// The permissions.
        /// </value>
        public Permissions Permissions { get; set; }

        /// <summary>
        /// Creates context for specified entity, accesstype and kernel
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="kernel">The kernel.</param>
        /// <param name="modifiedProperties">The modified properties.</param>
        /// <returns></returns>
        public static ModifyPolicyContext For(GuardianKernel kernel, IObjectAccessEntry entry, List<string> modifiedProperties)
        {
            var entityRowKey = kernel.Services.EntityKeyProvider.GetKey(entry.Entity);
            var entityTypeName = entry.Entity.GetType().Name;

            var generalPermissions = kernel.Principal.GetGeneralPermissions(entityTypeName, entry.AccessType);
            var rowLevelPermissions = kernel.Principal.GetRowLevelPermissions(
                entityTypeName,
                entry.AccessType,
                entityRowKey);

            var columnLevelRestrictionInGeneral = generalPermissions.SelectColumnRestrictions();
            var columnLevelRestrictionInRow = rowLevelPermissions.SelectColumnRestrictions();
            var columnLevelRestrictions = columnLevelRestrictionInGeneral.Concat(columnLevelRestrictionInRow).ToList();

            var permissions = new Permissions()
            {
                GeneralPermissions = generalPermissions,
                RowLevelPermissions = rowLevelPermissions,
                ColumnRestrictions = columnLevelRestrictions
            };

            var policyContext = new ModifyPolicyContext()
            {
                Kernel = kernel,
                AccessType = entry.AccessType,
                Entity = entry.Entity,
                EntityRowKey = entityRowKey,
                EntityTypeName = entityTypeName,
                ModifiedProperties = modifiedProperties,
                Permissions = permissions
            };

            return policyContext;
        }
    }
}
