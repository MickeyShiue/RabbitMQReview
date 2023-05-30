using System.Text;
using RabbitMQ.Client;

namespace RabbitMQ.Send
{
    public class WorkerSend
    {
        public void Send()
        {
            var factory = new ConnectionFactory { HostName = "localhost", Port = 5672 };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "task_queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var i = 0;

            while (true)
            {
                var message = $"Hello World：{i}";
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchange: string.Empty,
                    routingKey: "task_queue",
                    basicProperties: properties,
                    body: body);

                Console.WriteLine($"Sent {message}");
                i++;
                Thread.Sleep(1000);
            }
        }
    }
}
