using System.Collections.Generic;
using System.Linq;
using ListenList.Models;
using ListenList.Repositories;
using ListenList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;


namespace ListenList.Repositories
{
    public class PlaylistRepository : BaseRepository, IPlaylistRepository
    {
        public PlaylistRepository(IConfiguration configuration) : base(configuration) { }



        public Playlist GetPlaylistById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  p.Id, p.[Name] AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId
                        FROM Playlist p
                        WHERE p.Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    Playlist playlist = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        playlist = new Playlist()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "PlaylistName"),
                            Image = DbUtils.GetString(reader, "PlaylistImage"),
                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                        };
                    }
                    reader.Close();

                    return playlist;
                }
            }
        }
        public List<Playlist> GetAllPlaylist()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  p.Id AS PlaylistId, p.[Name] AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId,
                        ep.Id AS EpisodePlaylistId, ep.EpisodeId As EpisodeId,
                        e.Title AS EpisodeTitle, e.Description AS EpisodeDescription, e.Image As EpisodeImage, e.URL AS EpisodeURL, e.CategoryId AS EpisodeCategoryId
                        FROM Playlist p
                        LEFT JOIN EpisodePlaylist ep ON p.Id = ep.PlaylistId
                        LEFT JOIN Episode e ON ep.EpisodeId = e.Id
                        ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var playlists = new List<Playlist>();
                        while (reader.Read())
                        {
                            var playlistId = reader.GetInt32(reader.GetOrdinal("PlaylistId"));
                            var existingPlaylist = playlists.FirstOrDefault(c => c.Id == playlistId);
                            if (existingPlaylist == null)
                            {
                                existingPlaylist = new Playlist()
                                {
                                    Id = playlistId,
                                    Name = reader.GetString(reader.GetOrdinal("PlaylistName")),
                                    Image = reader.GetString(reader.GetOrdinal("PlaylistImage")),
                                    Episodes = new List<Episode>()
                                };
                                playlists.Add(existingPlaylist);
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("EpisodePlaylistId")))
                            {
                                existingPlaylist.Episodes.Add(new Episode()
                                {
                                    Id = DbUtils.GetInt(reader, "EpisodeId"),
                                    Title = DbUtils.GetString(reader, "EpisodeTitle"),
                                    Description = DbUtils.GetString(reader, "EpisodeDescription"),
                                    Image = DbUtils.GetString(reader, "EpisodeImage"),
                                    URL = DbUtils.GetString(reader, "EpisodeURL"),
                                    CategoryId = DbUtils.GetInt(reader, "EpisodeCategoryId"),
                                });
                            }
                        }
                        return playlists;
                    }
                }
            }
        }
        public void AddPlaylist(Playlist playlist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Playlist (Name, Image, UserProfileId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @Image, @UserProfileId)";
                    DbUtils.AddParameter(cmd, "@Name", playlist.Name);
                    DbUtils.AddParameter(cmd, "@Image", playlist.Image);
                    DbUtils.AddParameter(cmd, "@UserProfileId", playlist.UserProfileId);

                    playlist.Id = (int)cmd.ExecuteScalar();

                    //foreach (var e in playlist.EpisodeId)
                    //{
                    //    cmd.CommandText = @"
                    //                INSERT INTO EpisodePlaylist (EpisodeId, PlaylistId)
                    //                VALUES(@EpisodeId, @PlaylistId)";
                    //    cmd.Parameters.Clear();
                    //    cmd.Parameters.AddWithValue("@PlaylistId", playlist.Id);
                    //    cmd.Parameters.AddWithValue("@EpisodeId", e);

                    //    cmd.ExecuteNonQuery();

                    //}
                }
            }
        }


        public List<Playlist> GetPlaylistByUserId(int userProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  p.Id AS PlaylistId, p.[Name] AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId,
                        ep.Id AS EpisodePlaylistId, ep.EpisodeId As EpisodeId,
                        e.Title AS EpisodeTitle, e.Description AS EpisodeDescription, e.Image As EpisodeImage, e.URL AS EpisodeURL, e.CategoryId AS EpisodeCategoryId
                        FROM Playlist p
                        LEFT JOIN EpisodePlaylist ep ON p.Id = ep.PlaylistId
                        LEFT JOIN Episode e ON ep.EpisodeId = e.Id
                        WHERE p.UserProfileId = @UserProfileId
                        ";
                    DbUtils.AddParameter(cmd, "@UserProfileId", userProfileId);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var playlists = new List<Playlist>();
                        while (reader.Read())
                        {
                            var playlistId = reader.GetInt32(reader.GetOrdinal("PlaylistId"));
                            var existingPlaylist = playlists.FirstOrDefault(c => c.Id == playlistId);
                            if (existingPlaylist == null)
                            {
                                existingPlaylist = new Playlist()
                                {
                                    Id = playlistId,
                                    Name = reader.GetString(reader.GetOrdinal("PlaylistName")),
                                    Image = reader.GetString(reader.GetOrdinal("PlaylistImage")),
                                    Episodes = new List<Episode>()
                                };
                                playlists.Add(existingPlaylist);
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("EpisodePlaylistId")))
                            {
                                existingPlaylist.Episodes.Add(new Episode()
                                {
                                    Id = DbUtils.GetInt(reader, "EpisodeId"),
                                    Title = DbUtils.GetString(reader, "EpisodeTitle"),
                                    Description = DbUtils.GetString(reader, "EpisodeDescription"),
                                    Image = DbUtils.GetString(reader, "EpisodeImage"),
                                    URL = DbUtils.GetString(reader, "EpisodeURL"),
                                    CategoryId = DbUtils.GetInt(reader, "EpisodeCategoryId"),
                                });
                            }
                        }
                        return playlists;
                    }
                }
            }
        }

    }
}


