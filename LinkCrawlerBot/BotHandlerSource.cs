using System;
using System.Threading.Tasks;
using LinkCrawlerBot.Queue;
using LinkCrawlerBot.Queue.IQueue;
using LinkCrawlerBot.Service;
using LinkCrawlerBot.Service.IService;
using Quartz;

namespace LinkCrawlerBot
{
    public class BotHandlerSource : IJob
    {
        private ISourceService _sourceService;
        private IQueueArticle _queueArticle;

        public BotHandlerSource()
        {
            _sourceService = new SourceService();
            _queueArticle = new QueueArticle();
        }
        
        public async Task Execute(IJobExecutionContext context)
        {
            var dataSource = _sourceService.FindAll();
            foreach (var source in dataSource)
            {
                var rabbitMQArticles = _sourceService.GetSubLink(source);
                foreach (var rabbitMQArticle in rabbitMQArticles)
                {
                    _queueArticle.PushArticle(rabbitMQArticle);
                }
            }
        }
    }
}