using System;
using System.Text;
using LinkCrawlerBot.Queue.IQueue;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace LinkCrawlerBot.Queue
{
    public class QueueArticle : IQueueArticle
    {
        public void PushArticle(RabbitMQArticle rabbitMqArticle)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "crawler",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rabbitMqArticle));
            
            channel.BasicPublish(exchange: "",
                routingKey: "crawler",
                basicProperties: null,
                body: body);
        }
    }
}