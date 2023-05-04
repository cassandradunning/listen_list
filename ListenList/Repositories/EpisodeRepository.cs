using System.Collections.Generic;
using ListenList.Models;
using ListenList.Repositories;
using ListenList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace ListenList.Repositories
{
    public class EpisodeRepository : BaseRepository, IEpisodeRepository
    {
        public EpisodeRepository(IConfiguration configuration) : base(configuration) { }

        public List<Episode> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = EpisodeQuery;

                    var episodes = new List<Episode>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        episodes.Add(NewEpisode(reader));
                    }
                    reader.Close();

                    return episodes;
                }
            }
        }

        public object GetbyId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = EpisodeQuery + " WHERE q.id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    Episode episode = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        episode = NewEpisode(reader);
                    }
                    reader.Close();

                    return episode;
                }
            }
        }

        public void Add(Episode episode)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Episode (Title, Description, URL, Image)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Title, @Description, @URL, @Image)";
                    DbUtils.AddParameter(cmd, "@Title", episode.Title);
                    DbUtils.AddParameter(cmd, "@Description", episode.Description);
                    DbUtils.AddParameter(cmd, "@URL", episode.URL);
                    DbUtils.AddParameter(cmd, "@Image", episode.Image);

                    episode.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Episode episode)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Episode
                           SET Title = @Title,
                               Description = @Description,
                               URL = @URL,
                               Image = @Image,
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", episode.Title);
                    DbUtils.AddParameter(cmd, "@Description", episode.Description);
                    DbUtils.AddParameter(cmd, "@URL", episode.URL);
                    DbUtils.AddParameter(cmd, "@Image", episode.Image);
                    DbUtils.AddParameter(cmd, "@Id", episode.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Episode WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private string EpisodeQuery
        {
            get
            {
                return @"SELECT e.Id, e.Title, e.Description, e.URL, e.Image
                           FROM Episode e";
            }
        }

        private Episode NewEpisode(SqlDataReader reader)
        {
            return new Episode()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Title = DbUtils.GetString(reader, "Title"),
                Description = DbUtils.GetString(reader, "Description"),
                URL = DbUtils.GetString(reader, "URL"),
                Image = DbUtils.GetString(reader,"Image")
            };
        }
    }
}