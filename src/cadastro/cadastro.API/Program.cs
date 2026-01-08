using cadastro.Application.Interfaces;
using cadastro.Application.UseCase;
using cadastro.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<ICreateUser, CreateUserUseCase>();
builder.Services.AddScoped<IMensageria, RabbitMqPublisher>();

var app = builder.Build();

app.MapControllers();

app.Run();
