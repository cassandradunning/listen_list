using System.Collections.Generic;
using System.Numerics;
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

        public List<Episode> GetAllEpisodes()
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

        public object GetbyEpisodeId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = EpisodeQuery + " WHERE e.id = @Id";
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

        public List<Episode> GetEpisodeByPlaylistId(int PlaylistId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = EpisodeQuery + " WHERE ep.PlaylistId = @PlaylistId";
                    DbUtils.AddParameter(cmd, "@PlaylistId", PlaylistId);

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

        public void AddEpisode(Episode episode)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Episode (Title, Description, URL, Image, CategoryId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Title, @Description, @URL, @Image, @CategoryId)";
                    DbUtils.AddParameter(cmd, "@Title", episode.Title);
                    DbUtils.AddParameter(cmd, "@Description", episode.Description);
                    DbUtils.AddParameter(cmd, "@URL", episode.URL);
                    DbUtils.AddParameter(cmd, "@Image", episode.Image);
                    DbUtils.AddParameter(cmd, "@CategoryId", episode.CategoryId);

                    episode.Id = (int)cmd.ExecuteScalar();

                    foreach (var p in episode.PlaylistId)
                    {
                        cmd.CommandText = @"
                            INSERT INTO EpisodePlaylist (EpisodeId, PlaylistId)
                            VALUES(@EpisodeId, @PlaylistId)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@EpisodeId", episode.Id);
                        cmd.Parameters.AddWithValue("@PlaylistId", p);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void UpdateEpisode(Episode episode)
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
                               CategoryId = @CategoryId
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Title", episode.Title);
                    DbUtils.AddParameter(cmd, "@Description", episode.Description);
                    DbUtils.AddParameter(cmd, "@URL", episode.URL);
                    DbUtils.AddParameter(cmd, "@Image", episode.Image);
                    DbUtils.AddParameter(cmd, "@CategoryId", episode.CategoryId);
                    DbUtils.AddParameter(cmd, "@Id", episode.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEpisode(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM EpisodePlaylist where episodeId = @Id
                                        DELETE FROM Episode where Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private string EpisodeQuery
        {
            get
            {
                return @"
                        SELECT e.Id AS EpisodeId, e.Title AS EpisodeTitle, e.Description AS EpisodeDescription, e.URL AS EpisodeURL, e.Image AS EpisodeImage, 
                        c.Id AS CategoryId, c.Name AS CategoryName,
                        ep.Id AS EpisodePlayistId, ep.PlaylistId AS PlaylistId,
                        p.Id AS PlaylistId, p.Name AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId AS PlaylistUserProfileId
                           FROM Episode e
                           LEFT JOIN Category c on e.CategoryId = c.Id
                           LEFT JOIN EpisodePlaylist ep on ep.EpisodeId = e.Id
                           LEFT JOIN Playlist p on p.Id = ep.PlaylistId";
            }
        }

        private Episode NewEpisode(SqlDataReader reader)
        {
            return new Episode()
            {
                Id = DbUtils.GetInt(reader, "EpisodeId"),
                Title = DbUtils.GetString(reader, "EpisodeTitle"),
                Description = DbUtils.GetString(reader, "EpisodeDescription"),
                URL = DbUtils.GetString(reader, "EpisodeURL"),
                Image = DbUtils.GetString(reader,"EpisodeImage"),
                CategoryId = DbUtils.GetInt(reader,"CategoryId"),
                Category = new Category()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                    Name = reader.GetString(reader.GetOrdinal("CategoryName"))
                },
                PlaylistId = new List<int>()
                //Playlist = new Playlist()
                //{
                //    Id = reader.GetInt32(reader.GetOrdinal("PlaylistId")),
                //    Name = reader.GetString(reader.GetOrdinal("PlaylistName")),
                //    Image = reader.GetString(reader.GetOrdinal("PlaylistImage")),
                //    UserProfileId = reader.GetInt32(reader.GetOrdinal("PlaylistUserProfileId")),
                //},
            };
        }
    }
}