using cadastro.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace cadastro.Infrastructure.Messaging;

public class RabbitMqPublisher : IMensageria
{

    public ConnectionFactory _factory;


    public RabbitMqPublisher(IConfiguration configuration)
    {
         _factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQ:Host"],
            Port = int.Parse(configuration["RabbitMQ:Port"]),
            UserName = configuration["RabbitMQ:Username"],
            Password = configuration["RabbitMQ:Password"]
        };
    }

    public async Task Send(string message)
    {
     
        using var connection = await _factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "user-created", 
                                        durable: true, 
                                        exclusive: false, 
                                        autoDelete: false,
                                        arguments: null);
        
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty,
                                        routingKey:"user-created",
                                        body: body);
        
    }

}
