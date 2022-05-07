using System;
using System.ComponentModel;

namespace LinkCrawlerBot.Entity
{
    public enum Status
    {
        ACTIVE = 1,
        INACTIVE = 2,
        PENDING = 0
    }
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Link { get; set; }
        public string LinkSelector { get; set; }
        public string TitleDetailSelector { get; set; }
        public string DescriptionSelector { get; set; }
        public string ContentDetailSelector { get; set; }
        public string ThumbnailDetailSelector { get; set; }
        public string RemoveSelector { get; set; }
        public string TagDetailSelector { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Enum Status { get; set; } = Enums.Status.CommonStatus.ACTIVE;

        public override string ToString()
        {
            return $"thumbnail : {this.ThumbnailDetailSelector} , Id : {this.Id} - Name : {this.Name} - CateId : {this.CategoryId} , Link : {this.Link} , LinkSelector : {this.LinkSelector} , CreatedAt : {this.CreatedAt}, UpdatedAt : {this.UpdatedAt}";
        }
    }
}