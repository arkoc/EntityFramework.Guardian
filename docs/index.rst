Welcome to EntityFrameowrk.Guardian
==========================

EntityFramework.Guardian is a extension point for Entity Framework DbContext in order to implement Database Security by hooking database operations.

It enables the following features in your applications:

Build-in Interfaces
^^^^^^^^^^^^^^^^^^^^^^^^^^^
Build-in interfaces for implementing database security

Database Operations Hooking system
^^^^^^^^^^^^^^^^^^^^^^^^^
Centralized mechanism for hooking modifying/retrieving events

General Security
^^^^^^^^^^^^^^^^^^^^^^^
Restrict/grant access by entity type and access type (get, add, update, delete)

Row-Level Security
^^^^^^^^^^^^^^^^^^^^^^^
Restrict/grant access by row identificator and access type (get, add, update, delete)

Column-Level Security
^^^^^^^^^^^^^^^^^^
Restrict/grant access by column names and access type (get, add, update, delete)

Customization
^^^^^^^^^^^^^^^^^^^^^^
Many aspect of EntityFrameowrk.Guardian can be customized to fit **your** needs.


.. toctree::
   :maxdepth: 2
   :hidden:
   :caption: Introduction

   intro/big_picture
   intro/installation
   intro/configuration

.. toctree::
   :maxdepth: 2
   :hidden:
   :caption: Architecture

   architecture/guardian_kernel
   architecture/hooking_system
   architecture/permission_service
   architecture/guards
   architecture/policies
   architecture/misc

