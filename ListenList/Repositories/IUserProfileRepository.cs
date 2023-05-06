using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAllUsers();
        public UserProfile GetByUserId(int id);
        public UserProfile GetByFirebaseUserId(string firebaseUserId);
        public void AddUsers(UserProfile userProfile);
    }
}