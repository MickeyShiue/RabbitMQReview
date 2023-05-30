using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Send
{
    public class PublishSend
    {
        public void Send()
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Direct);

            var i = 0;
            while (true)
            {
                var message = $"log info:{i}";

                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "logs",
                    routingKey: string.Empty,
                    basicProperties: null,
                    body: body);

                Console.WriteLine($"Sent {message} Success");

                Thread.Sleep(1500);
                i++;
            }
        }
    }
}
