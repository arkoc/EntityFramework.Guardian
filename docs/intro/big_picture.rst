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

* Guards - ModifyGuard and RetrieveGuard, these guards invoke defined policies
* Policies - Policies define set of rules to be applyed to entities and permissions
* PermissionService - Service for retrieving general and row level permissions.
* EntityKeyProvider - Service for returning string representation of passed entity identificator

All these components are configurable.
