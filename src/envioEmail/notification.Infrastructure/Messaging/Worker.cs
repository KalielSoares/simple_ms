using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using notification.Application.DTOs;
using notification.Application.Interfaces;
using notification.Domain.Entities;
using notification.Infrastructure.Persistence;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace cadastro.Infrastructure.Messaging;

public class Worker : BackgroundService
{
    private readonly IConfiguration _configuration;
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
        consumer.ReceivedAsync += async (model,ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);  
               
                var UserDTO = JsonSerializer.Deserialize<UserDTO>(message);



                var User = new User(UserDTO.UserId, UserDTO.Name, UserDTO.Email, UserDTO.CreatedAt);
                
            } catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        };
            await _channel.BasicConsumeAsync("usuario-cadastrado",
                                            autoAck: false,
                                            consumer: consumer);   
    }    
}
