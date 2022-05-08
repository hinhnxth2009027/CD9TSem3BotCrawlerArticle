using System;

namespace ContentCrawlerBot.Entity
{
    public class Article
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public int CategoryId { get; set; }
        public string Thumbnail { get; set; }
        public string Content  { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public override string ToString()
        {
            return $"{this.Url} :{this.Title} :{this.Slug} :{this.CategoryId} :{this.Thumbnail} :{this.Content} :{this.Description} :{this.CreatedAt} : ";
        }
    }
}