using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using ListenList.Models;
using ListenList.Repositories;
using ListenList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace ListenList.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Category> GetAllCategories()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        c.Id AS CategoryId, c.Name AS CategoryName,
                                        e.Title AS EpisodeTitle, e.Description AS EpisodeDescription, e.Image As EpisodeImage, e.URL AS EpisodeURL, e.CategoryId AS EpisodeCategoryId
                                        FROM Category c
                                        LEFT JOIN Episode e ON e.CategoryId = c.Id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var categories = new List<Category>();
                        while (reader.Read())
                        {
                            var category = new Category()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                                Name = reader.GetString(reader.GetOrdinal("CategoryName")),
                            };
                            categories.Add(category);
                        }

                        return categories;
                    }
                }
            }
        }
    }
}