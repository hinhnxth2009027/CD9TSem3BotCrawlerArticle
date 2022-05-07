using System.ComponentModel;

namespace LinkCrawlerBot.Entity
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int SortNumber { get; set; }
        [DefaultValue(Status.ACTIVE)]
        public static Status Status { get; set; }
    }
}