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
        public static string ElasticSearchPass = "SlroOmtlKfAQjmKmwXrkYI1h";
        public static string CloudId = "CD9T:YXNpYS1zb3V0aGVhc3QxLmdjcC5lbGFzdGljLWNsb3VkLmNvbSQ5ZDJlYTgzNjIyZmQ0MWIwYWJiMmE3Y2IxNDc4N2E5YSQ1YzQzZWE2Yjk2ODQ0YjE3YTA3NWE2ODY4MjQwYWQwNA==";
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