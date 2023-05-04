using ListenList.Models;
using System.Collections.Generic;

namespace ListenList.Repositories
{
    internal interface IPlaylistRepository
    {
        public List<Playlist> GetAll();
        public object GetbyId(int id);
        public void Add(Playlist playlist);
        public void Update(Playlist episode);
        public void Delete(int id);
    }
}