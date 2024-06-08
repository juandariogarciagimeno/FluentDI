using Microsoft.Extensions.Hosting;
using FluentDI;

var builder = Host.CreateDefaultBuilder();
builder.AddFluentDI();
builder.UseConsoleLifetime();


var app = builder.Build();


app.Run();