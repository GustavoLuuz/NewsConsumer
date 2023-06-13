using System.Text;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using NewsPub.Domain;

namespace NewsPubConsumer
{
    public class Consumer
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "solicitation_1",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var solicitacao = System.Text.Json.JsonSerializer.Deserialize<Solicitation>(message);
                    Console.WriteLine($" [x] Received {solicitacao.SolicitationNumber} |" +
                        $" {solicitacao.Email} |" +
                        $" {solicitacao.Message}");
                    
                };
                channel.BasicConsume(queue: "solicitation_1",
                     autoAck: true,
                     consumer: consumer);


                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}