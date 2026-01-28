using cadastro.Application.Interfaces;
using cadastro.Application.Services;
using cadastro.Application.UseCase;
using cadastro.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// -----------------------------------------------------------------------
// MAPEAMENTO DE DEPENDÊNCIAS (DI)
// Aqui definimos qual classe de regra de negócio (UseCase) resolve cada interface.
// -----------------------------------------------------------------------

// <summary>
// Registra o UseCase de criação de usuário.
// DE: Interface ICreateUser (usada nos Controllers)
// PARA: Implementação CreateUserUseCase (onde reside a lógica de envio para o RabbitMQ)
// LIFE CYCLE: Scoped (Uma instância por requisição HTTP)
// </summary>
builder.Services.AddScoped<ICreateUser, CreateUserUseCase>();



builder.Services.AddScoped<UserEventFactory>();

builder.Services.AddSingleton<IMensageria, RabbitMqPublisher>();


var app = builder.Build();

app.MapControllers();

app.Run();
