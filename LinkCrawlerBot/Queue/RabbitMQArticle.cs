using LinkCrawlerBot.Entity;

namespace LinkCrawlerBot.Queue
{
    public class RabbitMQArticle
    {
        public string Url { get; set; }
        public string TitleDetailSelector { get; set; }
        public string DescriptionSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ThumbnailDetailSelector { get; set; }
        public string RemoveSelector { get; set; }
        public string TagDetailSelector { get; set; }
        public int SourceId { get; set; }

        public RabbitMQArticle(string url, Source source)
        {
            this.Url = url;
            this.TitleDetailSelector = source.TitleDetailSelector;
            this.DescriptionSelector = source.DescriptionSelector;
            this.ContentDetailSelector = source.ContentDetailSelector;
            this.ThumbnailDetailSelector = source.ThumbnailDetailSelector;
            this.RemoveSelector = source.RemoveSelector;
            this.TagDetailSelector = source.TagDetailSelector;
            this.SourceId = source.Id;
        }

        public override string ToString()
        {
            return $"url : {this.Url} - title : {this.TitleDetailSelector} - thumb : {this.ThumbnailDetailSelector} - sourceId : {this.SourceId}";
        }
    }
}