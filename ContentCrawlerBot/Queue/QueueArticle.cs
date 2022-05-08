using System;
using System.Text;
using ContentCrawlerBot.Queue.IQueue;
using ContentCrawlerBot.Service;
using ContentCrawlerBot.Service.IService;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ContentCrawlerBot.Queue
{
    public class QueueArticle : IQueueArticle
    {
        private IServiceArticle _serviceArticle;

        public QueueArticle()
        {
            _serviceArticle = new ServiceArticle();
        }
        public void GetRabbitMQArticle()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: "crawler",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var i = 0;
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var rabbitMqArticle = JsonToRabbitArticle(JObject.Parse(message));
                var article = _serviceArticle.GetArticle(rabbitMqArticle);
                _serviceArticle.Save(article);
            };
            channel.BasicConsume(queue: "crawler",
                autoAck: true,
                consumer: consumer);
            Console.ReadLine();
        }
        private static RabbitMqArticle JsonToRabbitArticle(JObject data)
        {
            return new RabbitMqArticle()
            {
                Url = (string)data["Url"],
                TitleDetailSelector = (string)data["TitleDetailSelector"],
                DescriptionSelector = (string)data["DescriptionSelector"],
                ContentDetailSelector = (string)data["ContentDetailSelector"],
                ThumbnailDetailSelector = (string)data["ThumbnailDetailSelector"],
                RemoveSelector = (string)data["RemoveSelector"],
                TagDetailSelector = (string)data["TagDetailSelector"],
                SourceId = Convert.ToInt32(data["SourceId"]),
                CategoryId = Convert.ToInt32(data["CategoryId"]),
            };
        }
    }
}