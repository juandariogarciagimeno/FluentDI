using FluentDI;

WebApplication Application;

var builder = WebApplication.CreateBuilder(args);

//Add fluent Dependency Injection
builder.AddFluentDI();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

Application = builder.Build();

if (Application.Environment.IsDevelopment())
{
    Application.UseSwagger();
    Application.UseSwaggerUI();
}

Application.UseAuthorization();

Application.MapControllers();

Application.Run();