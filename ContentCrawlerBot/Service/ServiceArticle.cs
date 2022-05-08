using System;
using System.Text;
using ContentCrawlerBot.Entity;
using ContentCrawlerBot.Queue;
using ContentCrawlerBot.Repository;
using ContentCrawlerBot.Repository.IRepository;
using ContentCrawlerBot.Service.IService;
using HtmlAgilityPack;

namespace ContentCrawlerBot.Service
{
    public class ServiceArticle : IServiceArticle
    {
        private IRepositoryArticle _repositoryArticle;
        public ServiceArticle()
        {
            _repositoryArticle = new RepositoryArticle();
        }
        public Article Save(Article article)
        {
            return _repositoryArticle.Save(article);
        }

        public Article GetArticle(RabbitMqArticle _rabbitMqArticle)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                var web = new HtmlWeb();
                HtmlDocument doc = web.Load(_rabbitMqArticle.Url);
                var title = doc.QuerySelector(_rabbitMqArticle.TitleDetailSelector)?.InnerText;
                var slug = NonUnicode(title).Replace(" ", "_");
                var description = doc.QuerySelector(_rabbitMqArticle.DescriptionSelector)?.InnerText;
                var image = doc.QuerySelector(_rabbitMqArticle.ThumbnailDetailSelector)?.Attributes["data-src"].Value;
                
                string thumbnail = "";
                if (image != null)
                {
                    thumbnail = image;
                }
                else
                {
                    thumbnail = "http://mcvideomd1fr.keeng.net/playnow/images/channel/avatar/20181128/0375357432_20181128122718.jpg";
                }
                var contents = doc.QuerySelectorAll(_rabbitMqArticle.ContentDetailSelector);
                StringBuilder contentBuilder = new StringBuilder();
                foreach (var content in contents)
                {
                    contentBuilder.Append(content.InnerHtml);
                }
                
                Article article = new Article()
                {
                    Url = _rabbitMqArticle.Url,
                    Title = title,
                    Slug = slug,
                    CategoryId = _rabbitMqArticle.CategoryId,
                    Thumbnail = thumbnail,
                    Content = contentBuilder.ToString(),
                    Description = description,
                    CreatedAt = DateTime.Now,
                };
                return article;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public static string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",  
                "đ",  
                "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",  
                "í","ì","ỉ","ĩ","ị",  
                "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",  
                "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",  
                "ý","ỳ","ỷ","ỹ","ỵ",};  
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",  
                "d",  
                "e","e","e","e","e","e","e","e","e","e","e",  
                "i","i","i","i","i",  
                "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",  
                "u","u","u","u","u","u","u","u","u","u","u",  
                "y","y","y","y","y",};  
            for (int i = 0; i < arr1.Length; i++)  
            {  
                text = text.Replace(arr1[i], arr2[i]);  
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());  
            }  
            return text.ToLower();  
        }  
    }
}