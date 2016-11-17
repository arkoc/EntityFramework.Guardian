Installation
============

Guardian is separated in two packages: 

* EntityFramework.Guardian - include everything needed for guardian (including dependecy to EntityFramework.Guardian.Entities)
* EntityFramework.Guardian.Entities - includes all permission entity interfaces

Install `NuGet Guardian package <https://www.nuget.org/packages/EntityFramework.Guardian/>`_ from Package Manager Console:

``PM> Install-Package EntityFramework.Guardian``


If you want only interfaces of permission entities ( in case your entities are in separate project ) 
you can install  `NuGet Entities package <https://www.nuget.org/packages/EntityFramework.Guardian.Entities/>`_ from Package Manager Console:

``PM> Install-Package EntityFramework.Guardian.Entities``

It includes only interfaces to permission entities.
