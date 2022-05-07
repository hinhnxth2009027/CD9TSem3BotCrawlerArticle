using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using LinkCrawlerBot.Entity;
using LinkCrawlerBot.Queue;
using LinkCrawlerBot.Repository;
using LinkCrawlerBot.Service.IService;

namespace LinkCrawlerBot.Service
{
    public class SourceService : ISourceService
    {
        private SourceRepository sourceRepository;
        private ArticleRepository articleRepository;
        public SourceService()
        {
            sourceRepository = new SourceRepository();
            articleRepository = new ArticleRepository();
        }
        public List<Source> FindAll()
        {
            articleRepository.TruncateArticles();
            return sourceRepository.FindAll();
        }

        public HashSet<RabbitMQArticle> GetSubLink(Source source)
        {
            var web = new HtmlWeb();
            HtmlDocument doc = web.Load((string) source.Link);
            var nodeList = doc.QuerySelectorAll((string) source.LinkSelector);
            HashSet<RabbitMQArticle> RabbitMQArticles = new HashSet<RabbitMQArticle>();
            
            foreach (var node in nodeList)
            {
                var link = node.Attributes["href"].Value;
                var existSubUrl = articleRepository.FindByLink(link);
                if (existSubUrl != null || string.IsNullOrEmpty(link) || link.Contains("#box_comment_vne") || link.Contains("video"))
                {
                    continue;
                }

                RabbitMQArticles.Add(new RabbitMQArticle(link,source));
            }

            return RabbitMQArticles;
        }
    }
}