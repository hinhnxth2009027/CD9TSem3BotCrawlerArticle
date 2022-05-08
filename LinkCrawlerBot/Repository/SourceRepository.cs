using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LinkCrawlerBot.Entity;
using LinkCrawlerBot.Repository.IRepository;
using LinkCrawlerBot.Util;

namespace LinkCrawlerBot.Repository
{
    public class SourceRepository : ISourceRepository
    {
        string SqlFindAllSource = "SELECT * FROM Sources";
        public List<Source> FindAll()
        {
            List<Source> sources = new List<Source>();
            try
            {
                using var cnn = ConnectionHelper.GetConnection();
                cnn.Open();
                var data = new SqlCommand(SqlFindAllSource,cnn).ExecuteReader();
                
                while (data.Read())
                {
                    sources.Add( new Source()
                    {
                        Id = Convert.ToInt32(data["Id"]),
                        Name = (string)data["Name"],
                        CategoryId = Convert.ToInt32(data["CategoryId"]),
                        Link = (string)data["Link"],
                        LinkSelector = (string)data["LinkSelector"],
                        ContentDetailSelector = (string)data["ContentDetailSelector"],
                        ThumbnailDetailSelector = (string)data["ThumbnailDetailSelector"],
                        TitleDetailSelector = (string)data["TitleDetailSelector"],
                        DescriptionSelector = (string)data["DescriptionSelector"],
                        RemoveSelector = (string)data["RemoveSelector"],
                        TagDetailSelector = (string)data["TagDetailSelector"],
                        CreatedAt = (DateTime)data["CreatedAt"],
                        UpdatedAt = (DateTime)data["UpdatedAt"],
                        Status = (Enums.Status.CommonStatus)data["Status"],
                    });
                }
                
                return sources;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}