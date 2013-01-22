Getting Started
================

Installation
-------------
ModelFramework is available as a [NuGet Package](http://nuget.org/packages/ModelFramework). 
To install it, open your Package Manager Console and type:
```
PM> Install-Package ModelFramework
```

Usage
-------

### Commands

TBD.

```csharp
public class RegisterUserCommand : Command
{
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    public void Execute()
    {
        // Command logic
    }
}
```

### Validation

TBD.

### Application Bus

TBD.

### Model Context

TBD ;)

Extensions
-----------

* [ASP.NET MVC](https://github.com/ChessOK/ModelFramework.Mvc)
* ASP.NET WebForms
* WCF
