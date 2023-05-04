using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    public interface IPlaylistRepository
    {
        public List<Playlist> GetAllPlaylists();
        public object GetbyPlaylistId(int id);
        public void AddPlaylist(Playlist playlist);
        public void UpdatePlaylist(Playlist playlist);
        public void DeletePlaylist(int id);
    }
}