using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IPlaylistRepository
    {
        public Playlist GetPlaylistById(int id);
        public List<Playlist> GetPlaylist();
        public void AddPlaylist(Playlist playlist);
      

    }
}