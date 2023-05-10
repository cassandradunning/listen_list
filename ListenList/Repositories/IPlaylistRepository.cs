using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IPlaylistRepository
    {
        public Playlist GetPlaylistById(int id);
        public List<Playlist> GetAllPlaylist();
        public void AddPlaylist(Playlist playlist);
        public List<Playlist> GetPlaylistByUserId(int userProfileId);


    }
}