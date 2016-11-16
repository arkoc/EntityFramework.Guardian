Welcome to EntityFrameowrk.Guardian
===================================

EntityFramework.Guardian is a extension point for Entity Framework DbContext in order to implement Database Security by hooking database operations.

It enables the following features in your applications:

Build-in Interfaces
^^^^^^^^^^^^^^^^^^^
Build-in entity interfaces for implementing database security

Database Operations Hooking system
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
Centralized mechanism for hooking database operations

General Security
^^^^^^^^^^^^^^^^
Restrict/grant access by entity type and access type\*

Row-Level Security
^^^^^^^^^^^^^^^^^^
Restrict/grant access by row identificator and access type\*

Column-Level Security
^^^^^^^^^^^^^^^^^^^^^
Restrict/grant access by column names and access type\*

Customization
^^^^^^^^^^^^^
Many aspect of EntityFrameowrk.Guardian can be customized to fit **your** needs.

**access types** : *get, add, update, delete*

   :caption: Introduction

   intro/big_picture
   intro/installation
   intro/configuration

.. toctree::
.. toctree::
   :maxdepth: 2
   :hidden:
   :maxdepth: 2
   :hidden:
   :caption: Architecture

   architecture/guardian_kernel
   architecture/hooking_system
   architecture/permission_service
   architecture/guards
   architecture/policies
   architecture/misc

