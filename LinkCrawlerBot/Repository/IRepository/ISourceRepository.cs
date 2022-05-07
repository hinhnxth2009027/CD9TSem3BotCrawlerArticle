using System.Collections.Generic;
using LinkCrawlerBot.Entity;

namespace LinkCrawlerBot.Repository.IRepository
{
    public interface ISourceRepository
    {
        List<Source> FindAll();
    }
}