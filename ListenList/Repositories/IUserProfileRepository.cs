using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IUserProfileRepository
    {
        public List<UserProfile> GetAll();
        public UserProfile GetById(int id);
        public UserProfile GetByFirebaseUserId(string firebaseUserId);
        public void Add(UserProfile userProfile);
    }
}