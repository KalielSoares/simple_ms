using System.Text;
using System.Text.Json;
using notification.Application.DTOs;
using notification.Application.Interfaces;
using notification.Application.UseCases;
using notification.Domain.Entities;
using notification.Infrastructure.Persistence.Repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace notification.Worker;

public class Worker(IConfiguration configuration, IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IConfiguration _configuration = configuration;
    private IConnection _connection;
    private IChannel _channel;

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var _factory = new ConnectionFactory
        {
            HostName = _configuration["RabbitMQ:Host"],
            Port = int.Parse(_configuration["RabbitMQ:Port"]),
            UserName = _configuration["RabbitMQ:Username"],
            Password = _configuration["RabbitMQ:Password"]
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
              _connection = await _factory.CreateConnectionAsync();
              _channel = await _connection.CreateChannelAsync();
              break;   
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        await _channel.QueueDeclareAsync(queue: "usuario-cadastrado", 
                                        durable: true, 
                                        exclusive: false, 
                                        autoDelete: false,
                                        arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var useCase = scope.ServiceProvider.GetRequiredService<CreateUserUseCase>();
    
                try 
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var userDto = JsonSerializer.Deserialize<UserDTO>(message);

                    if (userDto != null)
                    {
                        await useCase.Execute(userDto);
            
                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                }
                catch (Exception ex)
                {
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue: true);
                }
            }
        };

        await _channel.BasicConsumeAsync("usuario-cadastrado", autoAck: false, consumer: consumer);
    }
}
