using System.Collections.Generic;
using ContentCrawlerBot.Entity;

namespace ContentCrawlerBot.Repository.IRepository
{
    public interface IRepositoryArticle
    {
        List<Article> GetAll();
        Article FindArticleByUrl(string urlSource);
        Article Save(Article article);
    }
}