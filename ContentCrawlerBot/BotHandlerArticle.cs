using System.Threading.Tasks;
using ContentCrawlerBot.Queue;
using Quartz;

namespace ContentCrawlerBot
{
    public class BotHandlerArticle : IJob
    {
        private QueueArticle _queueArticle;
        public BotHandlerArticle()
        { 
            _queueArticle = new QueueArticle();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _queueArticle.GetRabbitMQArticle();
        }
    }
}