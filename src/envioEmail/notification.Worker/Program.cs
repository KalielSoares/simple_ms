using Microsoft.EntityFrameworkCore;
using notification.Application.Interfaces;
using notification.Application.UseCases;
using notification.Infrastructure.Persistence;
using notification.Infrastructure.Persistence.Repository;
using notification.Worker;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 31))));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<CreateUserUseCase>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); 
        Console.WriteLine("--- Migrations aplicadas com sucesso ---");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--- Erro ao aplicar migrations: {ex.Message} ---");
    }
}

host.Run();