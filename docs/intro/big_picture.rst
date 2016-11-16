Big Picture
===========

What is problem?
^^^^^^^^^^^^^^^^

In some kind of applications we need to restrict access to specific tables and/or rows and/or columns based on some context (e.g. User Permissions, Application Permissions and so on). 

And there is no build-in way or some kind of library to implement such kind of things in **application layer**.

How we solve this?
^^^^^^^^^^^^^^^^^^

We introduce EntityFramework.Guardian. This solution allows you to implement database security in easy and **right** way.
Guardian introduces set of interfaces for implementing entitites that are presenting permissions that are linked to entity type, row and columns. 

General part of guardian is `GuardianKernel`. This object holds following components:

* :ref:`Guards <refGuards>` - ModifyGuard and RetrieveGuard, these guards invoke defined policies
* :ref:`Policies <refPolicies>` - Policies define set of rules to be applyed to entities and permissions
* :ref:`PermissionsService <refPermissionsService>` - Service for retrieving general and row level permissions.
* :ref:`EntityKeyProvider<refEntityKeyProvider>` - Service for returning string representation of passed entity identificator

All these components are configurable.
