# EntityFramework.Guardian #

[![Build status](https://ci.appveyor.com/api/projects/status/r6rt7t75lf9r9cih?svg=true)](https://ci.appveyor.com/project/arkoc/entityframework-guardian)

## Overview ##

EntityFramework.Guardian is a extension point for Entity Framework DbContext in order to implement Database Security by hooking database operations.

Documentation is available here [entityframeworkguardian.readthedocs.io](http://entityframeworkguardian.readthedocs.io)

## Getting started ##
Install [NuGet package](https://www.nuget.org/packages/EntityFramework.Guardian/) from [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console):
```
PM> Install-Package EntityFramework.Guardian
```

```c#
var dbContext = new AppDbContext();
var guardianKernel = new GuardianKernel();
dbContext.GuardBy(guardianKernel);

guardianKernel.UseInMemoryPermission(...)
  
```

After call of `GuardBy` DbContext will not allow do anything in database. After call of `UseInMemoryPermission` function, `DbContext` will allow only operation allowed by permission passed to it.
