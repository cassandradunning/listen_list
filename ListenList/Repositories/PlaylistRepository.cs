using System.Collections.Generic;
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
        public List<Playlist> GetPlaylist()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT  p.Id, p.[Name] AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId
                        FROM Playlist p
                        ";
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var playlists = new List<Playlist>();
                        while (reader.Read())
                        {
                            playlists.Add(new Playlist()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "PlaylistName"),
                                Image = DbUtils.GetString(reader, "PlaylistImage"),
                                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
                            }); 
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
                }
            }
        }

       


        //private string PlaylistQuery
        //{
        //    get
        //    {
        //        return @"SELECT  p.Id AS PlaylistId, p.[Name] AS PlaylistName, p.Image AS PlaylistImage, p.UserProfileId AS UserProfileId,
        //                e.Title AS EpisodeTitle, e.Description, e.URL AS EpisodeURL, e.Image AS EpisodeImage,
        //                up.Username
        //                FROM Playlist p
        //                JOIN EpisodePlaylist ep ON ep.PlaylistId = p.Id
        //                JOIN Episode e ON e.Id = ep.EpisodeId
        //                JOIN UserProfile up on p.UserProfileId = up.Id";
        //    }
        //}

        //private Playlist NewPlaylist(SqlDataReader reader)
        //{
        //    return new Playlist()
        //    {
        //        Id = DbUtils.GetInt(reader, "Id"),
        //        Name = DbUtils.GetString(reader, "Name"),
        //        Image = DbUtils.GetString(reader, "Image"),
        //        UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
        //    };
        //}


    }

}
