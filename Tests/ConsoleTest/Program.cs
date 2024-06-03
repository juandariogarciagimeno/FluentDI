using Microsoft.Extensions.Hosting;
using FluentDI;

var builder = Host.CreateDefaultBuilder();
builder.AddFluentDI();

var app = builder.Build();

app.Run();