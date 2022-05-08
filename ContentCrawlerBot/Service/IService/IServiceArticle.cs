using ContentCrawlerBot.Entity;
using ContentCrawlerBot.Queue;

namespace ContentCrawlerBot.Service.IService
{
    public interface IServiceArticle
    {
        Article Save(Article article);
        Article GetArticle(RabbitMqArticle _rabbitMqArticle);
    }
}