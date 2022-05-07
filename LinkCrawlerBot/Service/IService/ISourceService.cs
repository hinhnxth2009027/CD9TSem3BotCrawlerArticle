using System.Collections.Generic;
using LinkCrawlerBot.Entity;
using LinkCrawlerBot.Queue;

namespace LinkCrawlerBot.Service.IService
{
    public interface ISourceService
    {
        List<Source> FindAll();
        HashSet<RabbitMQArticle> GetSubLink(Source source);
    }
}