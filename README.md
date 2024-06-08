```
______  _                      _   ______  _____ 
|  ___|| |                    | |  |  _  \|_   _|
| |_   | | _   _   ___  _ __  | |_ | | | |  | |  
|  _|  | || | | | / _ \| '_ \ | __|| | | |  | |  
| |    | || |_| ||  __/| | | || |_ | |/ /  _| |_ 
\_|    |_| \__,_| \___||_| |_| \__||___/   \___/ 
```
![Static Badge](https://img.shields.io/badge/MIT-license-blue?style=flat)
![Static Badge](https://img.shields.io/badge/.NET%20Core-blue?logo=dotnet)

---

## What is FluentDI
FluentDI is an utility library that allows for Dependency Injection through Attributes, making code cleaner and easier to find whether a service is being injected or not.

## How does it work?
First of all, after installing the package, FluentDI must be enabled during app building.

```csharp
using FluentDI;

WebApplication Application;

var builder = WebApplication.CreateBuilder(args);

//Add fluent Dependency Injection
builder.AddFluentDI();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

> FluentDI is compatible with regular Dependency Injection

Once enabled, in order to load the services, the Attribute `Injector` must be added to the desired services

```csharp
    [Injector<ITimeOfDayService>]
    public class EveningOfDay : ITimeOfDayService
    {}
```

If no value is provided, default lifetime is `Scoped`, this can be modified 

```csharp
    [Injector<IStatusService>(ServiceLifetime.Singleton)]
    public class StatusService : IStatusService
    {}
    ---
    [Injector<TimeService>(ServiceLifetime.Transient)]
    public class TimeService
    {}
```

Pretty simple right? :)
## Conditional Injection
Additionally, this library allows for injection of multiple implementations of a same interface, and decide which one to load based on a condition, supplied by a class implementation

```csharp
    public class TimeOfDaySelector : IRuntimeDependencySelector<ITimeOfDayService>
    {
        public Func<ITimeOfDayService, object[], bool> CanInject => (s, param) =>
        {
            if (param.Length == 0 || param[0] is not DateTime date)
                throw new ArgumentException("Param must be a date");

            var time = TimeOnly.FromDateTime(date);
            
            return time >= s.From && time <= s.To;
        };
    }
```
Then add the `Injector` attribute to all implementations
```csharp
    [Injector<ITimeOfDayService>]
    public class MorningOfDayService : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(8, 0, 0);

        public TimeOnly To => new TimeOnly(12, 0, 0);

        public string GetTimeOfDay()
        {
            return "It's daytime!";
        }
    }
```
```csharp
    [Injector<ITimeOfDayService>]
    public class NightOfDay : ITimeOfDayService
    {
        public TimeOnly From => new TimeOnly(18, 0, 0);

        public TimeOnly To => new TimeOnly(23, 59, 0);

        public string GetTimeOfDay()
        {
            return "It's nighttime!";
        }
    }
```

Lastly, when requesting for the service, it must be done through `DependencyFactory<>` class
```csharp
        public TimeOfDayController(IDependencyFactory<ITimeOfDayService> dependencyFactory)
        {
            this.TimeOfDayService = dependencyFactory.Get(DateTime.Now);
        }
```