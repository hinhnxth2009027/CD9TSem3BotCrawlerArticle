﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LinkCrawlerBot.Entity;
using LinkCrawlerBot.Repository.IRepository;
using LinkCrawlerBot.Util;

namespace LinkCrawlerBot.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private string SqlTruncateTableArticles = "Truncate TABLE Articles";
        private string SqlFindAll = "SELECT * FROM Articles";
        
        public void TruncateArticles()
        {
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    cnn.Open();
                    var command = new SqlCommand(SqlTruncateTableArticles, cnn);
                    var data = command.ExecuteNonQuery(); 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Article TruncateArticle error: " + e.Message);
                throw;
            }
        }
        
        public List<Article> FinAll()
        {
            List<Article> articles = new List<Article>();
            
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    cnn.Open();
                    var command = new SqlCommand(SqlFindAll, cnn);
                    var data = command.ExecuteReader();
                    while (data.Read())
                    {
                        articles.Add(Create(data));
                    }
                    return articles;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Article FindAll error: " + e.Message);
                throw;
            }
        }
        
        public Article FindByLink(string linkSource)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetConnection())
                {
                    var SqlFindByLink = $"SELECT * FROM Articles WHERE Url = '{linkSource}'";
                    cnn.Open();
                    var command = new SqlCommand(SqlFindByLink, cnn);
                    command.Prepare();
                    var data = command.ExecuteReader();
                    return data.Read() ? Create(data) : null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Article FindByLink error :" + e.Message);
                throw;
            }
        }

        public Article Create(SqlDataReader data)
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