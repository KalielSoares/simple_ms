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


    await channel.ExchangeDeclareAsync(exchange: "usuarios.exchange", type: ExchangeType.Direct);

    await channel.QueueDeclareAsync(queue: "usuario-cadastrado", 
                                    durable: true, 
                                    exclusive: false, 
                                    autoDelete: false);
    
    await channel.QueueBindAsync(queue: "usuario-cadastrado", 
                                 exchange: "usuarios.exchange", 
                                 routingKey: "usuario-cadastrado");

    var body = Encoding.UTF8.GetBytes(message);

    await channel.BasicPublishAsync(exchange: "usuarios.exchange",
                                    routingKey: "usuario-cadastrado",
                                    body: body);
}

}
