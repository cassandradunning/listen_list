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

        public List<Playlist> GetAll()
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

        public object GetbyId(int id)
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

        public void Add(Playlist playlist)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Playlist (Name, Image, EpisodePlaylistId, UserId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @Image, @EpisodePlaylistId, @UserId)";
                    DbUtils.AddParameter(cmd, "@Name", playlist.Name);
                    DbUtils.AddParameter(cmd, "@Image", playlist.Image);
                    DbUtils.AddParameter(cmd, "@EpisodePlaylistId", playlist.EpisodePlaylistId);
                    DbUtils.AddParameter(cmd, "@UserId", playlist.UserId);

                    playlist.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Playlist episode)
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
                               UserId = @UserId,
                            WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", episode.Name);
                    DbUtils.AddParameter(cmd, "@Image", episode.Image);
                    DbUtils.AddParameter(cmd, "@EpisodePlaylistId", episode.EpisodePlaylistId);
                    DbUtils.AddParameter(cmd, "@UserId", episode.UserId);
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
                return @"SELECT e.Id, e.Name, e.Image, e.EpisodePlaylistId, e.UserId
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
                UserId = DbUtils.GetInt(reader, "UserId")
            };
        }
    }

}
