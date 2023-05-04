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

        public List<Playlist> GetAllPlaylists()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = PlaylistQuery;

                    var playlists = new List<Playlist>();

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        playlists.Add(NewPlaylist(reader));
                    }
                    reader.Close();

                    return playlists;
                }
            }
        }

        public object GetbyPlaylistId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = PlaylistQuery + " WHERE q.id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);

                    Playlist playlist = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        playlist = NewPlaylist(reader);
                    }
                    reader.Close();

                    return playlist;
                }
            }
        }
        //public object GetbyUserProfileId(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = PlaylistQuery + " WHERE q.id = @Id";
        //            DbUtils.AddParameter(cmd, "@Id", id);

        //            Playlist playlist = null;

        //            var reader = cmd.ExecuteReader();
        //            if (reader.Read())
        //            {
        //                playlist = NewPlaylist(reader);
        //            }
        //            reader.Close();

        //            return playlist;
        //        }
        //    }
        //}
        public void AddPlaylist(Playlist playlist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Playlist (Name, Image, EpisodePlaylistId, UserProfileId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @Image, @EpisodePlaylistId, @UserProfileId)";
                    DbUtils.AddParameter(cmd, "@Name", playlist.Name);
                    DbUtils.AddParameter(cmd, "@Image", playlist.Image);
                    DbUtils.AddParameter(cmd, "@EpisodePlaylistId", playlist.EpisodePlaylistId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", playlist.UserProfileId);

                    playlist.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Playlist
                           SET Name = @Name,
                               Image = @Image,
                               EpisodePlaylistId = @EpisodePlaylistId,
                               UserProfileId = @UserProfileId,
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", playlist.Name);
                    DbUtils.AddParameter(cmd, "@Image", playlist.Image);
                    DbUtils.AddParameter(cmd, "@EpisodePlaylistId", playlist.EpisodePlaylistId);
                    DbUtils.AddParameter(cmd, "@UserProfileId", playlist.UserProfileId);
                    DbUtils.AddParameter(cmd, "@Id", playlist.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //not in action right now
        public void DeletePlaylist(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Playlist WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private string PlaylistQuery
        {
            get
            {
                return @"SELECT e.Id, e.Name, e.Image, e.EpisodePlaylistId, e.UserProfileId
                           FROM Playlist e";
            }
        }

        private Playlist NewPlaylist(SqlDataReader reader)
        {
            return new Playlist()
            {
                Id = DbUtils.GetInt(reader, "Id"),
                Name = DbUtils.GetString(reader, "Name"),
                Image = DbUtils.GetString(reader, "Image"),
                EpisodePlaylistId = DbUtils.GetInt(reader, "EpisodePlaylistId"),
                UserProfileId = DbUtils.GetInt(reader, "UserProfileId")
            };
        }
    }

}
