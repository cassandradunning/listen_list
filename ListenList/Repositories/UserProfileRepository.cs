using ListenList.Models;
using ListenList.Repositories;
using ListenList.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT Id, [Name], FirebaseUserId, Username, Email, Bio, Image 
                    FROM UserProfile";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var profiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            profiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                                FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                Username = DbUtils.GetString(reader, "Username"),
                                Email = DbUtils.GetString(reader, "Email"),
                                Bio = DbUtils.GetString(reader, "Bio"),
                                Image = DbUtils.GetString(reader, "Image")
                            });
                        }
                        return profiles;
                    }
                }
            }
        }

        public UserProfile GetByUserId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT up.Id, up.[Name],up.FirebaseUserId, up.Username up.Email, up.Bio, up.Image, 
                    p.[Name], p.Image, p.EpisodePlaylistId, p.UserProfileId
                    FROM UserProfile up
                    LEFT JOIN Playlist p ON p.UserProfileId = up.Id
                    LEFT JOIN EpisodePlaylist ep ON ep.EpisodeId = p.EpisodePlaylistId
                    LEFT JOIN Episode e ON e.Id = ep.EpisodeId
                    WHERE up.Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile user = null;
                        while (reader.Read())
                        {
                            if (user == null)
                            {
                                user = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                                    Username = DbUtils.GetString(reader, "Username"),
                                    Email = DbUtils.GetString(reader, "Email"), 
                                    Bio = DbUtils.GetString(reader, "Bio"),
                                    Image = DbUtils.GetString(reader, "Image"),
                                    Playlists = new List<Playlist>()
                                };

                            }
                            if (DbUtils.IsNotDbNull(reader, "PlaylistId"))
                            {
                                user.Playlists.Add(new Playlist()
                                {
                                    Id = DbUtils.GetInt(reader, "PlaylistId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                    Image = DbUtils.GetString(reader, "Image")                                   
                                });  
                            }
                        }
                        return user;
                    }
                }
            }
        }
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.FirebaseUserId, up.[Name] AS UserProfileName, up.Email, up.Username, up.Bio, up.Image
                        FROM userProfile up     
                        WHERE FirebaseUserId = @FirebaseUserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseUserId);

                    UserProfile user = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new UserProfile()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseUserId = DbUtils.GetString(reader, "FirebaseUserId"),
                            Name = DbUtils.GetString(reader, "UserProfileName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Username = DbUtils.GetString(reader, "Username"),
                            Bio = DbUtils.GetString(reader, "Bio"),
                            Image = DbUtils.GetString(reader, "Image"),
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }

        public void AddUsers(UserProfile userProfile)
                    {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO userProfile (FirebaseUserId, [Name], Email, Username, Bio, Image)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, @Email, @Username, @Bio, @Image)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@Username", userProfile.Username);
                    DbUtils.AddParameter(cmd, "@Bio", userProfile.Bio);
                    DbUtils.AddParameter(cmd, "@Image", userProfile.Image);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
