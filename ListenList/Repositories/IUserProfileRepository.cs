using ListenList.Models;

namespace ListenList.Repositories
{
    internal interface IUserProfileRepository
    {
        public UserProfile GetByFirebaseUserId(string firebaseuserProfileId);
        public void Add(UserProfile userProfile);
    }
}