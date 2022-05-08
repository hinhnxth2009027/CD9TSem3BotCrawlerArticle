using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ContentCrawlerBot.Entity;
using ContentCrawlerBot.Repository.IRepository;
using ContentCrawlerBot.Util;

namespace ContentCrawlerBot.Repository
{
    public class RepositoryArticle : IRepositoryArticle
    {
        private string QueryAllArticle = "SELECT * FROM Articles";
        private string QueryGetArticleByUrl = "SELECT * FROM Articles WHERE UrlSource = @UrlSource";
        private string InsertQuery = "INSERT INTO Articles( Url, Title, Slug, CategoryId, Thumbnail, Content, Description, CreatedAt,UpdatedAt,Status)" +
                                     " VALUES ( @Url, @Title, @Slug, @CategoryId, @Thumbnail,@Content, @Description, @CreatedAt, @UpdatedAt,@Status) ";
        public List<Article> GetAll()
        {
            List<Article> articles = new List<Article>();
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    cnn.Open();
                    var command = new SqlCommand(QueryAllArticle, cnn);
                    var data = command.ExecuteReader();
                    while (data.Read())
                    {
                        articles.Add(CreateArticle(data));
                    }
                    return articles;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;

            }
        }

        public Article FindArticleByUrl(string urlSource)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    cnn.Open();
                    var command = new SqlCommand(QueryGetArticleByUrl, cnn);
                    command.Prepare();
                    command.Parameters.AddWithValue("@UrlSource", urlSource);
                    var data = command.ExecuteReader();
                    return data.Read() ? CreateArticle(data) : null;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public Article Save(Article article)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    cnn.Open();
                    var command = new SqlCommand(InsertQuery, cnn);
                    command.Prepare();
                    command.Parameters.AddWithValue("@Url", article.Url);
                    command.Parameters.AddWithValue("@Title", article.Title ?? "");
                    command.Parameters.AddWithValue("@Slug", article.Slug ?? "");
                    command.Parameters.AddWithValue("@CategoryId", article.CategoryId);
                    command.Parameters.AddWithValue("@Thumbnail", article.Thumbnail ?? "");
                    command.Parameters.AddWithValue("@Content", article.Content ?? "");
                    command.Parameters.AddWithValue("@Description", article.Description ?? "");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@Status", 1 );
                    command.ExecuteNonQuery();
                    ElasticSearchHelper.GetInstance().IndexDocument(article);
                    return article;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        private Article CreateArticle(SqlDataReader data)
        {
            return new Article()
            {
                Url = (string)data["Url"],
                Title = (string)data["Title"],
                Slug = (string)data["Slug"],
                CategoryId = Convert.ToInt32(data["CategoryId"]),
                Thumbnail = (string)data["Thumbnail"],
                Content = (string)data["Content"],
                Description = (string)data["Description"],
                CreatedAt = (DateTime)data["CreatedAt"]
            };
        }
    }
}