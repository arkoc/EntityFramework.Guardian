Big Picture
===========

What is problem?
^^^^^^^^^^^^^^^^

In some kind of applications we need to restrict access to specific tables and/or rows and/or columns based on some context (e.g. User Permissions, Application Permissions and so on). 

And there is no build-in way or some kind of library to implement such kind of things in **application layer**.

How we solve this?
^^^^^^^^^^^^^^^^^^

We introduce EntityFramework.Guardian. This solution allows you to implement database security in easy and **right** way.

Guardian introduces access types:

.. code-block:: c#
    /// <summary>
    /// Access Types
    /// </summary>
    public enum AccessTypes
    {
        /// <summary>
        /// The get
        /// </summary>
        Get,
        /// <summary>
        /// The add
        /// </summary>
        Add,
        /// <summary>
        /// The update
        /// </summary>
        Update,
        /// <summary>
        /// The delete
        /// </summary>
        Delete
    }

Also Guardian introduces set of interfaces for implementing entitites that are presenting permissions that are linked to entity type, row and columns.

Here is simple entity interface that presents permission that is linked to entity type.

.. code-block:: c#
    /// <summary>
    /// Entity interfce presenting permission linked to entitytype
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// Gets the name of the entity type.
        /// </summary>
        /// <value>
        /// The name of the entity type.
        /// </value>
        string EntityTypeName { get; }

        /// <summary>
        /// Gets the type of the access.
        /// </summary>
        /// <value>
        /// The type of the access.
        /// </value>
        AccessTypes AccessType { get; }
    }

Guardian set two guards for Modifing and Retrieving operations:

* Retrieve ( *get* ) guard is set in ObjectContext.ObjectMaterialized event.
* Modify ( *add, update, delete* ) guard is set in ObjectContext.SavingChanges event.

General part of guardian is `GuardianKernel`. This object holds following components:

* :ref:`Guards <refGuards>` - ModifyGuard and RetrieveGuard, these guards invoke defined policies
* :ref:`Policies <refPolicies>` - Policies define set of rules to be applyed to entities and permissions
* :ref:`PermissionsService <refPermissionsService>` - Service for retrieving general and row level permissions.
* :ref:`EntityKeyProvider<refEntityKeyProvider>` - Service for returning string representation of passed entity identificator

All these components are configurable.
