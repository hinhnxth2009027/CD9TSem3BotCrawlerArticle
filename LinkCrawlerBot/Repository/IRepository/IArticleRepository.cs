using System.Collections.Generic;
using System.Data.SqlClient;
using LinkCrawlerBot.Entity;

namespace LinkCrawlerBot.Repository.IRepository
{
    public interface IArticleRepository
    {
        List<Article> FinAll();
        Article FindByLink(string linkSource);

        Article Create(SqlDataReader data);
    }
}