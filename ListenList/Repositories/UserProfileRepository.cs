using ListenList.Models;
using ListenList.Repositories;
using ListenList.Utils;
using Microsoft.Extensions.Configuration;

namespace ListenList.Repositories
{
    public class userProfileProfileRepository : BaseRepository, IUserProfileRepository
    {
        public userProfileProfileRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfile GetByFirebaseUserId(string firebaseuserProfileId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.Id, up.FirebaseUserId, up.[Name] AS UserProfileName, up.Email, up.Username, up.About, up.Image
                        FROM userProfile up     
                        WHERE FirebaseUserId = @FirebaseuserId";

                    DbUtils.AddParameter(cmd, "@FirebaseUserId", firebaseuserProfileId);

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
                            About = DbUtils.GetString(reader, "About"),
                            Image = DbUtils.GetString(reader, "Image"),
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }

        public void Add(UserProfile userProfile)
                    {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO userProfile (FirebaseUserId, [Name], Email, userProfilename, About, Image)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @Name, @Email, @userProfilename, @About, @Image)";
                    DbUtils.AddParameter(cmd, "@FirebaseUserId", userProfile.FirebaseUserId);
                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@Username", userProfile.Username);
                    DbUtils.AddParameter(cmd, "@About", userProfile.About);
                    DbUtils.AddParameter(cmd, "@Image", userProfile.Image);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
