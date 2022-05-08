using ContentCrawlerBot.Entity;
using Elasticsearch.Net;
using Nest;

namespace ContentCrawlerBot.Util
{
    public class ElasticSearchHelper
    {
        public static ElasticClient searchClient;
        public static string IndexName = "articles";
        public static string DefaultIndexName = "example-index";
        public static string ElasticSearchUser = "elastic";
        public static string ElasticSearchPass = "u6gzVKIowdqLw6K6hpJfea6F";
        public static string CloudId = "CD9T:dXMtY2VudHJhbDEuZ2NwLmNsb3VkLmVzLmlvJGE0MWQ2M2ZhMTFmYjQ4YzQ4NDc1OTFjMDRlYjkyNGExJGNjYjNmOTM1Y2M5ZjQ3ZDE5MjBhYjBkYTVmMzE1NWFh";
        public static ElasticClient GetInstance()
        {
            if (searchClient == null)
            {
                var setting = new ConnectionSettings(CloudId,
                        new BasicAuthenticationCredentials(ElasticSearchUser, ElasticSearchPass))
                    .DefaultIndex(DefaultIndexName)
                    .DefaultMappingFor<Article>(i => i.IndexName(IndexName));
                searchClient = new ElasticClient(setting);
            }

            return searchClient;
        }
    }
}